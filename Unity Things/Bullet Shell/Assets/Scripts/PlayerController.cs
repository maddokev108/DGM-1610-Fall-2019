using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20f;
    private float horizontalInput;
    private float forwardInput;
    public bool hidden = false;
    private float xBound = 17.8f;
    private float zBound = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //This code controls the player's movement.
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        //This code makes the player move, but stops them from going off-screen
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        float xPos = transform.position.x;
        float zPos = transform.position.z;

        if (Mathf.Abs(xPos) > xBound) //if the player is out of bounds (x)...
        {
            transform.position = new Vector3(xBound * Mathf.Abs(xPos) / xPos, 0, zPos); //...bring them back on-screen (x)
        }
        if (Mathf.Abs(zPos) > zBound) //if the player is out of bounds (z)...
        {
            transform.position = new Vector3(xPos, 0, zBound * Mathf.Abs(zPos) / zPos); //...bring them back on-screen (z)
        }


        if( !hidden && Input.GetAxisRaw("Hide") != 0 )
        {
            speed = 0;
            hidden = true;
        } else if (Input.GetAxisRaw("Hide") == 0)
        {
            speed = 20f;
            hidden = false;
        }
    }
}
