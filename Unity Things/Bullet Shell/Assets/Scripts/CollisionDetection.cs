using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    internal bool gameOver = false;
    internal int lives = 10;
    private PlayerController PlayerControllerScript;
    private float playerBonusMultiplier; //just made this to save myself some typing.
    private float previousMultiplier; //used by the score pickup so that stacked multipliers won't lose their effects all at once.

    internal int score = 0; //score, to be tracked by UI later in development. Increases 
    internal float bonusMultiplier = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        PlayerControllerScript = gameObject.GetComponent<PlayerController>();
        // playerBonusMultiplier = PlayerControllerScript.bonusMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            //Update the Score
            {
                float scoreIncrease = 100 * Time.deltaTime * bonusMultiplier;
                score += Mathf.RoundToInt(scoreIncrease); 
                Debug.Log("Score incremented by: " + scoreIncrease + ". Score is now: " + score + ". Current time (seconds): " + Time.time);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;
        if (!collidedObject.CompareTag("Untagged"))
        {
            Destroy(collidedObject);
            
            if (collidedObject.CompareTag("Bullet")) //If the player hits a bullet...
            {
                if (!PlayerControllerScript.hidden) //If the player is not hidden...
                {
                    //... Lose a life.
                    lives--;

                    // //subtract 500 points (about 5 seconds worth.
                    // float playerScore = PlayerControllerScript.score;
                    // playerScore = playerScore - 500;
                    // if (playerScore < 0) //If the score is negative...
                    // {
                    //     //... Set it to 0.
                    //     playerScore = 0;
                    // }

                    //subtract 500 points (about 5 seconds worth.
                    score = score - 500;
                    if (score < 0) //If the score is negative...
                    {
                        //... Set it to 0.
                        score = 0;
                    }

                    Debug.Log("Hit. Lives remaining: " + lives + ". Score Remaining: " + score );
                    if (lives <= 0) //If the player is dead...
                    {
                        //... End the game.
                        Debug.Log("Game Over"); 
                        gameOver = true;
                    }
                }

            }
            else if ( collidedObject.CompareTag("LifePickup") ) //If the player hits a 1-Up...
            {
                //... Add a life.
                lives++;
                Debug.Log("Life Pickup collected. Effect: 1-UP. Lives Remaining: " + lives);

            }
            else if ( collidedObject.CompareTag("SpeedPickup") ) //If the player hits a Speed boost...
            {
                //... Speed up the player.
                PlayerControllerScript.speed += 0.5f;
                Debug.Log("Speed Pickup collected. Effect: Speed Up. Current Speed: " + PlayerControllerScript.speed);

            }
            else if ( collidedObject.CompareTag("ScorePickup") ) //If the player hits a Score Multiplier...
            {
                //... Temporarily boost the score gained over time.
                // previousMultiplier = playerBonusMultiplier;
                // playerBonusMultiplier = playerBonusMultiplier + ( 5.0f - (5.0f / (Time.time / 20 + 1)) + 1 / ((Time.time / 10) + 1)); //The bonus multiplier has a maximum value infinitely approaching 5. The multiplier will start at x1.0 (at game start), and it will be larger based on how long the round has been going on.
                // StartCoroutine(BonusMultiplierCountdownRoutine());
                // Debug.Log("Score Pickup collected. Effect: Temporary Bonus Multiplier. Current Score Multiplier: " + playerBonusMultiplier);

                previousMultiplier = bonusMultiplier;
                bonusMultiplier = bonusMultiplier + ( 5.0f - (5.0f / (Time.time / 20 + 1)) + 1 / ((Time.time / 10) + 1)); //The bonus multiplier has a maximum value infinitely approaching 5. The multiplier will start at x1.0 (at game start), and it will be larger based on how long the round has been going on.
                StartCoroutine(BonusMultiplierCountdownRoutine());
                Debug.Log("Score Pickup collected. Effect: Temporary Bonus Multiplier. Current Score Multiplier: " + bonusMultiplier);
            }
        }
    }

    IEnumerator BonusMultiplierCountdownRoutine()
    {
        //wait 10 seconds
        yield return new WaitForSeconds(10);

        //remove effect.
        bonusMultiplier = previousMultiplier;
        Debug.Log("Bonus Multiplier over");
    }
}
