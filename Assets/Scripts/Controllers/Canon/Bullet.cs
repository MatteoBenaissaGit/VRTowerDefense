using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _timeBeforeDestroy;

    public LayerMask LayerMask;

    private void Start()
    {
        StartCoroutine(Destroy());
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.layer & LayerMask) == LayerMask)
            Destroy(gameObject);
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_timeBeforeDestroy);
        Destroy(gameObject);
    }
}