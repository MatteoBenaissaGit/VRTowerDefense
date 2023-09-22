using System.Collections.Generic;
using MatteoBenaissaLibrary.SingletonClassBase;
using PathGameplay;
using UnityEngine;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [field: SerializeField] public BaseManager PlayerBase { get; private set; }
        [field: SerializeField] public BaseManager EnemyBase { get; private set; }
        [field: SerializeField] public List<Path> Paths { get; private set; } = new List<Path>();
    }
}