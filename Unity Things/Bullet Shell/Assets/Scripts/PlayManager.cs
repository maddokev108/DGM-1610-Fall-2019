using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


    /*NOTE TO SELF: WHEN CLEANING THE GAME'S CODE (This one is done)

        when cleaning the game's code, put this (along with LifeScoreManager) into a script called GameManager. Make that into the main controller of lives, score, and starting/restarting the game.
        Also: there is currently a bug: since Time.time does not reset when loading a new scene, I'll need to use a custom timer for all of the timing things throughout the game's scripts.
            - bug solution: each time the game is started again, start a float PlayTimer coroutine that continuously increments a float playTime to track the number of seconds since the game was started. Then, replace all instances of Time.time in all scripts with a reference to the playTime variable in this script.
    */

public class PlayManager : MonoBehaviour
{
    // private GameObject menuScreen; //menu screen

    // internal float playTime; //meant to be like Time.time, but only starts running when the game actually starts being played. Accuracy to 0.00001 seconds.

    // void Start()
    // {
    //     menuScreen = GameObject.Find("Menu Screen"); //Find the Menu Screen.
    // }

    // public void StartGame()
    // {
    //     menuScreen.SetActive(false);
    //     StartCoroutine(PlayTimer());
    // }

    // public void RestartGame()
    // {
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    // }

    // IEnumerator PlayTimer()
    // {
    //     yield return new WaitForSeconds(0.00001f);
    //     playTime += 0.00001f;
    // }

}
