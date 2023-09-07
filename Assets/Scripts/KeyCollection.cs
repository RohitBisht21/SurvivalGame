using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollection : MonoBehaviour
{
    public float rotationSpeed = 40f;
    public ParticleSystem keyParticles;

    public static KeyCollection Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Assigning the instance to the static property
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }
    }
    private void Update()
    {
        // Rotate the player-collected keys slowly
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with a Key
        if (other.CompareTag("Player"))
        {
            // Destroy the Key GameObject
            Destroy(gameObject);
            AudioManager.Instance.Play("KeyPicked");
            keyParticles.Stop();
            // Perform any additional key collection logic here, e.g., increment a key count, play a sound, etc.
        }
    }
}
