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
        private List<SoldierController> _soldiers = new List<SoldierController>();
        private float _troopsPercentage;

        public void SpawnTroops(TroopGameplayData gameplayData)
        {
            //data and fields
            _gameplayData = gameplayData;
            
            //events
            OnSoldierDie += CheckTroopPercentage;

            transform.position = Vector3.zero;
            //spawns
            for (int i = 0; i < _gameplayData.Number; i++)
            {
                SoldierController soldier = Instantiate(RessourceManager.Instance.SoldierPrefabs[(int)_gameplayData.Soldier], transform, true);
                SoldierData data = RessourceManager.Instance.TroopsData.GetSoldierData(_gameplayData.Soldier);
                if (data == null)
                {
                    throw new Exception($"no data for this troop : {_gameplayData.Soldier.ToString()}");
                }
                soldier.SetSoldier(data, this, gameplayData.Path);
                soldier.transform.name = $"Soldier {data.Type} {i}";

                Vector3 spawnPoint = soldier.PathManager.GetSpawnPoint(_gameplayData.User);
                Debug.Log(spawnPoint);

                switch (i)
                {
                    case <= 0:
                        break;
                    case > 0 and <= 5:
                        spawnPoint = MathTools.GetPointInCircleFromCenter(spawnPoint, i * (360f / 5f), data.SpawnOffset);
                        break;
                    case > 5 and <= 18:
                        spawnPoint = MathTools.GetPointInCircleFromCenter(spawnPoint, (i - 5) * (360f / 12f), data.SpawnOffset * 2);
                        break;
                    case > 18:
                        Debug.LogError("not setup for more than 18 soldiers spawn");
                        break;
                }
                soldier.transform.position = spawnPoint;

            }
        }

        

        public void CheckTroopPercentage(SoldierController soldier)
        {
            _soldiers.Remove(soldier);
            _troopsPercentage = _soldiers.Count / (float)_gameplayData.Number;
        }
    }
}