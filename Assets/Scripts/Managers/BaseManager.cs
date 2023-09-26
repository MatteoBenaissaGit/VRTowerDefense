using System;
using System.Collections.Generic;
using Controllers;
using Data.Base;
using Enemy;
using PathGameplay;
using UnityEngine;

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
        public TroopSpawnerManager _troopSpawner;
    
        //private fields
        private List<TroopController> _troops = new List<TroopController>();

        private WaveManager _waveManager;

        private void Awake()
        {
            GameplayData = new BaseGameplayData(this);
            _troopSpawner = new TroopSpawnerManager(User);
        }
    }
}