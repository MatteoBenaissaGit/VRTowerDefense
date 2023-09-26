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
        
        
        #if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < PathPoints.Count-1; i++)
            {
                Gizmos.DrawSphere(PathPoints[i],0.5f);
                Gizmos.DrawLine(PathPoints[i],PathPoints[i+1]);
            }
            Gizmos.DrawSphere(PathPoints[^1],0.2f);
        }

#endif
    }
}