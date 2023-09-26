using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        public List<Transform> waypoints;

        [HideInInspector] public float moveSpeed;

        [SerializeField] private int currentWaypointIndex = 0;

        private void Start()
        {
            waypoints = EnemyManager.Instance.Waypoints;

            if (waypoints.Count > 0)
            {
                transform.position = waypoints[0].position;
            }
        }

        private void Update()
        {
            if (currentWaypointIndex >= waypoints.Count)
            {
                Destroy(gameObject);
                return;
            }

            MoveToWaypoint(waypoints[currentWaypointIndex]);
        }

        private void MoveToWaypoint(Transform waypoint)
        {
            Vector3 direction = (waypoint.position - transform.position).normalized;

            transform.Translate(direction * (moveSpeed * Time.deltaTime));

            if (Vector3.Distance(transform.position, waypoint.position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
    }
}