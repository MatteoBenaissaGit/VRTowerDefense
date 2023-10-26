using Managers;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Interactable
{
    public class Socle : MonoBehaviour
    {
        public XRSocketInteractor SocketInteractor;
        public int SocleIndex;
    
        private void Start()
        {
            SocketInteractor = GetComponent<XRSocketInteractor>();
        }

        public void OnSocketSelected(SelectEnterEventArgs args)
        {
            if (args.interactable != null)
            {
                GameObject placedObject = args.interactable.gameObject;

                Pion actualPionPlaced = placedObject.GetComponent<Pion>();

                if (actualPionPlaced.IsPlaced == false)
                {
                    actualPionPlaced.IsPlaced = true;
                    actualPionPlaced.LaunchCoroutine();
                    PionManager.Instance.OnPawnPlaced.Invoke(SocleIndex, actualPionPlaced);
                }
            }
        }
    }
}
