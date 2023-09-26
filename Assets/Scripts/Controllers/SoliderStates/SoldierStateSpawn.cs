using DG.Tweening;
using Managers;
using UnityEngine;

namespace Controllers.SoliderStates
{
    public class SoldierStateSpawn : SoldierStateBase
    {
        public SoldierStateSpawn(SoldierController controller) : base(controller)
        {
            _spawnTime = RessourceManager.Instance.TroopsData.GetSoldierData(Controller.GameplayData.Type).SpawnTime;
        }

        private float _time;
        private float _spawnTime;
        
        public override void OnEnterState()
        {
            Controller.transform.localScale = Vector3.zero;
            Controller.transform.DOScale(Vector3.one, _spawnTime);
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