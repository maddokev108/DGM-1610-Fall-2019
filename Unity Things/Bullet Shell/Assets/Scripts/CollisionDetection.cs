using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject); //destroy the bullet no mater what

            if (gameObject.GetComponent<PlayerController>().hidden)
            {
                //not sure if I want to put anything in here, so just leaving it open for now
            }
            else
            {
                Debug.Log("Hit");
                // Destroy(gameObject); //Destroy the player.
            }

        } else if ( other.gameObject.CompareTag("Pickup") )
        {
            Debug.Log("Pickup collected");
            Destroy(other.gameObject);
        }
    }
}
