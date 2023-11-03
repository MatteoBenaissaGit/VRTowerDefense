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
            Controller.View.SetAnimation("walk",false);
        }

        public override void UpdateState()
        {
            MoveTowardTargetPosition();

            float distance = Vector3.Distance(Controller.transform.position, Controller.GameplayData.TargetPosition - Controller.GameplayData.OffsetFromSpawnPoint);
            if (distance <= _data.DistanceToPathPointToSetReached)
            {
                Controller.GameplayData.TargetPosition = Controller.PathManager.GetNextPoint();
            }

            BaseManager baseToAttack = Controller.GetBaseToAttack();
            if (baseToAttack != null && Controller.IsInDetectionRange(baseToAttack.transform))
            {
                Controller.SetState(SoldierStateEnum.AttackBase);
            }

            SoldierController soldierToAttack = Controller.GetClosestSoldierToAttack();
            if (soldierToAttack != null && Controller.IsInDetectionRange(soldierToAttack.transform))
            {
                Controller.SetState(SoldierStateEnum.AttackSoldier);
            }
        }

        public override void OnExitState()
        {
            //Controller.View.SetAnimation("walk",false, false);
        }
        
        private void MoveTowardTargetPosition()
        {
            Vector3 position = Controller.transform.position;
            Vector3 targetPosition = Controller.GameplayData.TargetPosition - Controller.GameplayData.OffsetFromSpawnPoint;
            Controller.transform.position = Vector3.MoveTowards(position, targetPosition, (Controller.GameplayData.Speed) * Time.deltaTime);
        }
    }
}