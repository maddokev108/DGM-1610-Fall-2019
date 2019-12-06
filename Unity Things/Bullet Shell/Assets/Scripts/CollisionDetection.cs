using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* NOTE TO SELF: THIS SCRIPT SHOULD NOT BE THE MAIN STORAGE PLACE OF score, lives, or isGameOver. those have been moved to Game Manager. */

public class CollisionDetection : MonoBehaviour
{
/*================
    References
================*/
  //--Object/Script/Etc. References
    private GameObject gameManager; //Game Manager
    private PlayerController PlayerControllerScript; //PlayerController script reference
    private GameManager gameManagerScript; //GameManager script reference
    private AudioSource audioController; //Player's AudioSource component
    public AudioClip clink; //clink sound effect. plays when player is hit while hiding.
    public AudioClip squeak; //squeak sound effect. plays when player collects a powerup.
  //--Variable References
    private bool isGameOver; //tracks if the game is over. retrieved from GameManager
    private int lives; //lives, retrieved from GameManager


/*===============
    Variables
===============*/
  /*---------
    private
  ---------*/
    private List<int> previousMultiplierList = new List<int>(); //used by the score pickup so that stacked multipliers won't lose their effects all at once.

  /*----------
    internal
  ----------*/
    internal int bonusMultiplier = 1;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        // gameOverScreen = GameObject.Find("Game Over Screen");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        PlayerControllerScript = gameObject.GetComponent<PlayerController>();
        audioController = gameObject.GetComponent<AudioSource>();
        // gameOverScreen.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        isGameOver = gameManagerScript.isGameOver; //see if the game is over yet.

        GameObject collidedObject = other.gameObject;
        if (!collidedObject.CompareTag("Untagged") && !isGameOver) //if the player hits something important while the game is running...
        {

            switch(collidedObject.tag)
            {
                case "Bullet": //If the player hits a bullet...
                    gameManagerScript.UpdateScore(-500); //subtract 500 points (about 5 seconds worth of score).

                    if (!PlayerControllerScript.isHiding) //If the player is not hidden...
                    {
                        gameManagerScript.UpdateLives(-1); //... Lose a life.
                    }
                    else //if the player is hiding...
                    {
                        StartCoroutine(PlayClink(Random.Range(1,4)));
                    }
                    break;
                case "LifePickup": //If the player hits a 1-Up...
                    gameManagerScript.UpdateLives(1); //... Add a life.
                    gameManagerScript.UpdateScore(1000); //... Add 1000 score.
                    PlaySqueak();
                    break;
                
                case "SpeedPickup": //If the player hits a Speed boost...
                    //... Speed up the player.
                    if (PlayerControllerScript.speed <= 15)
                    {
                        PlayerControllerScript.speed += 1.0f;
                    }
                    Debug.Log("Speed Pickup collected. Effect: Speed Up. Current Speed: " + PlayerControllerScript.speed);
                    gameManagerScript.UpdateScore(1000); //... Add 1000 score.
                    PlaySqueak();
                    break;
                
                case "ScorePickup": //If the player hits a Score Multiplier...
                    //... Temporarily boost the score gained over time.
                    previousMultiplierList.Add(bonusMultiplier);
                    bonusMultiplier = Mathf.RoundToInt( bonusMultiplier + 4.3f - (5 / (gameManagerScript.playTime / 20 + 1)) + 1 / ((gameManagerScript.playTime / 10) + 1) ); //The bonus multiplier has a maximum value 5. The multiplier will start at x1 (at game start), and it will be larger based on how long the round has been going on. This bonus is stackable (additive).
                    StartCoroutine(BonusMultiplierCountdownRoutine());
                    Debug.Log("Score Pickup collected. Effect: Temporary Bonus Multiplier. Current Score Multiplier: " + bonusMultiplier);
                    PlaySqueak();
                    gameManagerScript.UpdateScore(1000); //... Add 1000 score.
                    break;
            }
            Destroy(collidedObject); //destroy the thing
        }
    }
    //Plays the squeak.
    void PlaySqueak()
    {
      audioController.clip = squeak;
      audioController.time = 0.0f;
      audioController.volume = 1.0f;
      audioController.Play();
    }
    //Play the clink.
    IEnumerator PlayClink(int version)
    {
      audioController.clip = clink;
      audioController.time = 2.0f * version;
      audioController.volume = 0.2f;
      audioController.Play();
      yield return new WaitForSeconds(0.5f);
      audioController.Stop();
    }

    //Start the Bonus Multiplier countdown
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
