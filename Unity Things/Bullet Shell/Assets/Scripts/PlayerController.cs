using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    internal float speed = 5.5f;
    internal float previousSpeed; //to prevent the player's speed from resetting every time they hide.
    private float horizontalInput;
    private float forwardInput;
    internal bool isHiding = false;
    private float xBound = 17.8f;
    private float zBound = 10.0f;

    private bool cursorToggledOff;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cursorToggledOff = true;
    }

    // Update is called once per frame
    void Update()
    {
        bool isGameOver = GameObject.Find("Player").GetComponent<CollisionDetection>().isGameOver;
        if (!isGameOver) //checks to see if the game is still running.
        {
            // transform.Translate(Vector3.up * ( 0.492f - transform.position.y) );




            // //This code controls the player's movement.
            // horizontalInput = Input.GetAxis("Horizontal");
            // forwardInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxisRaw("Mouse X");
            forwardInput = Input.GetAxisRaw("Mouse Y");

            // //This code makes the player move
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

             float xPos = transform.position.x; //tracks the player's x position
             float zPos = transform.position.z; //tracks the player's z position



            if(Input.GetKeyUp("left ctrl"))
            {
                if (cursorToggledOff)
                {
                    Cursor.lockState = CursorLockMode.None;
                    cursorToggledOff = false;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    cursorToggledOff = true;
                }
            }

            float speedAdjusterInput = Input.GetAxisRaw("Vertical");
                speed += 0.1f * speedAdjusterInput;

            if (Mathf.Abs(xPos) > xBound) //if the player is out of bounds (x)...
            {
                transform.position = new Vector3(xBound * Mathf.Abs(xPos) / xPos, 0.492f, zPos); //...bring them back on-screen (x)
            }
            if (Mathf.Abs(zPos) > zBound) //if the player is out of bounds (z)...
            {
                transform.position = new Vector3(xPos, 0.492f, zBound * Mathf.Abs(zPos) / zPos); //...bring them back on-screen (z)
            }



            if (!isHiding) //If the player is not hidden...
            {
                //...Save the player's speed
                previousSpeed = speed;

                if (Input.GetAxisRaw("Hide") != 0 ) //When the "hide" button is pressed down...
                {
                    speed = 0; //...Stop the player from moving
                    isHiding = true;
                }
                
            }
            else if (Input.GetAxisRaw("Hide") == 0) //When the "hide" button is released...
            {
                speed = previousSpeed; //...Let the player move again
                isHiding = false;
            }
        }
        else{
            Cursor.lockState = CursorLockMode.None;
            cursorToggledOff = false;
        }
    }
}
