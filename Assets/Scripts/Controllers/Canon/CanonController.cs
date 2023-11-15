using UnityEngine;

namespace Controllers.Canon
{
    public class CanonController : MonoBehaviour
    {   
        [SerializeField] private GameObject _xRoll;
        [SerializeField] private GameObject _yRoll;
        [SerializeField] private float _multiplierX = 1;
        [SerializeField] private float _multiplierY = 1;

        private float _baseMinCanonZ = 55;
        private float _maxSpringCanonZ = 20;
        
        private void Update()
        {
            float yAngle = _xRoll.transform.localRotation.eulerAngles.x;
            if (yAngle > 180)
            {
                yAngle = 360 - ((360 - yAngle) * _multiplierX);
            }
            else
            {
                yAngle *= _multiplierX;
            }
            
            float baseZAngle = _yRoll.transform.localRotation.eulerAngles.x > 20
                ? _yRoll.transform.localRotation.eulerAngles.x - 21
                : _yRoll.transform.localRotation.eulerAngles.x;
            float zAngle = _baseMinCanonZ + (baseZAngle * _multiplierY);

            transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, yAngle, zAngle);
        }
    }
}