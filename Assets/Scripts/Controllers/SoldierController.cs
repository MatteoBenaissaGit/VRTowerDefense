using Data.Troops;
using Managers;
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
    
    public class SoldierController : EntityController<SoldierGameplayData>
    {
        private TroopController _troop;
        
        public void SetSoldier(SoldierData data, TroopController troop)
        {
            GameplayData = new SoldierGameplayData(data);
            _troop = troop;
        }

        public override void Die()
        {
            base.Die();
            _troop.OnSoldierDie.Invoke(this);
        }
    }
}