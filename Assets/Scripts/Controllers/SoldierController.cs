using System;
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
        Walk = 2
    }
    
    public class SoldierController : EntityController<SoldierGameplayData>
    {
        [field:SerializeField] public SoldierView View { get; private set; }
        
        public TroopController Troop { get; private set; }
        public PathUserManager PathManager { get; private set; }
        public SoldierStateEnum CurrentState { get; private set; }
        public Action<SoldierStateEnum> OnStateChanged { get; set; }
        
        private SoldierStateBase _soldierState;

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
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }

            CurrentState = state;
            
            _soldierState?.OnEnterState();
        }
    }
}