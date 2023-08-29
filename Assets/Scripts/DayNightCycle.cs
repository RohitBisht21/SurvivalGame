using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float dayDuration = 300.0f; // Duration of one full day in seconds

    private float currentTime = 0.0f;

    void Update()
    {
        // Update the current time based on real-time
        currentTime += Time.deltaTime;

        // Calculate the angle for the sun's rotation based on the current time
        float sunRotationAngle = (currentTime / dayDuration) * 360.0f;

        // Rotate the Directional Light to simulate the sun's movement
        // You may need to adjust the axis and direction based on your scene's setup
        transform.rotation = Quaternion.Euler(sunRotationAngle, 0f, 0f);
    }
}
