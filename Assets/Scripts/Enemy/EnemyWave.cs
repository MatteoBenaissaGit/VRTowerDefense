using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyWave : MonoBehaviour
    {
        public List<global::Enemy.Enemy> enemies;
        public float spawnDelay;

        public EnemyWave()
        {
            enemies = new List<global::Enemy.Enemy>();
            spawnDelay = 2f;
        }
    }
}