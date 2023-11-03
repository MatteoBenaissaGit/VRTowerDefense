using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _timeBeforeDestroy;
    [SerializeField] private ParticleSystem _explosionParticle;

    public LayerMask LayerMask;

    private List<SoldierController> _soldiersTouched = new List<SoldierController>();

    private void Start()
    {
        StartCoroutine(Destroy());
    }

    private void OnCollisionEnter(Collision other)
    {
        _soldiersTouched.ForEach(x => x.SetLife(-1000));
        _explosionParticle.Play();
        _explosionParticle.transform.parent = null;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        SoldierController soldier = other.GetComponent<SoldierController>();
        if (soldier == null)
        {
            soldier = other.GetComponentInChildren<SoldierController>();
        }

        if (soldier != null)
        {
            if (_soldiersTouched.Contains(soldier))
            {
                return;
            }

            Debug.Log("soldier");
            _soldiersTouched.Add(soldier);
        }
    }

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_timeBeforeDestroy);

        _soldiersTouched.ForEach(x => x.SetLife(-1000));
        _explosionParticle.transform.parent = null;
        _explosionParticle.transform.rotation = Quaternion.Euler(Vector3.zero);
        _explosionParticle.Play();
        Destroy(gameObject);
    }
}