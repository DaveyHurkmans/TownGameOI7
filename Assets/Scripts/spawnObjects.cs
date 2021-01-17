using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObjects : MonoBehaviour
{
int tree = -1;
int stone = -7;
int brush = -2;
int resource;
string randomLocation;
// public string[] usedSpots;

public GameObject ObjectToSpawn;
public GameObject Tree;
public GameObject Brush;
public GameObject Stone;
public float timeLeft = 30.0f;
 
    void Start()
    {
        // Row 1
        Vector3 cube1 = new Vector3 (5, resource, 21);
        // usedSpots[0] = "cube1";
        Vector3 cube2 = new Vector3 (2, resource, 21);
        Vector3 cube3 = new Vector3 (-1, resource, 21);
        Vector3 cube4 = new Vector3 (-4, resource, 21);

        // Row 2
        Vector3 cube5 = new Vector3 (5, resource, 24);
        Vector3 cube6 = new Vector3 (2, resource, 24);
        Vector3 cube7 = new Vector3 (-1, resource, 24);
        Vector3 cube8 = new Vector3 (-4, resource, 24);

        // Row 3
        Vector3 cube9 = new Vector3 (5, resource, 27);
        Vector3 cube10 = new Vector3 (2, resource, 27);
        Vector3 cube11 = new Vector3(-1, resource, 27);
        Vector3 cube12 = new Vector3(-4, resource, 27);

        // Row 4 
        Vector3 cube13 = new Vector3(5, resource, 30);
        Vector3 cube14 = new Vector3(2, resource, 30);
        Vector3 cube15 = new Vector3(-1, resource, 30);
        Vector3 cube16 = new Vector3(-4, resource, 30);
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        // Debug.Log(timeLeft);
        if (timeLeft < 0)
        {
            spawnItem();
            timeLeft = 5.0f;
            return;
        }
    }

    private void PickRandomNumber(int maxInt) {
        int randomNum = Random.Range(1, maxInt+1);
        if(randomNum == 1){ resource = tree; }
        if(randomNum == 2){ resource = stone; }
        if(randomNum == 3){ resource = brush; }
    }

    private void spawnItem() {
        
        // Debug.Log(ObjectToSpawn);
        Instantiate( Tree,  Tree.transform.position, Tree.transform.rotation);
        // Instantiate( Stone,  new Vector3(5, stone, 24), Stone.transform.rotation);
        
        // if( usedSpots[].Contains() == false ){    
        //     // add item to list
        //     // spawn item on given location
        //         // randomLocation = 'cube' + PickRandomNumber(16);
        //         // Instantiate( ObjectToSpawn, randomLocation, Camera.Main.transform.rotation);

        // } else {return;}
    } 


}
