using UnityEngine;

public class CircularCameraOrbit : MonoBehaviour
{
    public Transform target;  
    public float radius = 5f; // Radius of the circular orbit
    public float rotationSpeed = 10f; // Speed of the camera's rotation

    private Vector3 offset;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Please assign a target for the camera to orbit around.");
            enabled = false;
            return;
        }

        offset = transform.position - target.position;
    }

    void Update()
    {
        // Calculate the desired position for the camera
        float angle = Time.time * rotationSpeed;
        Vector3 desiredPosition = target.position + offset + new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationSpeed);

        // Make the camera look at the target object
        transform.LookAt(target);
    }
}
