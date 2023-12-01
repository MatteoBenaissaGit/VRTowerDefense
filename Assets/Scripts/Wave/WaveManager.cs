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
        [field: SerializeField] public BaseManager BaseManager { get; private set; }
        [field: SerializeField] public WaveData WaveData { get; private set; }

        private float _actualTimeBetweenWaves;

        public Action OnLaunchFirstWave { get; set; }

        public bool StartLaunch;

        private int _currentWaveIndex = 0;

        private void Awake()
        {
            StartLaunch = false;
            OnLaunchFirstWave += Launch;
        }

        private void Start()
        {
            if (StartLaunch)
            {
                OnLaunchFirstWave.Invoke();
            }
        }

        private void Launch()
        {
            WaveData.CanSpawnTroop = true;
            _actualTimeBetweenWaves = WaveData.TimeBetweenWave;
            StartLaunch = true;
        }

        private void Update()
        {
            if (WaveData.CanSpawnTroop == false)
            {
                return;
            }

            if (WaveData.CanSpawnTroop && StartLaunch)
            {
                _actualTimeBetweenWaves -= Time.deltaTime;

                if (_actualTimeBetweenWaves <= 0)
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

                _actualTimeBetweenWaves = WaveData.Waves[_currentWaveIndex].TimeBeforeNextWaves;
                _currentWaveIndex++;
                WaveData.CanSpawnTroop = true;
            }
            else
            {
                _currentWaveIndex = 0;
                SpawnWave();
            }
        }

        private void OnDisable()
        {
            WaveData.TimeBetweenWave = 0;
        }
    }
}