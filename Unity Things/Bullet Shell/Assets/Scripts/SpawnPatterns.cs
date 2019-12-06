using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    NOTE TO SELF: WHAT THIS SCRIPT DOES
      - instantiates the bullet patterns off-screen
          + rotates the bullet patterns to face some direction such that they travel across the screen.
*/

public class SpawnPatterns : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject patternPrefab;
    public float rot;
    private float xBound = 17.8f;
    private float zBound = 10.0f;

    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    //Enables spawning
    public void StartSpawning()
    {
        Invoke("SpawnPattern", Random.Range(1.0f, 2.0f));
    }

    //Spawn a bullet pattern, then calls itself after a randomized delay.
    void SpawnPattern()
    {
        bool isGameOver = gameManagerScript.isGameOver;
        if (!isGameOver) //checks to see if the game is still running.
        {
            //Chooses random off-screen position
            int rand = (( Random.Range(1, 3) - 1 ) * 2) - 1; //Steps: |1. RNG| 1 or 2. |2. N-1| 0 or 1. |3. N*2| 0 or 2. |4. N-1| -1 or 1.
            float xPos = ( xBound * Random.Range(1.0f, 1.25f) + 7) * rand; //chooses an off-screen x-position.
            rand = (( Random.Range(1, 3) - 1 ) * 2) - 1; // random -1 or 1 again.
            float zPos = ( zBound * Random.Range(1.0f, 1.5f) + 7) * rand; //chooses an off-screen y-position.

            rot = Mathf.Atan2(zPos, -1*xPos) * 180 / Mathf.PI + 90 + Random.Range(-20.0f, 20.0f); //Chooses a somewhat randomized angle facing the play area. 


            //Instantiates the pattern, then rotates it to face the play area.
            GameObject newPattern = Instantiate(patternPrefab);
            newPattern.GetComponent<Transform>().position = new Vector3(xPos, 0.8f, zPos); //spawn at the generated coordinates
            newPattern.GetComponent<Transform>().Rotate(0, rot, 0); //face the generated direction

            //makes the game spawn enemies faster over time.
            float playTime = gameManagerScript.playTime; //retrieves playTime from GameManager
            float spawnRate;
            if (playTime < 150)
            {
                spawnRate = ((25 + 5.8f * playTime - 0.0325f * playTime * playTime) / (playTime + 4)) * 0.7f; //spawnRate gradually decreases (gets faster) according to this equation.
            }
            else
            {
                spawnRate = 0.7f; //fastest spawnRate. Achieved after 150 seconds.
            }
            Invoke("SpawnPattern", Random.Range(1.0f, spawnRate));
        }
    }

}
