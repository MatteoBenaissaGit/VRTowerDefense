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
            DetectionRange = data.DetectionRange;
            AttackRange = data.AttackRange;
            AttackSpeed = data.AttackSpeed;
        }

        public int Life;
        public int Damage;
        public float Speed;
        public float DetectionRange;
        public float AttackRange;
        public float AttackSpeed;
    }
    
    public abstract class EntityController<TClass> : MonoBehaviour where TClass : EntityGameplayData
    {
        public TClass GameplayData { get; protected set; }

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