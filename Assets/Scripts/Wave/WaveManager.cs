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
    }

    public class WaveManager : MonoBehaviour
    {
        public BaseManager BaseManager;
        public WaveData WaveData;
        private int _currentWaveIndex = 0;

        private void Start()
        {
            WaveData.ActualTimeBetweenWaves = WaveData.TimeBetweenWaves;
            WaveData.CanSpawnTroop = true;
        }

        private void Update()
        {
            if (WaveData.CanSpawnTroop)
                WaveData.ActualTimeBetweenWaves -= Time.deltaTime;

            if (WaveData.ActualTimeBetweenWaves <= 0)
            {
                WaveData.ActualTimeBetweenWaves = WaveData.TimeBetweenWaves;
                WaveData.CanSpawnTroop = false;
                SpawnWave();
            }
        }

        private void SpawnWave()
        {
            if (_currentWaveIndex < WaveData.Waves.Count)
            {
                Wave currentWave = WaveData.Waves[_currentWaveIndex];

                BaseManager._troopSpawner.SpawnTroopAtPath(currentWave.PathNumber, currentWave.NumberOfTroops,
                    currentWave.SoldierType);

                _currentWaveIndex++;
                WaveData.CanSpawnTroop = true;
            }
        }
    }
}