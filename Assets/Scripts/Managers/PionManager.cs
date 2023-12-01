using System;
using System.Collections;
using Controllers;
using DG.Tweening;
using Interactable;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
        
        public AudioSource PawnAudioSourceLaunch;
        
        public Image LoadPawnArcher, LoadPawnChevalier;

        public Action<int,Pion> OnPawnPlaced;

        private Vector3 _chevalierPawnScale, _archerPawnScale;
    
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

            LoadPawnArcher.fillAmount = 0;
            LoadPawnChevalier.fillAmount = 0;

            _chevalierPawnScale = CurrentCacPawn.transform.localScale;
            _archerPawnScale = CurrentDistancePawn.transform.localScale;

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
                LoadPawnChevalier.fillAmount = 0;
                GameObject newPawn = Instantiate(CacPawnPrefab, _originCacPawnPosition, Quaternion.identity);
                newPawn.transform.localScale = Vector3.Scale(_chevalierPawnScale, transform.localScale);
                CurrentCacPawn = newPawn.GetComponent<Pion>();
                _originCacPawnPosition = CurrentCacPawn.transform.position;
            }
            
            if (CurrentDistancePawn == null)
            {
                LoadPawnArcher.fillAmount = 0;
                GameObject newPawn = Instantiate(DistancePawnPrefab, _originDistancePawnPosition, Quaternion.identity);
                newPawn.transform.localScale = Vector3.Scale(_archerPawnScale, transform.localScale);
                CurrentDistancePawn = newPawn.GetComponent<Pion>();
                _originDistancePawnPosition = CurrentDistancePawn.transform.position;
            }
        }

        private const float RespawnTime = 6f;
        
        public void LaunchOnPawnPlaced(int indexSocle, Pion pion)
        {
            Debug.Log($"Pion posé sur socle {indexSocle}");

            if (pion.Type == SoldierType.SimpleCac)
            {
                LoadPawnChevalier.DOFillAmount(1, RespawnTime).SetEase(Ease.Flash);
            }
            else if (pion.Type == SoldierType.SimpleDistance)
            {
                LoadPawnArcher.DOFillAmount(1, RespawnTime).SetEase(Ease.Flash);
            }
            
            PawnAudioSourceLaunch.Play();
            GameManager.Instance.PlayerBase.TroopSpawner.SpawnTroopAtPath(indexSocle, pion.NumberOfTroopsToSpawn, pion.Type);
            StartCoroutine(DestroyPawn(pion));
        }

        public IEnumerator DestroyPawn(Pion pion)
        {
            float timeToDestroyInSeconds = RespawnTime;
            yield return new WaitForSeconds(timeToDestroyInSeconds);
            Destroy(pion.transform.gameObject);
        }
    }
}