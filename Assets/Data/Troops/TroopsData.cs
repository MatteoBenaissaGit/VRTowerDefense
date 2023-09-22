using System;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace Data.Troops
{
    public abstract class EntityData
    {
        public string EntityName;
        public int Life;
        public int Damage;
        public float Speed;
    }
    
    [Serializable]
    public class SoldierData : EntityData
    {
        public SoldierType Type;
    }
    
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Troops", order = 2)]
    public class TroopsData : ScriptableObject
    {
        [field: SerializeField] public List<SoldierData> SoldiersData { get; private set; }
    }
}