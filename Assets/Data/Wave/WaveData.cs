using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Wave", order = 2)]
public class WaveData : ScriptableObject
{
    [Header("Timer Waves")]
    public float TimeBetweenWaves;
    public float ActualTimeBetweenWaves;
    public bool CanSpawnTroop;

    public List<Wave> Waves;
}
