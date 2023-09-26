using System;
using Controllers.SoliderStates;
using Data.Troops;
using Managers;
using PathGameplay;
using UnityEngine;

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

        public SoldierType Type { get; set; }
    }

    public enum SoldierStateEnum
    {
        None = 0,
        Spawn = 1,
        Walk = 2
    }
    
    public class SoldierController : EntityController<SoldierGameplayData>
    {
        public PathUserManager PathManager { get; private set; }
        
        private TroopController _troop;
        private SoldierStateBase _soldierState;
        private SoldierStateEnum _currentState;

        public void SetSoldier(SoldierData data, TroopController troop, Path path)
        {
            GameplayData = new SoldierGameplayData(data);
            _troop = troop;
            PathManager = new PathUserManager(path);
            
            SetState(SoldierStateEnum.Spawn);
        }

        private void Update()
        {
            _soldierState.UpdateState();
        }

        public override void Die()
        {
            base.Die();
            _troop.OnSoldierDie.Invoke(this);
        }

        public void SetState(SoldierStateEnum state)
        {
            if (state == _currentState)
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
            
            _soldierState?.OnEnterState();
        }
    }
}