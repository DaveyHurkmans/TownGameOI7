using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerAnimations : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // bool jumpPressed = Input.GetKey(KeyCode.Space);
        // bool chopPressed = Input.GetKey(KeyCode.Mouse0); //Left mouse button

        if (Input.GetKeyDown(KeyCode.Space | KeyCode.JoystickButton4))
        {
            animator.SetTrigger("Jumping");
        }

        if (Input.GetKeyDown(KeyCode.Mouse0 | KeyCode.JoystickButton3))
        {
            animator.SetTrigger("Chopping");
        }

    }
}
