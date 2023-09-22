using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class WaveManager : MonoBehaviour
    {
        public static WaveManager Instance;

        public List<EnemyWave> waves;
        private int _currentWaveIndex = 0;
        public bool isActive;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("There is already one WaveManager in this scene !");
            }
        }

        private void Update()
        {
            if (isActive)
            {
                isActive = false;
                StartCoroutine(SpawnWave());
            }
        }

        private IEnumerator SpawnWave()
        {
            while (_currentWaveIndex < waves.Count)
            {
                EnemyWave currentWave = waves[_currentWaveIndex];
                foreach (global::Enemy.Enemy enemy in currentWave.enemies)
                {
                    SpawnEnemy(enemy);
                    yield return new WaitForSeconds(currentWave.spawnDelay);
                }

                _currentWaveIndex++;
                yield return new WaitForSeconds(5f);
            }
        }

        private void SpawnEnemy(global::Enemy.Enemy enemy)
        {
            var actualEnemy = Instantiate(enemy, transform.position, Quaternion.identity);

            EnemyManager.Instance.AllEnemies.Add(actualEnemy);

            actualEnemy.GetComponent<EnemyMovement>().waypoints = EnemyManager.Instance.Waypoints;
        }
    }
}