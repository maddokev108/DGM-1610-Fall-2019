using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LifeScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    private GameObject player;
    private PlayerController playerControllerScript;
    private CollisionDetection playerCollisionDetectionScript;

    private bool isGameOver;
    private bool isPlayerHiding;
    internal int lives;
    internal int score;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
        playerCollisionDetectionScript = playerCollisionDetectionScript = player.GetComponent<CollisionDetection>();

        lives = 7;
        score = 0;

        UpdateScore(0);
        UpdateLives(0);
        StartCoroutine(ScoreCounter());
    }

    void Update()
    {
        isGameOver = playerCollisionDetectionScript.isGameOver;
        isPlayerHiding = playerControllerScript.isHiding;
    }

    //updates the score on the screen
    public void UpdateScore(int amount)
    {
        score += amount; //change the score by the given amount.
        if (score < 0) //If the score is negative...
        {
            score = 0; //... Set it to 0.
        }

        scoreText.text = "Score: " + score; //display the new value.
        
    }

    //updates the lives on the screen
    public void UpdateLives(int amount)
    {
        lives += amount; //change the number of lives by the given amount.
        livesText.text = "lives: " + lives; //display the new value.
    }

    IEnumerator ScoreCounter()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(0.1f);

            int bonusMultiplier = playerCollisionDetectionScript.bonusMultiplier;

            if (isPlayerHiding) //if the player is hiding...
            {
                UpdateScore(-2 * bonusMultiplier); //... Decrease their score.
            }
            else //Otherwise...
            {
                UpdateScore(10 * bonusMultiplier); //... Increase their score.
            }
        }
    }
}
