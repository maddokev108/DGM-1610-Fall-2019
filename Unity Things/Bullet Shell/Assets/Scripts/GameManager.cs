using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
/*================
    References
================*/

  /*--------
    public
  --------*/
    public TextMeshProUGUI scoreText; //score display
    public TextMeshProUGUI livesText; //lives display

  /*---------
    private
  ---------*/
    private GameObject menuScreen; //Menu Screen
    private GameObject gameOverScreen; //Game Over Screen
    private GameObject helpScreen; //Help Screen
    private GameObject player; //player reference;
    private PlayerController playerControllerScript; //PlayerController script reference
    private CollisionDetection playerCollisionDetectionScript; //CollisionDetection script reference


/*===============
    Variables
===============*/

  /*---------
    private
  ---------*/


  /*----------
    internal
  ----------*/
    internal int lives; //lives
    internal int score; //score
    internal bool isGameOver; //tracks if the game is over or not
    [SerializeField]internal float playTime; //meant to be like Time.time, but only starts running when the game actually starts being played. Accuracy to 0.001 seconds.
    internal bool cursorToggledOff; //tracks whether or not the cursor is enabled.




/*=====================
    Unity Functions
=====================*/

    void Start()
    {
        isGameOver = true;   

        gameOverScreen = GameObject.Find("Game Over Screen"); //find the Game Over Screen object.
        menuScreen = GameObject.Find("Menu Screen"); //Find the Menu Screen object.
        helpScreen = GameObject.Find("Help Screen"); //find the Help Screen object.
        player = GameObject.Find("Player"); //find the Player object.
        playerControllerScript = player.GetComponent<PlayerController>(); //find the player's PlayerController script.
        playerCollisionDetectionScript = playerCollisionDetectionScript = player.GetComponent<CollisionDetection>(); //find the player's CollisionDetection script.

        gameOverScreen.SetActive(false); //hide the Game Over Screen.
        helpScreen.SetActive(false); //hide the Help Screen.
        player.SetActive(false); //Hide the player.
        GameObject shell = player.transform.Find("Shell").gameObject; //find the shell
        shell.SetActive(false); //hide the shell.
    }
    void Update()
    {
        // isGameOver = playerCollisionDetectionScript.isGameOver; //check if the game has ended yet

        if(Input.GetKeyUp("left ctrl")) //If the user pressd the control key...
        {
            //... Toggle the cursor on/off:
            if (cursorToggledOff) //If the cursor is currently locked...
            {   
                ToggleCursorOn(); //... unlock it.
            }
            else //If the cursor is currently unlocked...
            {
                ToggleCursorOff(); //... lock it.
            }
        }
    }

/*======================
    Custom Functions
======================*/

  /*-------------
    Game States
  -------------*/
    //starts the gameplay. Runs when the "Start" button is clicked on the main menu.
    public void StartGame()
    {

        /*~~~~~~~~~~~~~~
          Initializers
        ~~~~~~~~~~~~~~*/
        lives = 7; //Set the player's starting lives.
        score = 0; //Set the score to zero.
        isGameOver = false; //The game is running.
        player.transform.position = new Vector3(0.0f, player.transform.position.y, 0.0f);


        /*~~~~~~~~~~~~~~~~
          Function Calls
        ~~~~~~~~~~~~~~~~*/
        //methods & functions
        menuScreen.SetActive(false); //Hide the main menu.
        player.SetActive(true); //show the player
        gameObject.GetComponent<SpawnPickups>().StartSpawning();
        gameObject.GetComponent<SpawnPatterns>().StartSpawning();

        UpdateScore(0); //Update the score display.
        UpdateLives(0); //Update the lives display.
        ToggleCursorOff(); //lock the cursor.

        //Coroutines
        StartCoroutine(ScoreCounter()); //start counting score.
        StartCoroutine(PlayTimer()); //start counting playTime.
    }

    //Resets the game and brings you back to the main menu. Runs when the "Restart" button is clicked in the Game Over Screen.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload the scene.
    }

    //Ends the game. Runs when the player runs out of lives.
    public void GameOver()
    {
        isGameOver = true; //... End the game.
        Debug.Log("Game Over. Time: " + Time.time + ". Score: " + score + "."); //debug message.
        gameOverScreen.SetActive(true); //unhide the Game Over Screen.
        ToggleCursorOn(); //unlock the cursor.
    }

    //shows the help screen. runs when the "Help" button is clicked in the main menu.
    public void DisplayHelpScreen()
    {
      menuScreen.SetActive(false);
      helpScreen.SetActive(true);
      player.SetActive(true);
    }

    //returns to the main menu. runs when the "Close Help" button is clicked in the help menu.
    public void CloseHelpScreen()
    {
      menuScreen.SetActive(true);
      helpScreen.SetActive(false);
      player.SetActive(false);
    }

  /*-----------------
    Lives and Score
  -----------------*/
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

    //updates the lives on the screen, then checks if the player is dead.
    public void UpdateLives(int amount)
    {
        lives += amount; //change the number of lives by the given amount.
        livesText.text = "lives: " + lives; //display the new value.

        if (lives <= 0) //If the player is dead...
        {
            lives = 0; //set lives to 0 (just in case it went negative).
            livesText.text = "lives: " + lives; //display the new value.
            GameOver();
        }
    }

  /*---------------
    Cursor Things
  ---------------*/
    //locks the cursor
    public void ToggleCursorOff()
    {
      Cursor.lockState = CursorLockMode.Locked; //lock it.
      Cursor.visible = false;
      cursorToggledOff = true; //update bool.
    }

    //unlocks the cursor
    public void ToggleCursorOn()
    {
        Cursor.lockState = CursorLockMode.None; //unlock it.
        Cursor.visible = true;
        cursorToggledOff = false; //update bool
    }

  /*------------
    Coroutines
  ------------*/

    //tracks the time (seconds) since the game started.
    IEnumerator PlayTimer()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(0.1f);
            playTime += 0.1f;
        }
    }

    //
    IEnumerator ScoreCounter()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(0.1f);

            int bonusMultiplier = playerCollisionDetectionScript.bonusMultiplier; //retrieve the bonusMultiplier value.
            bool isPlayerHiding = playerControllerScript.isHiding; //check if the player is hiding.

            if (isPlayerHiding) //if the player is hiding...
            {
                UpdateScore(-2 * bonusMultiplier); //... Decrease their score by a little bit.
            }
            else //Otherwise...
            {
                UpdateScore(10 * bonusMultiplier); //... Increase their score.
            }
        }
    }

}
