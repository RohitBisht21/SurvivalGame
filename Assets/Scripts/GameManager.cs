using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

    public int zombiesDefeated = 0;
    public bool boss1Defeated = false;
    public GameObject gate; // Reference to your gate GameObject

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ZombieDefeated()
    {
        zombiesDefeated++;

        // Check if both conditions are met to activate the gate
        if (zombiesDefeated >= 20 && boss1Defeated)
        {
            ActivateGate();
        }
    }

    public void Boss1Defeated()
    {
        boss1Defeated = true;

        // Check if both conditions are met to activate the gate
        if (zombiesDefeated >= 20 && boss1Defeated)
        {
            ActivateGate();
        }
    }

    private void ActivateGate()
    {
        if (gate != null)
        {
            // Activate the gate (you can enable any necessary components here)
            gate.SetActive(true);
        }
    }
}
