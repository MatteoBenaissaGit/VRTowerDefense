using System;
using Controllers;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace View
{
    public class SoldierView : MonoBehaviour
    {
        [field:SerializeField] public Animator Animator { get; private set; }
        public SoldierController Controller { get; private set; }

        public void Initialize(SoldierController controller)
        {
            Controller = controller;
        }
        
        public void SpawnAnimation()
        {
            Vector3 scale = Controller.transform.localScale;
            Controller.transform.localScale = Vector3.zero;
            float spawnTime = RessourceManager.Instance.TroopsData.GetSoldierData(Controller.GameplayData.Type).SpawnTime;
            Controller.transform.DOScale(scale, spawnTime);

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
                    Debug.Log("walk view");
                    UpdateWalk();
                    break;
                case SoldierStateEnum.AttackBase:
                    UpdateAttack();
                    break;
                case SoldierStateEnum.AttackSoldier:
                    Debug.Log("attack view");
                    UpdateAttack();
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
        
        private void UpdateAttack()
        {
            if (Controller.AttackTarget == null)
            {
                return;
            }
            
            Vector3 modifiedTargetPos = new Vector3(Controller.AttackTarget.position.x, transform.position.y, Controller.AttackTarget.position.z);
            transform.LookAt(modifiedTargetPos);
        }

        public void SetAnimation(string name, bool isTrigger, bool valueIfNotTrigger = true)
        {
            if (isTrigger)
            {
                Animator.SetTrigger(name);
                return;
            }

            Animator.SetBool(name, valueIfNotTrigger);
        }
    }
}