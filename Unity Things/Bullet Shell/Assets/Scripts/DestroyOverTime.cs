using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    private GameManager gameManagerScript; //GameManager script reference
    private float playTime; //tracks time (seconds) since the game began. retrieved from the GameManager script

    public float lifespan = 10.0f; //how long the powerup will last before despawning
    public float born; //timestamp of when the pickup was spawned

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>(); //find the GameManager script.

        playTime = gameManagerScript.playTime; //retrieve playTime from the GameManager script.
        born = playTime; //create the timestamp.
    }

    // Update is called once per frame
    void Update()
    {
        bool isGameOver = gameManagerScript.isGameOver; //retrieve isGameOver from the GameManager script.
        playTime = gameManagerScript.playTime; //retrieve playTime from the GameManager script.

        if (!isGameOver) //if the game is still running...
        {
            //... Check the powerup's age:
            if ( playTime >= lifespan + born ) //if the powerup gets to old...
            {
                Destroy(gameObject); //... destroy it.
            }
        }
    }
}
