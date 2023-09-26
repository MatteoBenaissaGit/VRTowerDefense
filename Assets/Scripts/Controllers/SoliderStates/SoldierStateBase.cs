using Unity.VisualScripting;

namespace Controllers.SoliderStates
{
    public abstract class SoldierStateBase
    {
        public SoldierStateBase(SoldierController controller)
        {
            Controller = controller;
        }
        public SoldierController Controller;
        
        public abstract void OnEnterState();
        public abstract void UpdateState();
        public abstract void OnExitState();
    }
}