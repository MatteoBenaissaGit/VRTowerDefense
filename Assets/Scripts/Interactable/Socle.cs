using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Socle : MonoBehaviour
{
    public XRSocketInteractor socketInteractor;
    public int SocleIndex;
    
    private void Start()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
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
                PionManager.Instance.OnPawnPlaced.Invoke(SocleIndex);
            }
        }
    }
}
