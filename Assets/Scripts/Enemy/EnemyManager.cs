using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public enum EnemyType
    {
        Walker,
        Runner,
        Tank
    }

    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance;

        public List<Transform> Waypoints;

        public List<global::Enemy.Enemy> AllEnemies;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < Waypoints.Count - 1; i++)
            {
                Gizmos.DrawLine(Waypoints[i].transform.position, Waypoints[i + 1].transform.position);
            }
        }
    }
}