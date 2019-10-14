using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private float lastDog = -1f; //initialize lastDog as -1 to allow the player to fire imediately.
    private float dogTime;
    private float cooldown = 1f;

    // Update is called once per frame
    void Update()
    {
        //get the time since the last dog was spawned
        dogTime = Time.time - lastDog;

        // On spacebar press, send dog if the cooldown has finished.
        if (Input.GetKeyDown(KeyCode.Space) & dogTime >= cooldown)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            //update the timestamp for the most recent fire.
            lastDog = Time.time;
        }
    }
}
