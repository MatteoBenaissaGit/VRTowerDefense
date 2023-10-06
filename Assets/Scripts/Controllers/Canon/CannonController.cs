using UnityEngine;

public class CannonController : MonoBehaviour
{
    public Transform cannonBarrel;
    public GameObject ballPrefab;
    public float shootForce = 10f;

    [SerializeField] private GameObject xRoll;
    [SerializeField] private GameObject yRoll;
    
    private void Update()
    {
        transform.localRotation = Quaternion.Euler(yRoll.transform.rotation.eulerAngles.x,
            xRoll.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject ball = Instantiate(ballPrefab, cannonBarrel.position, Quaternion.identity);
        
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.AddForce(cannonBarrel.forward * shootForce, ForceMode.VelocityChange);
    }
}