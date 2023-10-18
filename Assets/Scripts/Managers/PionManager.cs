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
}