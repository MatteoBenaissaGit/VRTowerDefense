using System.Collections;
using System.Collections.Generic;
using Enemy;
using TMPro;
using UnityEngine;

public class LaunchRope : MonoBehaviour
{
    private bool _gameLaunched;
    public ProjectileThrow ProjectileThrow;
    public WaveManager _wave;
    public TMP_Text _ropeText;

    public void ThrowBall()
    {
        if (_gameLaunched == false)
        {
            _wave.StartLaunch = true;
            _wave.OnLaunchFirstWave.Invoke();
            _ropeText.text = "Pull to shoot";
            _ropeText.color = Color.white;
            _gameLaunched = true;
        }
        
        ProjectileThrow.ThrowObject();
    } 
}
