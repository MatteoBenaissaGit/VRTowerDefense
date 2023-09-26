using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Managers;
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
        public BaseManager BaseManager;
        public WaveData WaveData;
        private int _currentWaveIndex = 0;

        public Action OnLauchFirstWave;
        [SerializeField] private bool _startLaunch;
        
        private void Awake()
        {
            OnLauchFirstWave += Lauch;
        }

        private void Lauch()
        {
            _startLaunch = true;
        }
        
        private void Start()
        {
            WaveData.CanSpawnTroop = true;
        }

        private void Update()
        {
            if (WaveData.CanSpawnTroop == false)
                return;

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

                BaseManager._troopSpawner.SpawnTroopAtPath(currentWave.PathNumber, currentWave.NumberOfTroops,
                    currentWave.SoldierType);

                WaveData.ActualTimeBetweenWave = WaveData.Waves[_currentWaveIndex].TimeBeforeNextWaves;
                _currentWaveIndex++;
                WaveData.CanSpawnTroop = true;
            }
        }
    }
}