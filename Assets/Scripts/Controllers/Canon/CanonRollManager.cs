using System;
using UnityEngine;

public class CanonRollManager : MonoBehaviour
{
    [SerializeField] private GameObject xRoll;
    [SerializeField] private GameObject yRoll;

    [SerializeField] private float _rollOffset;

    void Update()
    {
        transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x , yRoll.transform.rotation.eulerAngles.x, xRoll.transform.rotation.eulerAngles.x);
    }
}