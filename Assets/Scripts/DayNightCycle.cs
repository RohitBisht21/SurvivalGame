using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
    public float dayDuration = 300.0f; // Duration of a full day-night cycle in seconds
    private Light directionalLight;
    private float currentTime = 0.0f;

    void Start()
    {
        directionalLight = GetComponent<Light>();
        StartCoroutine(UpdateSunRotation());
    }

    IEnumerator UpdateSunRotation()
    {
        while (true)
        {
            // Update the current time based on real-time
            currentTime += Time.deltaTime;

            // Calculate the angle for the sun's rotation based on the current time
            float sunRotationAngle = (currentTime / dayDuration) * 360.0f;

            // Rotate the Directional Light to simulate the sun's movement
            directionalLight.transform.rotation = Quaternion.Euler(sunRotationAngle, 0f, 0f);

            // Wait for a short interval before the next update
            yield return new WaitForSeconds(0.1f); // Adjust the interval as needed
        }
    }
}
