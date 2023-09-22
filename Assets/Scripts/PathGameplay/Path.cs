using System;
using System.Collections.Generic;
using UnityEngine;

namespace PathGameplay
{
    public class Path : MonoBehaviour
    {
        [field: SerializeField] public List<Vector3> PathPoints { get; private set; } = new List<Vector3>();

        public UnityEngine.Vector3 GetFirstPoint()
        {
            if (PathPoints.Count <= 0)
            {
                throw new Exception("no points in path");
            }
            return PathPoints[0];
        }

        public UnityEngine.Vector3 GetLastPoint()
        {
            if (PathPoints.Count <= 0)
            {
                throw new Exception("no points in path");
            }
            return PathPoints[^1];
        }
    }
}