using System.Collections.Generic;
using Controllers;
using Data.Troops;
using MatteoBenaissaLibrary.SingletonClassBase;
using UnityEngine;
using View;

namespace Managers
{
    public class RessourceManager : Singleton<RessourceManager>
    {
        [field:SerializeField] public TroopsData TroopsData { get; private set; } 
        [field:SerializeField] public List<SoldierController> SoldierPrefabs { get; private set; } //list them in the same order as the SoldierTypeEnum
        [field:SerializeField] public TroopController TroopPrefab { get; private set; } //list them in the same order as the SoldierTypeEnum
        [field:SerializeField] public GameObject ArrowPrefab { get; private set; } //list them in the same order as the SoldierTypeEnum

        protected override void Awake()
        {
            base.Awake();
            TroopsData.Initialize();
        }

        public TroopController InstantiateTroop()
        {
            return Instantiate(TroopPrefab);
        }

        public GameObject InstantiateObject(GameObject o)
        {
            return Instantiate(o);
        }
    }
}