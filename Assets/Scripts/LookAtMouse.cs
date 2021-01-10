using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    Vector2 rotation = new Vector2(0, 0);
    public float speed = 2;
    public float maxLTurn = -31;
    public float maxRTurn = 31;

    void Update()
    {
        if (rotation.y > maxLTurn || rotation.y < maxRTurn)
        {
            rotation.y += Input.GetAxis("Mouse X");
            // print(rotation.y);
            transform.eulerAngles = (Vector2)rotation * speed;
        }
        else { print("sorry out of boundary"); }
    }
}
