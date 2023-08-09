using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Survival : MonoBehaviour
{
    [Header("Player Health")]
    public float MaxHealth = 100f;
    public float Health = 0f;
    public Slider HealthSlider;

    [Header("Player Hunger")]
    public float MaxHunger = 100f;
    public float Hunger = 0f;
    public float HungerOT = 0.02f;
    public Slider HungerSlider;

    [Header("Player Thrist")]
    public float MaxThirst= 100f;
    public float Thirst = 0f;
    public float ThirstOT = 0.08f;
    public Slider ThirstSlider;

    private void Start()
    {
        Health = MaxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        Hunger = Hunger - HungerOT * Time.deltaTime;
        Thirst = Thirst - ThirstOT * Time.deltaTime;

        UpdateSliders();
    }

    public void UpdateSliders()
    {
        HealthSlider.value = Health / MaxHealth;
        HungerSlider.value = Hunger / MaxHunger;
        ThirstSlider.value = Thirst / MaxThirst;
    }
}
