using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _timeBeforeDestroy;

    public LayerMask LayerMask;

    private List<SoldierController> _soldiersTouched = new List<SoldierController>();
    
    private void Start()
    {
        StartCoroutine(Destroy());
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.layer & LayerMask) == LayerMask)
        {
            _soldiersTouched.ForEach(x=>x.SetLife(-1000));
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SoldierController soldier = other.GetComponent<SoldierController>();
        if (soldier == null)
        {
            soldier = other.GetComponentInChildren<SoldierController>();
        }
        
        if (soldier != null)
        {
            _soldiersTouched.Add(soldier);
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_timeBeforeDestroy);
        Destroy(gameObject);
    }
}