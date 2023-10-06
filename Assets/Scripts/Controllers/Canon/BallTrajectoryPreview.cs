using UnityEngine;
using UnityEngine.Serialization;

public class BallTrajectoryPreview : MonoBehaviour
{
    [SerializeField] private CannonController _cannonController;
    [SerializeField] private LineRenderer _trajectoryLineRenderer;
    public Transform cannonBarrel;
    public int numberOfPoints = 100;
    public float predictionTime = 2f;

    private void Start()
    {
        _trajectoryLineRenderer.positionCount = numberOfPoints;
        _trajectoryLineRenderer.enabled = true;
    }

    private void Update()
    {
        ShowTrajectory();
    }

    void ShowTrajectory()
    {
        Vector3[] points = new Vector3[numberOfPoints];

        float timeStep = predictionTime / numberOfPoints;
        
        Vector3 velocity = Quaternion.Euler(cannonBarrel.eulerAngles) * Vector3.forward * _cannonController.shootForce;
        
        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = i * timeStep;
            float yOffset = velocity.y * t + 0.5f * Physics.gravity.y * t * t;
            Vector3 position = cannonBarrel.position + velocity * t;
            position.y += yOffset;
            points[i] = position;
        }

        _trajectoryLineRenderer.SetPositions(points);
    }
}