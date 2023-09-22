using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyMovement))]
    public class Enemy : MonoBehaviour
    {
        public EnemyType Type;

        public int Life;
        public int Speed;

        private EnemyMovement _enemyMovement;

        private void Start()
        {
            _enemyMovement = GetComponent<EnemyMovement>();

            DefineEnemyType();
        }

        private void DefineEnemyType()
        {
            switch (Type)
            {
                case EnemyType.Walker:
                    Life = 2;
                    Speed = 4;
                    break;
                case EnemyType.Runner:
                    Life = 1;
                    Speed = 6;
                    break;
                case EnemyType.Tank:
                    Life = 3;
                    Speed = 2;
                    break;
            }
        }

        private void Update()
        {
            _enemyMovement.moveSpeed = Speed;

            if (Life <= 0)
                Destroy(gameObject);
        }

        public void TakeDamage(int damage)
        {
            Life -= damage;
        }
    }
}