using Data.Troops;
using Managers;
using UnityEngine;

namespace Controllers.SoliderStates
{
    public class SoldierStateWalk : SoldierStateBase
    {
        private SoldierData _data;
        
        public SoldierStateWalk(SoldierController controller) : base(controller)
        {
            _data = RessourceManager.Instance.TroopsData.GetSoldierData(Controller.GameplayData.Type);
        }
        
        public override void OnEnterState()
        {
        }

        public override void UpdateState()
        {
            MoveTowardTargetPosition();

            float distance = Vector3.Distance(Controller.transform.position, Controller.GameplayData.TargetPosition);
            if (distance <= _data.DistanceToPathPointToSetReached)
            {
                Controller.GameplayData.TargetPosition = Controller.PathManager.GetNextPoint();
            }
        }

        public override void OnExitState()
        {
            
        }
        
        private void MoveTowardTargetPosition()
        {
            Vector3 position = Controller.transform.position;
            Vector3 targetPosition = Controller.GameplayData.TargetPosition - Controller.GameplayData.OffsetFromSpawnPoint;
            Controller.transform.position = Vector3.MoveTowards(position, targetPosition, Controller.GameplayData.Speed / 100);
        }
    }
}