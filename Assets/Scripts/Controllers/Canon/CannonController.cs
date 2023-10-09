using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private GameObject xRoll;
    [SerializeField] private GameObject yRoll;

    public float BlastPower = 5;

    public Transform ShotPoint;
    

    private void Update()
    {
        transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            xRoll.transform.rotation.eulerAngles.x, yRoll.transform.rotation.eulerAngles.x + 45);
    }
}