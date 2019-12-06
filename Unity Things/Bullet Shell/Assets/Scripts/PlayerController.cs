using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*NOTE TO SELF: the cursor locking functionality of this script has been moved to GameManager. */

public class PlayerController : MonoBehaviour
{
    private GameObject shell; //the object that will be used as the visual cue for when the player is hiding in their shell.

    private float xBound = 17.8f; //left-right screen bounds
    private float zBound = 10.0f; //top-bottom screen bounds

    internal float speed = 5.5f; //player movement speed
    internal float previousSpeed; //stores the player's speed so it doesn't reset every time they hide
    internal bool isHiding = false; //tracks whether or not the player is hiding in their shell


    // private bool cursorToggledOff; //tracks whether or not the cursor is enabled.

    // Start is called before the first frame update
    void Start()
    {
        shell = GameObject.Find("Shell"); //find the shell.
        shell.SetActive(false); //hide the shell.
    }

    // Update is called once per frame
    void Update()
    {
        bool isGameOver = GameObject.Find("Game Manager").GetComponent<GameManager>().isGameOver;
        if (!isGameOver) //checks to see if the game is still running.
        {
            float xPos = transform.position.x; //tracks the player's x position
            float zPos = transform.position.z; //tracks the player's z position
            float horizontalInput = Input.GetAxisRaw("Mouse X"); //get horizontal mouse movement.
            float forwardInput = Input.GetAxisRaw("Mouse Y"); //get vertical mouse movement.

            //This code makes the player move
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);


            if (Mathf.Abs(xPos) > xBound) //if the player is out of bounds (x)...
            {
                transform.position = new Vector3(xBound * Mathf.Abs(xPos) / xPos, 0.492f, zPos); //...bring them back on-screen (x)
            }
            if (Mathf.Abs(zPos) > zBound) //if the player is out of bounds (z)...
            {
                transform.position = new Vector3(xPos, 0.492f, zBound * Mathf.Abs(zPos) / zPos); //...bring them back on-screen (z)
            }


            //Hiding mechanic
            if (!isHiding) //If the player is not hiding...
            {
                previousSpeed = speed; //... Save the player's speed

                if (Input.GetAxisRaw("Hide") != 0 ) //if the "Hide" button is pressed down...
                {
                    speed = 0; //...Stop the player from moving
                    isHiding = true; //update bool.
                    shell.SetActive(true);
                }

            }
            else if ( Input.GetAxisRaw("Hide") == 0 ) //Otherwise...
            {
                speed = previousSpeed; //... Let the player move again.
                isHiding = false; //update bool.
                shell.SetActive(false); //hide the shell.

            }
        }

    }
}
