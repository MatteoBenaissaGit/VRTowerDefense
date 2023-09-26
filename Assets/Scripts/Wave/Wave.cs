using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Wave : MonoBehaviour
    {
        public List<global::Enemy.Enemy> enemies;
        public float spawnDelay;

        public Wave()
        {
            enemies = new List<global::Enemy.Enemy>();
            spawnDelay = 2f;
        }
    }
}