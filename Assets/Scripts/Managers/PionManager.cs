using System;
using System.Collections;
using Controllers;
using Interactable;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    public class PionManager : MonoBehaviour
    {
        public static PionManager Instance;

        private Vector3 _originCacPawnPosition;
        private Vector3 _originDistancePawnPosition;

        public GameObject CacPawnPrefab;
        public GameObject DistancePawnPrefab;

        public Pion CurrentCacPawn;
        public Pion CurrentDistancePawn;

        public Action<int,Pion> OnPawnPlaced;
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("There is already another PionManager in this scene !");
            }

            OnPawnPlaced += LaunchOnPawnPlaced;
        }

        private void Start()
        {
            _originCacPawnPosition = CurrentCacPawn.transform.position;
            _originDistancePawnPosition = CurrentDistancePawn.transform.position;
        }

        private void Update()
        {
            if (CurrentCacPawn == null)
            {
                GameObject newPion = Instantiate(CacPawnPrefab, _originCacPawnPosition, Quaternion.identity);
                CurrentCacPawn = newPion.GetComponent<Pion>();
                _originCacPawnPosition = CurrentCacPawn.transform.position;
            }
            
            if (CurrentDistancePawn == null)
            {
                GameObject newPion = Instantiate(DistancePawnPrefab, _originDistancePawnPosition, Quaternion.identity);
                CurrentDistancePawn = newPion.GetComponent<Pion>();
                _originDistancePawnPosition = CurrentDistancePawn.transform.position;
            }
        }

        public void LaunchOnPawnPlaced(int indexSocle, Pion pion)
        {
            Debug.Log($"Pion posé sur socle {indexSocle}");
        
            GameManager.Instance.PlayerBase.TroopSpawner.SpawnTroopAtPath(indexSocle, pion.NumberOfTroopsToSpawn, pion.Type);
            StartCoroutine(DestroyPawn(pion));
        }

        public IEnumerator DestroyPawn(Pion pion)
        {
            float timeToDestroyInSeconds = 6f;
            yield return new WaitForSeconds(timeToDestroyInSeconds);
            Destroy(pion.transform.gameObject);
        }
    }
}