using System;
using System.Collections.Generic;
using Controllers;
using Data.Base;
using Data.Troops;
using Enemy;
using PathGameplay;
using UnityEngine;
using View;

namespace Managers
{
    public class BaseGameplayData
    {
        public BaseGameplayData(BaseManager manager)
        {
            Life = manager.Data.Life;
        }
    
        public int Life { get; set; }
    }

    public class BaseManager : MonoBehaviour
    {
        //editor fields
        [field: SerializeField] public BaseData Data { get; private set; }
        [field: SerializeField] public UserType User { get; private set; }
    
        //public fields
        public BaseGameplayData GameplayData { get; private set; }
        public BaseView View { get; private set; }
        public TroopSpawnerManager TroopSpawner  { get; private set; }
        public Action OnBaseTakeDamage  { get;  set; }
        public Action OnBaseDie { get;  set; }
    
        //private fields
        private List<TroopController> _troops = new List<TroopController>();

        private WaveManager _waveManager;

        private void Awake()
        {
            GameplayData = new BaseGameplayData(this);
            TroopSpawner = new TroopSpawnerManager(User);

            OnBaseDie += Die;
        }

        public void SetLife(int value)
        {
            GameplayData.Life += value;

            if (value < 0)
            {
                OnBaseTakeDamage.Invoke();
            }
            
            if (GameplayData.Life <= 0)
            {
                OnBaseDie.Invoke();
            }
        }

        private void Die()
        {
            Debug.Log("base died");
        }
    }
}