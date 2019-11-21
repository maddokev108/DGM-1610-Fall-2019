using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    NOTE TO SELF: WHAT THIS SCRIPT DOES
      - instantiates random pickups on-screen.
*/

public class SpawnPickups : MonoBehaviour
{
    public GameObject[] pickupPrefabList;

    private float zBound = 5.0f;
    private float xBound = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnPickup", Random.Range(.50f, 1.00f));
    }

    void SpawnPickup()
    {
        bool gameOver = GameObject.Find("Player").GetComponent<CollisionDetection>().gameOver;
        if (!gameOver) //checks to see if the game is still running.
        {
            GameObject pickupPrefab = ChooseRandomPickupType(); //randomly select a pick-up type
            float rot = Random.Range(0.0f, 180.0f);
            GameObject newPickup = Instantiate(pickupPrefab, new Vector3(Random.Range(-xBound, xBound), pickupPrefab.transform.position.y, Random.Range(-zBound, zBound)), pickupPrefab.transform.rotation);
            newPickup.GetComponent<Transform>().Rotate(rot, 0, 0);
            Invoke("SpawnPickup", Random.Range(5.0f, 10.0f));
        }
    }
    GameObject ChooseRandomPickupType()
    {
        int choice;
        float chance = Random.Range(0.0f, 100.0f);
        if (chance >= 75.0f) //25% chance to...
        {
            //...choose Life Pickup.
            choice = 0; 
        }
        else if (chance >= 50) //25% chance to...
        {
            //...choose Speed Pickup.
            choice = 1;
        }
        else //50% chance to...
        {
            //...choose Score Multiplier Pickup
            choice = 2;
        }

        return pickupPrefabList[choice];
    }
}
