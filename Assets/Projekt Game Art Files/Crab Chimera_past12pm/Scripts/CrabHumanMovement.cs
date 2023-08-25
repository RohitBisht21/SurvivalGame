using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMovement
{


public class CrabHumanMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 15f;

    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float jumpHeight;

    Vector3 velocity;
    bool isGrounded;

    public float runspeed;

    public float normalspeed;

    public bool isRunningg = false;
    
    public Animator anim;
    int isWalkingHash;
    int isRunningHash;

    public bool attacking = false;
    
    public float rotationSpeed;

    
    
// Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

            
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Vector3 move = transform.right * x + transform.forward * z;
        //controller.Move(move * speed * Time.deltaTime);

        Vector3 movementDirection = new Vector3(x, 0, z);
        movementDirection.Normalize();

        controller.Move(movementDirection * speed * Time.deltaTime);

        if(movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(-movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        bool isRunning = anim.GetBool(isRunningHash);
        bool isWalking = anim.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);



        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))

        {
            isRunningg = true;
            speed = runspeed;
            
            
        }

        else
        {
            isRunningg = false;
            speed = normalspeed;
            
            
        }

        if (attacking == false &&  !isWalking && forwardPressed)

        {
            anim.SetBool(isWalkingHash, true);  
            anim.SetBool("isAttacking", false);
        }
        
        if (isWalking && !forwardPressed)

        {
            anim.SetBool(isWalkingHash, false);  
        }


        if (attacking == false && !isRunning &&(forwardPressed && runPressed))

        {
            anim.SetBool(isRunningHash, true); 
            anim.SetBool("isAttacking", false);
        }

        if (isRunning && (!forwardPressed || !runPressed))

        {
            anim.SetBool(isRunningHash, false);  
        }
        
        
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isAttacking", true);
            anim.SetBool(isRunningHash, false); 
            anim.SetBool(isWalkingHash, false); 

            attacking = true;
            speed = 0.0f;
            runspeed = 0.0f;
            normalspeed = 0.0f;

            StartCoroutine(attacked());
        }


    }

    IEnumerator attacked()
    {
        yield return new WaitForSeconds(2.0f);
        attacking = false;
        
        runspeed = 20.0f;
        normalspeed = 10.0f;
    }


    
}
}
