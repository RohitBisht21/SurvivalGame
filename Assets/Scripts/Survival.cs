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

    public TakeDamage damageEffect;

    private float healthDecreaseRate = 1f;
    private CharacterController characterController;
    private float fallStartHeight;
    public float fallDamageMultiplier = 0.5f;
    private bool isFalling = false;

    private bool isMovingInWater = false;

    public Canvas deadCanvas;

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
        characterController = GetComponent<CharacterController>();
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


             // Check for falling
        if (!characterController.isGrounded)
        {
            if (!isFalling)
            {
                isFalling = true;
                fallStartHeight = transform.position.y;
            }
        }
        else if (isFalling)
        {
            isFalling = false;
            float fallDistance = fallStartHeight - transform.position.y;
            if (fallDistance > 10.0f) // Adjust the threshold if needed
            {
                float fallDamage = fallDistance * fallDamageMultiplier;
                TakeDamage(fallDamage);
            }
        }


        // flashlight control
        if (flashOff && Input.GetButtonDown("F"))
        {
            flashLight.SetActive(true);
            flashOff = false;
            flashOn = true;
            AudioManager.Instance.Play("Flash");
        }
        else if (flashOn && Input.GetButtonDown("F"))
        {
            flashLight.SetActive(false);
            flashOff = true;
            flashOn = false;
            AudioManager.Instance.Play("Flash");
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
            AudioManager.Instance.Stop("Running");

            // Check for player movement in water and play/stop swimming sound
            float movementInput = Mathf.Abs(ManageControls.Instance.inputX) + Mathf.Abs(ManageControls.Instance.inputZ);
                
                if (movementInput > 0.1f) // Adjust the threshold as needed
                {
                    if (!isMovingInWater)
                    {
                        isMovingInWater = true;
                        AudioManager.Instance.Play("WaterWalk");
                    }

                }
                else
                {
                    if (isMovingInWater)
                    {
                        isMovingInWater = false;
                        AudioManager.Instance.Stop("WaterWalk");
                    }
                }
            
            
        }
        else
        {
            ManageControls.Instance.animator.SetBool("Swimming", false);
            ManageControls.Instance.moveSpeed = 8f;
            ManageControls.Instance.jumpSpeed = 10f;
            PickUpController.Instance.Gun.SetActive(true);
            AudioManager.Instance.Stop("WaterWalk");

        }
    }

    public void TakeDamage(float damage)
    {
        AudioManager.Instance.Play("PlayerHit");
        Health -= damage;
        Health = Mathf.Clamp(Health, 0, MaxHealth);

        UpdateSliders();
        if(Health<=0)
        {
           deadCanvas.gameObject.SetActive(true);
           characterController.enabled=false;
           ManageControls.Instance.animator.enabled=false;
            AudioManager.Instance.Stop("Running");
        }
        damageEffect.StartDamageEffect();
    }

    public void IncreaseHunger(float value)
    {
        Hunger = Mathf.Clamp(Hunger + value, 0, MaxHunger);
        UpdateSliders();
    }
    public void UseHealthKit(float value)
    {
        Health = Mathf.Clamp(Health + value, 0, MaxHealth);
        UpdateSliders();
    }

    public void ResetSliderValues()
{
    Health = MaxHealth;
    Hunger = MaxHunger;
    Thirst = MaxThirst;
    UpdateSliders();
}

}
