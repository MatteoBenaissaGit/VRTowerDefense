using System;
using System.Collections.Generic;
using Controllers;
using Data.Base;
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
    
        //private fields
        private TroopSpawnerManager _troopSpawner;
        private List<TroopController> _troops = new List<TroopController>();

        private void Awake()
        {
            GameplayData = new BaseGameplayData(this);
            _troopSpawner = new TroopSpawnerManager(User);
        }

        private void Start()
        {
            _troops.Add(_troopSpawner.SpawnTroopAtPath(0, 18, SoldierType.SimpleCac));
        }
    }
}