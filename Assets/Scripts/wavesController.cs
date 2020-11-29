using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wavesController : MonoBehaviour
{

    //adjust this to change speed
    public float speed = 2f;
    //adjust this to change how high it goes
    public float height = 0.5f;
    public int islandHeight = 2;
    void Update()
    {
        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.position;
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed) - islandHeight;
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(pos.x, newY, pos.z) * height;
    }
}
