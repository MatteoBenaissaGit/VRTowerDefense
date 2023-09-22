using System;
using System.Collections.Generic;
using Data.Troops;
using Managers;
using PathGameplay;
using UnityEngine;

namespace Controllers
{
    public class TroopGameplayData
    {
        public TroopGameplayData(SoldierType type, int number, UserType user, PathGameplay.Path path) //TODO add a position or path reference
        {
            Soldier = type;
            Number = number;
            Path = path;
            User = user;
        }
        
        public SoldierType Soldier { get; private set; }
        public int Number { get; private set; }
        public PathGameplay.Path Path { get; private set; }
        public UserType User { get; private set; }
    }
    
    /// <summary>
    /// Control a troop
    /// </summary>
    public class TroopController : MonoBehaviour
    {
        public Action<SoldierController> OnSoldierDie { get; set; }
        
        private TroopGameplayData _gameplayData;
        private PathUserManager _pathManager;
        private List<SoldierController> _soldiers = new List<SoldierController>();
        private float _troopsPercentage;

        public void SpawnTroops(TroopGameplayData gameplayData)
        {
            //data and fields
            _gameplayData = gameplayData;
            _pathManager = new PathUserManager(gameplayData.Path);
            
            //events
            OnSoldierDie += CheckTroopPercentage;
            
            //spawns
            for (int i = 0; i < _gameplayData.Number; i++)
            {
                SoldierController solider = Instantiate(RessourceManager.Instance.SoldierPrefabs[(int)_gameplayData.Soldier]);
                SoldierData data = RessourceManager.Instance.TroopsData.SoldiersData.Find(x => x.Type == _gameplayData.Soldier);
                if (data == null)
                {
                    throw new Exception($"no data for this troop : {_gameplayData.Soldier.ToString()}");
                }
                solider.SetSoldier(data, this);
                
                //TODO spawn troops in a circle or smth
                solider.transform.position = _pathManager.GetSpawnPoint(_gameplayData.User);
            }
        }

        public void CheckTroopPercentage(SoldierController soldier)
        {
            _soldiers.Remove(soldier);
            _troopsPercentage = _soldiers.Count / (float)_gameplayData.Number;
        }
    }
}