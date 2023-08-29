using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    private float healthIncrement = 30f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Survival.Instance.UseHealthKit(healthIncrement);
            gameObject.SetActive(false);
        }
    }
}
