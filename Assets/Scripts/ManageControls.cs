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
    private float moveSpeed;
    private float mouseSpeed;
    private float jumpSpeed;
    private float gravity;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this; // Assign the instance to the static property
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }
        //Get required components
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        characterController = tempPlayer.GetComponent<CharacterController>();
        animator = tempPlayer.transform.GetComponent<Animator>();

        // initialize var
        moveSpeed = 7f;
        gravity =-15f;
        mouseSpeed = 100f;
        jumpSpeed = 7f;
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

        // running animation
        if (inputZ != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
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
