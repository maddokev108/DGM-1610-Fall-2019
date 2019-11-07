using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    internal bool gameOver = false;
    internal int lives = 1;

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
                lives--;
                Debug.Log("Hit. Lives remaining: " + lives);
                if (lives <= 0)
                {
                    // Destroy(gameObject); //Destroy the player.
                    Debug.Log("Game Over");
                    gameOver = true;
                }

            }

        } else if ( other.gameObject.CompareTag("Pickup") )
        {
            lives++;
            Debug.Log("Pickup collected. Effect: 1-UP. Lives Remaining: " + lives);
            Destroy(other.gameObject);
        }
    }
}
