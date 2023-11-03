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
        public float DetectionRange;
        public float AttackRange;
        public float AttackSpeed;
    }
    
    [Serializable]
    public class SoldierData : EntityData
    {
        public SoldierType Type;
        public float SpawnTime;
        public float SpawnOffset;
        public float DistanceToPathPointToSetReached;
    }
    
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Troops", order = 2)]
    public class TroopsData : ScriptableObject
    {
        [field: SerializeField] public List<SoldierData> SoldiersData { get; private set; }

        private Dictionary<SoldierType, SoldierData> _dictionarySoldiersData = new Dictionary<SoldierType, SoldierData>();

        public void Initialize()
        {
            foreach (SoldierData soldier in SoldiersData)
            {
                if (_dictionarySoldiersData.ContainsKey(soldier.Type))
                {
                    continue;
                }
                _dictionarySoldiersData.Add(soldier.Type, soldier);
            }
        }

        public SoldierData GetSoldierData(SoldierType type)
        {
            if (_dictionarySoldiersData.ContainsKey(type) == false)
            {
                throw new Exception("no data for soldier");
            }

            return _dictionarySoldiersData[type];
        }
    }
}