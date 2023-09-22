using UnityEngine;

namespace Data.Base
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Base", order = 1)]
    public class BaseData : ScriptableObject
    {
        [field:SerializeField] public int Life { get; private set; } 
    }
}