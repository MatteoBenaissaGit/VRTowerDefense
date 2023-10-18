using System;
using UnityEngine;

public class Pion : MonoBehaviour
{
    public LayerMask LayerMask;
    public bool CanBeDestroy;

    private void OnCollisionEnter(Collision collision)
    {
        if ((LayerMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            Destroy(gameObject);
        }
    }
}