using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    // click object Raycast
    public float force = 5;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Vector3 playerDistanceToObject = Vector3.Distance(hit.transform.position, player.transform.position);

            if (Physics.Raycast(ray, out hit, 20.0f))
            {
                if (hit.transform != null)
                {
                    Rigidbody rb;
                    if (rb = hit.transform.GetComponent<Rigidbody>())
                    {
                        PrintName(hit.transform.gameObject);
                        DestroyObject(hit.transform.gameObject);
                    }
                }
            }
        }
    }

    private void PrintName(GameObject go)
    {
        print(go.name);
    }

    // private void LaunchIntoAir(Rigidbody rig)
    // {
    //     rig.AddForce(rig.transform.up * force, ForceMode.Impulse);
    // }

    private void DestroyObject(GameObject go)
    {

        // if (playerDistanceToObject >= 20.0f)
        // {
        Destroy(go);
        // }
    }


}

