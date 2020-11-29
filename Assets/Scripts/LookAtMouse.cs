using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    Vector2 rotation = new Vector2 (0, 0);
	public float speed = 2;

	void Update () {
		rotation.y += Input.GetAxis ("Mouse X");
		transform.eulerAngles = (Vector2)rotation * speed;
	}
}
