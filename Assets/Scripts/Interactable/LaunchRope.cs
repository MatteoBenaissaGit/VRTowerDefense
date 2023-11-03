using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchRope : MonoBehaviour
{
    public ProjectileThrow ProjectileThrow;

    public void ThrowBall()
    {
        ProjectileThrow.ThrowObject();
    } 
}
