using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ManageControls : MonoBehaviour
{
    //components
    public static ManageControls Instance { get; private set; }
    private CharacterController characterController;
    public Animator animator;

    // movement variable
    public float inputX;
    public float inputZ;
    private Vector3 hMovement;
    private Vector3 vMovement;
    private Vector3 velocity;
    public float moveSpeed;
    private float mouseSpeed;
    public float jumpSpeed;
    private float gravity;

    private bool isRunning = false;
    private bool wasInAir = false;
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

    // Start is called before the first frame update
    void Start()
    {
        //Get required components
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        characterController = tempPlayer.GetComponent<CharacterController>();
        animator = tempPlayer.transform.GetComponent<Animator>();

        // initialize var
        moveSpeed = 8f;
        gravity =-15f;
        mouseSpeed = 100f;
        jumpSpeed = 10f;
        
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        // gravity 
        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            animator.SetBool("standindJump", true);
            velocity.y = jumpSpeed;
        }
        else
        {
            animator.SetBool("standindJump", false);
        }
        velocity.y += gravity * Time.deltaTime;

        // Running Sound & Animation
        if (inputZ != 0 && characterController.isGrounded || inputX != 0 && characterController.isGrounded)
        {
            animator.SetBool("isRunning", true);
            if (!isRunning)
            {
                isRunning = true;
                AudioManager.Instance.Play("Running"); // Play running sound
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            if (isRunning)
            {
                isRunning = false;
                AudioManager.Instance.Stop("Running"); // Stop running sound
            }
        }

        // Check for landing sound
        if (characterController.isGrounded)
        {
            if (wasInAir)
            {
                wasInAir = false;
                AudioManager.Instance.Play("Jumping"); // Play landing sound
            }
        }
        else
        {
            wasInAir = true;
        }
    }

    private void FixedUpdate()
    {
        // input forward/backward
        vMovement = characterController.transform.forward * inputZ;
        hMovement = characterController.transform.right * inputX;

        // character movement with gravity
        characterController.Move(vMovement * moveSpeed * Time.deltaTime);
        characterController.Move(hMovement * moveSpeed * Time.deltaTime);
        characterController.Move(velocity * Time.deltaTime);
        
    }


}
