using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private float hungerValue = 10f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Survival.Instance.IncreaseHunger(hungerValue);
            gameObject.SetActive(false);
        }
    }
}
