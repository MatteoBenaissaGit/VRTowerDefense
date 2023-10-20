using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PionManager : MonoBehaviour
{
    public static PionManager Instance;

    private Vector3 _originPosition;
    public GameObject PionPrefab;

    public Pion ActualPion;

    public Action<int> OnPawnPlaced;
    
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
        _originPosition = ActualPion.transform.position;
    }

    private void Update()
    {
        if (ActualPion == null)
        {
            GameObject newPion = Instantiate(PionPrefab, _originPosition, Quaternion.identity);
            ActualPion = newPion.GetComponent<Pion>();
            _originPosition = ActualPion.transform.position;
        }
    }

    public void LaunchOnPawnPlaced(int indexSocle)
    {
        Debug.Log($"Pion posé sur socle {indexSocle}");
        
    }
}