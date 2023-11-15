using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controllers.Canon
{
    public class CanonLeverController : MonoBehaviour
    {
        [SerializeField] private XRGrabInteractable _grab;
        [SerializeField] private Rigidbody _rigidbody;

        private void Update()
        {
            _rigidbody.isKinematic = _grab.isSelected == false;
        }
    }
}