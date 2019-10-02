using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horInput;
    public float speed = 90;
    private float xRange = 15;
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //This code keeps the player inside the boundaries
        if ( transform.position.x < -xRange )
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if ( transform.position.x > xRange )
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        //This statement checks for the space key input
        if ( Input.GetKeyDown(KeyCode.Space) ) 
        {
            //Fire food
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }

        //This makes the player move
        horInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * horInput * speed);
    }
}
