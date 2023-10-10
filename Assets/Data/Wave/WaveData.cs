using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Wave", order = 2)]
public class WaveData : ScriptableObject
{
    [FormerlySerializedAs("ActualTimeBetweenWave")] [Header("Timer Waves")]
    public float TimeBetweenWave;
    public bool CanSpawnTroop;

    public List<Wave> Waves;
}
