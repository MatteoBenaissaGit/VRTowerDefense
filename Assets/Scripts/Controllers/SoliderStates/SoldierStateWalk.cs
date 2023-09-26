using UnityEngine;

namespace Controllers.SoliderStates
{
    public class SoldierStateWalk : SoldierStateBase
    {
        public SoldierStateWalk(SoldierController controller) : base(controller)
        {
            
        }
        
        public override void OnEnterState()
        {
        }

        public override void UpdateState()
        {
            MoveTowardTargetPosition();
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