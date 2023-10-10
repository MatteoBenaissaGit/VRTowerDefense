using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Managers;
using MatteoBenaissaLibrary.Attributes.ReadOnly;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    [Serializable]
    public class Wave
    {
        public int PathNumber;
        public int NumberOfTroops;
        public SoldierType SoldierType;
        public float TimeBeforeNextWaves;
    }

    public class WaveManager : MonoBehaviour
    {
        [field:SerializeField] public BaseManager BaseManager { get; private set; }
        [field:SerializeField] public WaveData WaveData { get; private set; }
        
        public Action OnLaunchFirstWave { get; set; }

        [SerializeField] private bool _startLaunch;
        
        private int _currentWaveIndex = 0;
        
        private void Awake()
        {
            _startLaunch = false;
            OnLaunchFirstWave += Launch;
        }

        private void Start()
        {
            WaveData.CanSpawnTroop = true;
            OnLaunchFirstWave.Invoke();
        }
        
        private void Launch()
        {
            _startLaunch = true;
        }

        private void Update()
        {
            if (WaveData.CanSpawnTroop == false)
            {
                return;
            }

            if (WaveData.CanSpawnTroop && _startLaunch)
            {
                WaveData.ActualTimeBetweenWave -= Time.deltaTime;

                if (WaveData.ActualTimeBetweenWave <= 0)
                {
                    WaveData.CanSpawnTroop = false;
                    SpawnWave();
                }
            }
        }

        private void SpawnWave()
        {
            if (_currentWaveIndex < WaveData.Waves.Count)
            {
                Wave currentWave = WaveData.Waves[_currentWaveIndex];

                BaseManager.TroopSpawner.SpawnTroopAtPath(currentWave.PathNumber, currentWave.NumberOfTroops,
                    currentWave.SoldierType);

                WaveData.ActualTimeBetweenWave = WaveData.Waves[_currentWaveIndex].TimeBeforeNextWaves;
                _currentWaveIndex++;
                WaveData.CanSpawnTroop = true;
            }
        }

        private void OnDisable()
        {
            WaveData.ActualTimeBetweenWave = 0;
        }
    }
}