using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{   
    // [SerializeField] private GameObject CannonSupport;
    // [SerializeField] private GameObject Cannon;
    
    [SerializeField] private GameObject xRoll;
    [SerializeField] private GameObject yRoll;
    
    private void Update()
    {
        transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            xRoll.transform.rotation.eulerAngles.x, yRoll.transform.rotation.eulerAngles.x + 45);
    }
}