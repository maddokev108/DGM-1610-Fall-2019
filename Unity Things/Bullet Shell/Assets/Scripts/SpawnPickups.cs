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
    private GameObject gameManager;
    private GameManager gameManagerScript;

    private float zBound = 5.0f;
    private float xBound = 10.0f;

    //enables spawning.
    public void StartSpawning()
    {
        Invoke("SpawnPickup", Random.Range(.50f, 1.00f)); //start spawning.
        gameManager = GameObject.Find("Game Manager"); //find the Game Manager object.
        gameManagerScript = gameManager.GetComponent<GameManager>(); //find the GameManager script.
    }

    //spawns a pickup, then calls itself after a randomized delay.
    void SpawnPickup()
    {
        bool isGameOver = gameManagerScript.isGameOver;
        if (!isGameOver) //checks to see if the game is still running.
        {
            GameObject pickupPrefab = ChooseRandomPickupType(); //randomly select a pick-up type
            float rot = Random.Range(0.0f, 180.0f);
            GameObject newPickup = Instantiate(pickupPrefab, new Vector3(Random.Range(-xBound, xBound), pickupPrefab.transform.position.y, Random.Range(-zBound, zBound)), pickupPrefab.transform.rotation);
            newPickup.GetComponent<Transform>().Rotate(rot, 0, 0);
            Invoke("SpawnPickup", Random.Range(5.0f, 10.0f));
        }
    }

    //picks a pickup type based on set probabilities.
    GameObject ChooseRandomPickupType()
    {
        int choice;
        float chance = Random.Range(0.0f, 100.0f);
        if (chance >= 75.0f) //25% chance to...
        {
            //...choose Life Pickup.
            choice = 0; 
        }
        else if (chance >= 37.5f) //37.5% chance to...
        {
            //...choose Speed Pickup.
            choice = 1;
        }
        else //37.5% chance to...
        {
            //...choose Score Multiplier Pickup
            choice = 2;
        }

        return pickupPrefabList[choice];
    }
}
