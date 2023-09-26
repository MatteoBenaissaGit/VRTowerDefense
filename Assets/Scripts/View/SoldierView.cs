using System;
using Controllers;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace View
{
    public class SoldierView : MonoBehaviour
    {
        public SoldierController Controller { get; private set; }

        public void Initialize(SoldierController controller)
        {
            Controller = controller;
        }
        
        public void SpawnAnimation()
        {
            Controller.transform.localScale = Vector3.zero;
            float spawnTime = RessourceManager.Instance.TroopsData.GetSoldierData(Controller.GameplayData.Type).SpawnTime;
            Controller.transform.DOScale(Vector3.one, spawnTime);

            Controller.OnStateChanged += LaunchAnimation;
        }

        private void Update()
        {
            switch (Controller.CurrentState)
            {
                case SoldierStateEnum.None:
                    break;
                case SoldierStateEnum.Spawn:
                    break;
                case SoldierStateEnum.Walk:
                    UpdateWalk();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void LaunchAnimation(SoldierStateEnum state)
        {
            switch (state)
            {
                case SoldierStateEnum.None:
                    break;
                case SoldierStateEnum.Spawn:
                    break;
                case SoldierStateEnum.Walk:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void UpdateWalk()
        {
            Vector3 modifiedTargetPos = new Vector3(Controller.GameplayData.TargetPosition.x, transform.position.y, Controller.GameplayData.TargetPosition.z);
            transform.LookAt(modifiedTargetPos);
        }
    }
}