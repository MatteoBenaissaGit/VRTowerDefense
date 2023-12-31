﻿using System;
using System.Collections.Generic;
using Controllers.SoliderStates;
using Data.Troops;
using DG.Tweening;
using Managers;
using PathGameplay;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using View;

namespace Controllers
{
    public enum SoldierType
    {
        SimpleCac = 0,
        SimpleDistance = 1
    }

    public class SoldierGameplayData : EntityGameplayData
    {
        public SoldierGameplayData(SoldierData data) : base(data)
        {
            Type = data.Type;
        }

        public SoldierType Type { get; private set; }
        public UserType User { get; set; }
        public bool IsGoingToAnotherPath { get; set; }
        public Action IsAtPathEnd { get; set; } //TODO use this to launch the soldier attack to the enemy base
        public Vector3 OffsetFromSpawnPoint { get; set; }
        public Vector3 TargetPosition { get; set; }
    }

    public enum SoldierStateEnum
    {
        None = 0,
        Spawn = 1,
        Walk = 2,
        AttackBase = 3,
        AttackSoldier = 4
    }
    
    public class SoldierController : EntityController<SoldierGameplayData>
    {
        [field:SerializeField] public SoldierView View { get; private set; }
        [field:SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field:SerializeField] public ParticleSystem DeathParticles { get; private set; }
        [field:SerializeField] public LayerMask SoldiersAndBaseLayer { get; private set; }
        
        public TroopController Troop { get; private set; }
        public PathUserManager PathManager { get; private set; }
        public SoldierStateEnum CurrentState { get; private set; }
        public Action<SoldierStateEnum> OnStateChanged { get; set; }
        public Transform AttackTarget { get; private set; }
        
        private SoldierStateBase _soldierState;
        private List<SoldierController> _soldiersInRange = new List<SoldierController>();
        private BaseManager _baseToAttack;

        private void Awake()
        {
            View.Initialize(this);
        }

        public void SetSoldier(SoldierData data, TroopController troop, Path path, Vector3 offsetFromSpawnPoint, UserType user)
        {
            GameplayData = new SoldierGameplayData(data);
            Troop = troop;
            
            SetState(SoldierStateEnum.Spawn);

            GameplayData.User = user;
            PathManager = new PathUserManager(path, user);
            GameplayData.TargetPosition = PathManager.GetNextPoint();
            GameplayData.OffsetFromSpawnPoint = offsetFromSpawnPoint;
        }

        private void Update()
        {
            _soldierState?.UpdateState();

            if (Rigidbody.velocity.magnitude != 0)
            {
                Rigidbody.velocity = Vector3.Lerp(Rigidbody.velocity, Vector3.zero, 2f * Time.deltaTime);
            }
            
            DetectEnemies();
        }

        public void SetLife(int value)
        {
            if (gameObject == null || transform == null)
            {
                return;
            }
        
            transform.DOComplete();
            transform.DOPunchScale(Vector3.up * 0.1f, 0.5f);
            GameplayData.Life += value;
            if (GameplayData.Life <= 0)
            {
                Debug.Log("die");
                Die();
            }
        }

        public override void Die()
        {
            base.Die();
            Troop.OnSoldierDie.Invoke(this);
            DeathParticles.Play();
            DeathParticles.transform.parent = null;
            Destroy(this.gameObject);
        }

        public void SetState(SoldierStateEnum state)
        {
            if (state == CurrentState)
            {
                return;
            }

            _soldierState?.OnExitState();

            switch (state)
            {
                case SoldierStateEnum.None:
                    Debug.LogError("Soldier state set to None");
                    break;
                case SoldierStateEnum.Walk:
                    _soldierState = new SoldierStateWalk(this);
                    break;
                case SoldierStateEnum.Spawn:
                    _soldierState = new SoldierStateSpawn(this);
                    break;
                case SoldierStateEnum.AttackBase:
                    _soldierState = new SoldierStateAttack(this, null, _baseToAttack);
                    AttackTarget = _baseToAttack.transform;
                    break;
                case SoldierStateEnum.AttackSoldier:
                    _soldierState = new SoldierStateAttack(this, GetClosestSoldierToAttack(), null);
                    AttackTarget = GetClosestSoldierToAttack().View.transform;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }

            CurrentState = state;
            
            _soldierState?.OnEnterState();
        }

        private int _currentDetectionSize;
        private RaycastHit[] _results = new RaycastHit[10];

        private void DetectEnemies()
        {
            int size = Physics.SphereCastNonAlloc(transform.position, GameplayData.DetectionRange, transform.forward, _results, 0, SoldiersAndBaseLayer);
            if (size == _currentDetectionSize)
            {
                return;
            }
            _currentDetectionSize = size;
            
            _soldiersInRange.Clear();
            _baseToAttack = null;
            
            foreach (RaycastHit hit in _results)
            {
                Collider hitCollider = hit.collider;
                if (hitCollider == null)
                {
                    continue;
                }
                
                SoldierController soldier = hitCollider.gameObject.GetComponent<SoldierController>();
                if (soldier != null)
                {
                    _soldiersInRange.Add(soldier);
                    continue;
                }

                BaseManager attackBase = hitCollider.gameObject.GetComponent<BaseManager>();
                if (attackBase != null && attackBase.User != GameplayData.User)
                {
                    _baseToAttack = attackBase;
                }
            }
        }

        public SoldierController GetClosestSoldierToAttack()
        {
            if (_soldiersInRange == null || _soldiersInRange.Count == 0)
            {
                return null;
            }

            float minDistance = float.MaxValue;
            SoldierController closest = null;
            foreach (SoldierController soldier in _soldiersInRange)
            {
                if (soldier == null || soldier.transform == null ||
                    soldier.GameplayData.User == GameplayData.User)
                {
                    continue;
                }
                
                float distance = Mathf.Sqrt((transform.position - soldier.transform.position).sqrMagnitude);
                if (distance >= minDistance)
                {
                    continue;
                }

                minDistance = distance;
                closest = soldier;
            }

            return closest;
        }

        public BaseManager GetBaseToAttack()
        {
            return _baseToAttack;
        }

        public bool IsInAttackRange(Transform t)
        {
            float distance = Vector3.Distance(transform.position, t.position);
            return distance <= GameplayData.AttackRange;
        }
        
        public bool IsInDetectionRange(Transform t)
        {
            float distance = Vector3.Distance(transform.position, t.position);
            return distance <= GameplayData.DetectionRange;
        }
        
        #if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if (Application.isPlaying == false)
            {
                return;
            }
            
            Handles.color = Color.yellow;
            Handles.DrawWireDisc(transform.position,Vector3.up, GameplayData.DetectionRange);
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position,Vector3.up, GameplayData.AttackRange);
        }

#endif
    }
}