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

    // for flashlight
    public GameObject flashLight;
    public bool flashOn;
    public bool flashOff;

    private float healthDecreaseRate = 1f;
    public static Survival Instance { get; private set; }

    private void Awake()
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

    private void Start()
    {
        Health = MaxHealth;
        flashOff = true;
        flashLight.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        Hunger = Mathf.Clamp(Hunger - HungerOT * Time.deltaTime, 0, MaxHunger);
        Thirst = Mathf.Clamp(Thirst - ThirstOT * Time.deltaTime, 0, MaxThirst);

        UpdateSliders();

        if (Hunger <= 0 && Thirst <= 0)
        {
            TakeDamage(healthDecreaseRate * 2 * Time.deltaTime); // Health decreases faster when both are empty
        }
        else if (Hunger <= 0 || Thirst <= 0)
        {
            TakeDamage(healthDecreaseRate * Time.deltaTime); // Regular health decrease
        }

        // flashlight control
        if (flashOff && Input.GetButtonDown("F"))
        {
            flashLight.SetActive(true);
            flashOff = false;
            flashOn = true;
        }
        else if (flashOn && Input.GetButtonDown("F"))
        {
            flashLight.SetActive(false);
            flashOff = true;
            flashOn = false;
        }
    }

    public void UpdateSliders()
    {
        HealthSlider.value = Health / MaxHealth;
        HungerSlider.value = Hunger / MaxHunger;
        ThirstSlider.value = Thirst / MaxThirst;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
       
        if (hit.gameObject.CompareTag("Water"))
        {
            
            ManageControls.Instance.animator.SetBool("Swimming", true);
            ManageControls.Instance.moveSpeed = 1f; 
            ManageControls.Instance.jumpSpeed = 0f;
            PickUpController.Instance.Gun.SetActive(false);

        }
        else
        {
            ManageControls.Instance.animator.SetBool("Swimming", false);
            ManageControls.Instance.moveSpeed = 7f;
            ManageControls.Instance.jumpSpeed = 7f;
            PickUpController.Instance.Gun.SetActive(true);
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health, 0, MaxHealth);

        UpdateSliders();
        if(Health<=0)
        {
            Debug.Log("YOU ARE DEAD");
        }
    }

}
