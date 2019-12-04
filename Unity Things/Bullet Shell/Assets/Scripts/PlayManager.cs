using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{

    /*NOTE TO SELF: WHEN CLEANING THE GAME'S CODE

        when cleaning the game's code, put this (along with LifeScoreManager) into a script called GameManager. Make that into the main controller of lives, score, and starting/restarting the game.
        Also: there is currently a bug: since Time.time does not reset when loading a new scene, I'll need to use a custom timer for all of the timing things throughout the game's scripts.
            - bug solution: each time the game is started again, start a float PlayTimer coroutine that continuously increments a float playTime to track the number of seconds since the game was started. Then, replace all instances of Time.time in all scripts with a reference to the playTime variable in this script.
    */
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
