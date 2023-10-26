using System.Collections;
using Controllers;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Interactable
{
    public class Pion : MonoBehaviour
    {
        public LayerMask DestroyingLayer;
        public bool CanBeDestroy;
    
        public SoldierType Type;
        public int NumberOfTroopsToSpawn;

        public bool IsPlaced;
    
        private void OnCollisionEnter(Collision collision)
        {
            if ((DestroyingLayer.value & (1 << collision.gameObject.layer)) != 0)
            {
                Destroy(gameObject);
            }
        }
    
        public void LaunchCoroutine()
        {
            StartCoroutine(WaitingForPosition());
        }
    
        IEnumerator WaitingForPosition()
        {
            yield return new WaitForSeconds(0.3f);
            GetComponent<XRGrabInteractable>().enabled = false;
        }

        public void DestroyPawn()
        {
            Destroy(gameObject);
        }
    }
}