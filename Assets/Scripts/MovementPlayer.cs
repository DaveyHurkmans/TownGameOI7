using System.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //animator
    public Animator animator;

    //Variables
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;  
    
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;

    public float rotationSpeed;

    //References
    private CharacterController controller;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        animator.SetBool("IsGrounded", true);
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");
        float moveH = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveH, 0, moveZ);
        moveDirection.Normalize();
        moveDirection = transform.TransformDirection(moveDirection);
        
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);            
        }

        if(isGrounded)
        {

            if(moveDirection != Vector3.zero && !Input.GetKey(KeyCode.JoystickButton4))
            {
                Walk();
            } 
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.JoystickButton4))
            {
                Run();
            } 
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }

            moveDirection *= moveSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        
        controller.Move(moveDirection * Time.deltaTime); 
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

        

    private void Idle()
    {
        //idle
        animator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        animator.SetFloat("Speed", 1.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        animator.SetFloat("Speed", 3, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        animator.SetTrigger("Jumping");
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    // private void Punch()
    // {
    //     animator.SetTrigger("Chopping");
    // }

    private void Conversation()
    {
        // animator.SetTrigger("Talking");
    }


}