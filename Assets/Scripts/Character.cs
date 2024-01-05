using System.Security.Cryptography;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (DialogueManager.isActive == true)
            return;

        float xMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(new Vector3(xMovement, 0, 0));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
        }
    }
}
