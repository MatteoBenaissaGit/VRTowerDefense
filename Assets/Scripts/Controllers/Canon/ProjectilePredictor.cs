using System.Collections.Generic;
using UnityEngine;

namespace Controllers.Canon
{
    [RequireComponent(typeof(LineRenderer))]
    public class TrajectoryPredictor : MonoBehaviour
    {

        [SerializeField, Tooltip("Le marker de détection d'objet")] private Transform _hitMarker;
        [SerializeField, Range(10, 200), Tooltip("La distance max du LineRenderer")] private int _maxPoints = 200;
        [SerializeField] private float _offsetYTarget;
        [SerializeField] private List<Collider> _collidersToIgnore = new List<Collider>();
         
        private LineRenderer _trajectoryLine;
        private float _increment = 0.025f;
        private float _rayOverlap = 1.1f;


        private void Start()
        {
            if (_trajectoryLine == null)
                _trajectoryLine = GetComponent<LineRenderer>();

            SetTrajectoryVisible(true);
        }

        public void PredictTrajectory(ProjectileProperties projectile)
        {
            Vector3 velocity = projectile.direction * (projectile.initialSpeed / projectile.mass);
            Vector3 position = projectile.initialPosition;

            UpdateLineRender(_maxPoints, (0, position));

            for (int i = 1; i < _maxPoints; i++)
            {
                velocity = CalculateNewVelocity(velocity, projectile.drag, _increment);
                Vector3 nextPosition = position + velocity * _increment;

                float overlap = Vector3.Distance(position, nextPosition) * _rayOverlap;

                if (Physics.Raycast(position, velocity.normalized, out RaycastHit hit, overlap))
                {
                    if (hit.collider == null)
                    {
                        continue;
                    }

                    UpdateLineRender(i, (i - 1, hit.point));
                    MoveHitMarker(hit);
                    break;
                }

                _hitMarker.gameObject.SetActive(false);
                position = nextPosition;
                UpdateLineRender(_maxPoints, (i, position));
            }
        }

        private void UpdateLineRender(int count, (int point, Vector3 pos) pointPos)
        {
            _trajectoryLine.positionCount = count;
            _trajectoryLine.SetPosition(pointPos.point, pointPos.pos);
        }

        private Vector3 CalculateNewVelocity(Vector3 velocity, float drag, float increment)
        {
            velocity += Physics.gravity * increment;
            velocity *= Mathf.Clamp01(1f - drag * increment);
            return velocity;
        }

        private void MoveHitMarker(RaycastHit hit)
        {
            _hitMarker.gameObject.SetActive(true);
            float offset = 1f;

            _hitMarker.position = new Vector3(hit.point.x, hit.point.y + _offsetYTarget, hit.point.z);
            // hitMarker.rotation = Quaternion.LookRotation(hit.normal, Vector3.up);
        }

        public void SetTrajectoryVisible(bool visible)
        {
            _trajectoryLine.enabled = visible;
            _hitMarker.gameObject.SetActive(visible);
        }
    }
}