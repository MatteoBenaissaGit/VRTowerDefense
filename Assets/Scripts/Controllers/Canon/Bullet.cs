using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _timeBeforeDestroy;

    private void Start()
    {
        StartCoroutine(Destroy());
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_timeBeforeDestroy);
        Destroy(gameObject);
    }
}