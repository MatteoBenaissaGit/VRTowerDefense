using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(TrajectoryPredictor))]
public class ProjectileThrow : MonoBehaviour
{
    TrajectoryPredictor trajectoryPredictor;

    [SerializeField] Rigidbody objectToThrow;

    [SerializeField] float force;

    [SerializeField] Transform StartPosition;

    [SerializeField] private GameObject _explosion;

    [SerializeField] private InputActionReference _action1;
    [SerializeField] private InputActionReference _action2;

    void OnEnable()
    {
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();

        if (StartPosition == null)
            StartPosition = transform;
    }

    private void Start()
    {
        //_action1.action.Enable();
        //_action1.action.performed += context => { ThrowObject(); };
        
        //_action2.action.Enable();
        //_action2.action.performed += context => { ThrowObject(); };
    }

    void Update()
    {
        Predict();
    }

    void Predict()
    {
        trajectoryPredictor.PredictTrajectory(ProjectileData());
    }

    ProjectileProperties ProjectileData()
    {
        ProjectileProperties properties = new ProjectileProperties();
        Rigidbody r = objectToThrow.GetComponent<Rigidbody>();

        properties.direction = StartPosition.forward;
        properties.initialPosition = StartPosition.position;
        properties.initialSpeed = force;
        properties.mass = r.mass;
        properties.drag = r.drag;

        return properties;
    }

    public void ThrowObject()
    {
        Rigidbody thrownObject = Instantiate(objectToThrow, StartPosition.position, Quaternion.identity);
        thrownObject.AddForce(StartPosition.forward * force, ForceMode.Impulse);

        _explosion.GetComponent<ParticleSystem>().Play();
    }
}