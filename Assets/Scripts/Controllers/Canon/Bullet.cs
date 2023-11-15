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
    [SerializeField] private Collider _colliderCollide;

    public LayerMask LayerMask;

    private List<SoldierController> _soldiersTouched = new List<SoldierController>();
    private float _timerSafe = 1f;

    private void Awake()
    {
        _timerSafe = 1f;
        _colliderCollide.isTrigger = true;
    }

    private void Start()
    {
        StartCoroutine(Destroy());
    }

    private void Update()
    {
        _timerSafe -= Time.deltaTime;
        if (_timerSafe < 0)
        {
            _colliderCollide.isTrigger = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Explode();
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

        Explode();
    }

    private void Explode()
    {
        if (_timerSafe > 0)
        {
            return;
        }
        
        _soldiersTouched.ForEach(x => x?.SetLife(-1000));
        _explosionParticle.transform.parent = null;
        _explosionParticle.transform.rotation = Quaternion.Euler(Vector3.zero);
        _explosionParticle.Play();
        Destroy(gameObject);
    }
}