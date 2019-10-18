using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    NOTE TO SELF: WHAT THIS SCRIPT DOES
      - instantiates random pickups on-screen.
*/

public class SpawnPickups : MonoBehaviour
{
    public GameObject pickupPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnPickup", Random.Range(5.0f, 10.0f));
    }

    void SpawnPickup()
    {
        float rot = Random.Range(0.0f, 180.0f);
        GameObject newPickup = Instantiate(pickupPrefab, new Vector3(Random.Range(-14.0f, 14.0f), pickupPrefab.transform.position.y, Random.Range(-5.0f, 5.0f)), pickupPrefab.transform.rotation);
        // newPickup.GetComponent<Transform>().position = new Vector3(Random.Range(-8.0f, 8.0f), pickupPrefab.transform.position.y, Random.Range(-20.0f, 20.0f));
        newPickup.GetComponent<Transform>().Rotate(rot, 0, 0);
        Invoke("SpawnPickup", Random.Range(5.0f, 10.0f));
    }

}
