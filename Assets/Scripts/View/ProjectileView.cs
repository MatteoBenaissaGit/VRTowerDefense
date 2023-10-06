using System;
using UnityEngine;

namespace View
{
    public class ProjectileView : MonoBehaviour
    {
        private Vector3 _target;
        
        public void SetTarget(Vector3 target)
        {
            _target = target;
        }

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _target, 0.01f);
            transform.LookAt(_target);
            if (Vector3.Distance(transform.position, _target) < 0.2f)
            {
                Destroy(gameObject);
            }
        }
    }
}