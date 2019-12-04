using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private PlayerController PlayerControllerScript;
    private GameObject gameManager;
    private LifeScoreManager lifeScoreManagerScript;
    private GameObject gameOverScreen;
    internal bool isGameOver = false;
    private int lives;
    private List<int> previousMultiplierList = new List<int>(); //used by the score pickup so that stacked multipliers won't lose their effects all at once.
    private int score; //score, to be tracked by UI later in development.
    internal int bonusMultiplier = 1;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        gameOverScreen = GameObject.Find("Game Over Screen");
        PlayerControllerScript = gameObject.GetComponent<PlayerController>();
        lifeScoreManagerScript = gameManager.GetComponent<LifeScoreManager>();

        score = lifeScoreManagerScript.score;
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if (!isGameOver)
        // {
        //     //Update the Score
        //     {
        //         float scoreIncrease = 100 * Time.deltaTime * bonusMultiplier;
        //         score += Mathf.RoundToInt(scoreIncrease); 
        //         //Debug.Log("Score incremented by: " + scoreIncrease + ". Score is now: " + score + ". Current time (seconds): " + Time.time);
        //     }
        // }
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;
        if (!collidedObject.CompareTag("Untagged") && !isGameOver) //if the player hits something important while the game is running...
        {
            Destroy(collidedObject); //destroy the thing
            switch(collidedObject.tag)
            {
                case "Bullet": //If the player hits a bullet...
                    lifeScoreManagerScript.UpdateScore(-500); //subtract 500 points (about 5 seconds worth of score).

                    if (!PlayerControllerScript.isHiding) //If the player is not hidden...
                    {
                        lifeScoreManagerScript.UpdateLives(-1); //... Lose a life.

                        if (lifeScoreManagerScript.lives <= 0) //If the player is dead...
                        {
                            lifeScoreManagerScript.lives = 0; //set lives to 0 (just in case it went negative).
                            isGameOver = true; //... End the game.
                            Debug.Log("Game Over. Time: " + Time.time + ". Score: " + score + "."); //debug message.
                            gameOverScreen.SetActive(true);
                        }
                    }
                    break;
                case "LifePickup": //If the player hits a 1-Up...
                    //... Add a life.
                    lifeScoreManagerScript.UpdateLives(1);
                    break;
                
                case "SpeedPickup": //If the player hits a Speed boost...
                    //... Speed up the player.
                    PlayerControllerScript.speed += 0.5f;
                    Debug.Log("Speed Pickup collected. Effect: Speed Up. Current Speed: " + PlayerControllerScript.speed);
                    break;
                
                case "ScorePickup": //If the player hits a Score Multiplier...
                    //... Temporarily boost the score gained over time.
                    previousMultiplierList.Add(bonusMultiplier);
                    bonusMultiplier = Mathf.RoundToInt( bonusMultiplier + 4.3f - (5 / (Time.time / 20 + 1)) + 1 / ((Time.time / 10) + 1) ); //The bonus multiplier has a maximum value 5. The multiplier will start at x1 (at game start), and it will be larger based on how long the round has been going on. This bonus is stackable (additive).
                    StartCoroutine(BonusMultiplierCountdownRoutine());
                    Debug.Log("Score Pickup collected. Effect: Temporary Bonus Multiplier. Current Score Multiplier: " + bonusMultiplier);
                    break;
            }
        }
    }

    IEnumerator BonusMultiplierCountdownRoutine()
    {
        yield return new WaitForSeconds(10); //wait 10 seconds

        //remove effect.
        int index = previousMultiplierList.Count-1; //finds the index of the last item in previousMultiplierList. This item is the previous value of the score multiplier.
        bonusMultiplier = previousMultiplierList[index]; //sets the score multiplier to its previous value.
        previousMultiplierList.RemoveAt(index); //removes the stored value from the list.
        Debug.Log("Bonus Multiplier over. Current Score Multiplier: " + bonusMultiplier); //debug message
    }
}
