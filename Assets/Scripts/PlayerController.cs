using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // public Rigidbody theRB;
    public float moveSpeed;
    public float runSpeed;
    public float jumpForce;
    public CharacterController controller;
    public float gravityScale;
    private Vector3 moveDirection;



    // Start is called before the first frame update
    void Start()
    {
        // theRB = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical") ) + (transform.right * Input.GetAxis("Horizontal") );
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if(controller.isGrounded) {
            moveDirection.y = -0.5f;
            if(Input.GetButtonDown("Jump")){
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

    }
}
