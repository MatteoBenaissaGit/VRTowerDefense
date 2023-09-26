using DG.Tweening;
using Managers;
using UnityEngine;

namespace Controllers.SoliderStates
{
    public class SoldierStateSpawn : SoldierStateBase
    {
        public SoldierStateSpawn(SoldierController controller) : base(controller)
        {
            
        }

        private float _time;
        private float _spawnTime;
        
        public override void OnEnterState()
        {
            Controller.View.SpawnAnimation();
        }

        public override void UpdateState()
        {
            _time += Time.deltaTime;
            if (_time > _spawnTime)
            {
                Controller.SetState(SoldierStateEnum.Walk);
            }
        }

        public override void OnExitState()
        {
            
        }
    }
}