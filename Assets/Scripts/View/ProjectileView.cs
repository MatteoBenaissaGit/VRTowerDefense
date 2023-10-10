using System;
using DG.Tweening;
using UnityEngine;

namespace View
{
    public class ProjectileView : MonoBehaviour
    {
        private Vector3 _target;
        
        public void SetTarget(Vector3 target)
        {
            _target = target;
            transform.LookAt(_target);
            transform.DOMove(_target, 0.5f).SetEase(Ease.Flash).OnComplete(() => Destroy(gameObject));
        }
    }
}