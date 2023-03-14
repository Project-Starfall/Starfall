using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Set game to false
    public static bool GameIsPaused = false;
    // This is the pause menu
    public GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        // Pause if escape is pressed down
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // if game is pause resume game
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // This function resumes the game
    public void Resume()
    {
        // The game is not paused
        PauseMenuUI.SetActive(false);
        // Resume time
        Time.timeScale = 1f;
        // Play the game
        GameIsPaused = false;
    }

    // This function pauses the game
    void Pause()
    {
        // Pause the game
        PauseMenuUI.SetActive(true);
        // Freeze the game objects
        Time.timeScale = 0f;
        // The game is paused
        GameIsPaused = true;
    }

    public void loadMenu()
    {
        //Time.timeScale = 0f;
        // This function starts the game
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        Debug.Log("Loading Menu...");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit(); // Quit the Game
    }
}
