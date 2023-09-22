using Data.Troops;
using UnityEngine;

namespace Controllers
{
    public abstract class EntityGameplayData
    {
        public EntityGameplayData(EntityData data)
        {
            Life = data.Life;
            Damage = data.Damage;
            Speed = data.Speed;
        }

        public int Life;
        public int Damage;
        public float Speed;
    }
    
    public abstract class EntityController<TClass> : MonoBehaviour where TClass : EntityGameplayData
    {
        protected TClass GameplayData;

        public virtual void TakeDamage(int value)
        {
            GameplayData.Life += value;

            if (GameplayData.Life <= 0)
            {
                Die();
            }
        }
        
        public virtual void Die()
        {
            
        }
    }
}