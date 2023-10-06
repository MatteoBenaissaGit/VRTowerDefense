using System;
using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class BaseView : MonoBehaviour
    {
        [SerializeField] private BaseManager _manager;
        [SerializeField] private Transform _lifeBarTransform;
        [SerializeField] private Image _lifeBar;

        private void Awake()
        {
            _manager.OnBaseTakeDamage += UpdateLifeBar;
        }

        private void Update()
        {
            _lifeBarTransform.LookAt(Camera.main.transform.position);
        }

        private void UpdateLifeBar()
        {
            _lifeBar.DOFillAmount((float)_manager.GameplayData.Life / _manager.Data.Life, 0.25f);
        }
    }
}
