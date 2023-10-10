using System;
using Managers;
using UnityEngine;
using View;

namespace Controllers.SoliderStates
{
    public class SoldierStateAttack : SoldierStateBase
    {
        private SoldierController _soldierToAttack;
        private BaseManager _baseToAttack;

        private float _attackCooldown;
        
        public SoldierStateAttack(SoldierController controller, SoldierController soldierToAttack = null, BaseManager baseToAttack = null) : base(controller)
        {
            _soldierToAttack = soldierToAttack;
            _baseToAttack = baseToAttack;

            _attackCooldown = controller.GameplayData.AttackSpeed;
        }

        public override void OnEnterState()
        {
        }

        public override void UpdateState()
        {
            //base
            if (_baseToAttack != null)
            {
                if (_baseToAttack.GameplayData.Life <= 0)
                {
                    Debug.Log("life at 0");
                    Controller.SetState(SoldierStateEnum.None);
                }

                if (Controller.IsInAttackRange(_baseToAttack.transform))
                {
                    _attackCooldown -= Time.deltaTime;
                    if (_attackCooldown <= 0)
                    {
                        Controller.Rigidbody.velocity = Vector3.zero;
                        _baseToAttack.SetLife(-Controller.GameplayData.Damage);
                        _attackCooldown = Controller.GameplayData.AttackSpeed;

                        LaunchAttackVisual(_baseToAttack.transform);
                    }
                }
                else
                {
                    _attackCooldown = Controller.GameplayData.AttackSpeed;
                    MoveToward(_baseToAttack.transform);
                }
                return;
            }
            
            //soldier
            if (_soldierToAttack == null 
                || _soldierToAttack.transform == null
                || _soldierToAttack.GameplayData.Life <= 0)
            {
                Controller.SetState(SoldierStateEnum.Walk);
                return;
            }
            if (Controller.IsInAttackRange(_soldierToAttack.transform))
            {
                _attackCooldown -= Time.deltaTime;
                if (_attackCooldown <= 0)
                {
                    Controller.Rigidbody.velocity = Vector3.zero;
                    _soldierToAttack.SetLife(-Controller.GameplayData.Damage);
                    _attackCooldown = Controller.GameplayData.AttackSpeed;
                    
                    LaunchAttackVisual(_soldierToAttack.transform);
                }
            }
            else
            {
                _attackCooldown = Controller.GameplayData.AttackSpeed;
                MoveToward(_soldierToAttack.transform);
            }
        }

        private void LaunchAttackVisual(Transform toAttack)
        {
            switch (Controller.GameplayData.Type)
            {
                case SoldierType.SimpleCac:
                    break;
                case SoldierType.SimpleDistance:
                    ProjectileView arrow = RessourceManager.Instance.InstantiateObject(RessourceManager.Instance.ArrowPrefab)
                        .GetComponent<ProjectileView>();
                    arrow.transform.position = Controller.transform.position;
                    arrow.SetTarget(toAttack.transform.position);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void OnExitState()
        {
        }
        
        private void MoveToward(Transform transform)
        {
            Vector3 position = Controller.transform.position;
            Vector3 targetPosition = transform.position;
            Controller.transform.position = Vector3.MoveTowards(position, targetPosition, (Controller.GameplayData.Speed) * Time.deltaTime);
        }
    }
}