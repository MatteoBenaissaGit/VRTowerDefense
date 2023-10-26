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
            SoldierType = type;
            Number = number;
            Path = path;
            User = user;
        }
        
        public SoldierType SoldierType { get; private set; }
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
        public float TroopPercentage { get; private set; }
        
        private TroopGameplayData _gameplayData;
        private List<SoldierController> _soldiers = new List<SoldierController>();

        public void SpawnTroops(TroopGameplayData gameplayData)
        {
            //data and fields
            _gameplayData = gameplayData;
            
            //events
            OnSoldierDie += CheckTroopPercentage;

            transform.position = Vector3.zero;
            
            PathUserManager userPath = new PathUserManager(gameplayData.Path, _gameplayData.User);

            //spawns
            for (int i = 0; i < _gameplayData.Number; i++)
            {
                SoldierController soldierPrefab = null;
                switch (gameplayData.SoldierType)
                {
                    case SoldierType.SimpleCac:
                        soldierPrefab = gameplayData.User == UserType.Player ? 
                            RessourceManager.Instance.PlayerCacSoldier : RessourceManager.Instance.EnemyCacSoldier;
                        break;
                    case SoldierType.SimpleDistance:
                        soldierPrefab = gameplayData.User == UserType.Player ? 
                            RessourceManager.Instance.PlayerDistanceSoldier : RessourceManager.Instance.EnemyDistanceSoldier;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                SoldierController soldier = Instantiate(soldierPrefab, transform, true);
                SoldierData data = RessourceManager.Instance.TroopsData.GetSoldierData(_gameplayData.SoldierType);
                if (data == null)
                {
                    throw new Exception($"no data for this troop : {_gameplayData.SoldierType.ToString()}");
                }
                soldier.transform.name = $"Soldier {data.Type} {i}";

                Vector3 spawnPoint = userPath.GetSpawnPoint(_gameplayData.User);
                Vector3 baseSpawnPoint = spawnPoint;

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
                
                soldier.SetSoldier(data, this, gameplayData.Path, baseSpawnPoint - spawnPoint, _gameplayData.User);

                _soldiers.Add(soldier);
            }
        }
        
        private void CheckTroopPercentage(SoldierController soldier)
        {
            _soldiers.Remove(soldier);
            TroopPercentage = (float)_soldiers.Count / (float)_gameplayData.Number;
        }
    }
}