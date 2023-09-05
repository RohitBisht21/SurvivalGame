using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int zombiesDefeated = 0;
    public bool boss1Defeated = false;
    public GameObject gate;
    public ParticleSystem portal;

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

    public void DefeatZombie()
    {
        zombiesDefeated++;

        // Check if the conditions are met to activate the gate.
        CheckGateActivation();
    }

    public void DefeatBoss1()
    {
        boss1Defeated = true;

        // Check if the conditions are met to activate the gate.
        CheckGateActivation();
    }

    private void CheckGateActivation()
    {
        // Check if both conditions are met to activate the gate.
        if (boss1Defeated && zombiesDefeated >= 20)
        {
                gate.SetActive(true);
                portal.Play();
        }
    }
}
