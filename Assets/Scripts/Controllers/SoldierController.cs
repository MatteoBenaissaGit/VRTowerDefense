using System;
using System.Collections.Generic;
using Controllers.SoliderStates;
using Data.Troops;
using Managers;
using PathGameplay;
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
        
        public TroopController Troop { get; private set; }
        public PathUserManager PathManager { get; private set; }
        public SoldierStateEnum CurrentState { get; private set; }
        public Action<SoldierStateEnum> OnStateChanged { get; set; }
        
        private SoldierStateBase _soldierState;
        private SoldierController[] _soldiersInRange;
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

        public override void Die()
        {
            base.Die();
            Troop.OnSoldierDie.Invoke(this);
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
                    break;
                case SoldierStateEnum.AttackSoldier:
                    _soldierState = new SoldierStateAttack(this, GetClosestSoldierToAttack(), null);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }

            CurrentState = state;
            
            _soldierState?.OnEnterState();
        }
        
        private void DetectEnemies()
        {
            Vector3 origin = transform.position;

            RaycastHit[] results = new RaycastHit[100];
            var size = Physics.SphereCastNonAlloc(origin, GameplayData.DetectionRange, transform.forward, results, 0);

            List<SoldierController> controllers = new List<SoldierController>();
            BaseManager baseToAttack = null;
            foreach (RaycastHit hit in results)
            {
                if (hit.collider == null)
                {
                    continue;
                }
                
                SoldierController soldier = hit.collider.gameObject.GetComponent<SoldierController>();
                if (soldier != null)
                {
                    controllers.Add(soldier);
                    continue;
                }

                BaseManager attackBase = hit.collider.gameObject.GetComponent<BaseManager>();
                if (attackBase != null && attackBase.User != GameplayData.User)
                {
                    baseToAttack = attackBase;
                }
            }
            _baseToAttack = baseToAttack;
            _soldiersInRange = controllers.ToArray();
        }

        public SoldierController GetClosestSoldierToAttack()
        {
            if (_soldiersInRange == null || _soldiersInRange.Length == 0)
            {
                return null;
            }

            float minDistance = float.MaxValue;
            SoldierController closest = null;
            foreach (SoldierController soldier in _soldiersInRange)
            {
                if (soldier.GameplayData.User == GameplayData.User)
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

        public bool IsInRange(Transform t)
        {
            float distance = Mathf.Sqrt((transform.position - t.position).sqrMagnitude);
            return distance <= GameplayData.AttackRange;
        }
    }
}