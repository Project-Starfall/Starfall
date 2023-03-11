using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SaveSystem;

public class MainMenu : MonoBehaviour
{
    // This is the pause menu
    public GameObject LoadButtonUI;

    // Start
    /* is there a save -> set load button active
     * if not -> disable load button
     * 
     */
    
    // newgame(){
    // ss.loadgame()
    // playGame()}

    public void PlayGame ()
   {        
      // This function starts the game
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

    public void Quit ()
    {
       Application.Quit(); // Quit the Game
       Debug.Log("QUIT"); // Show QUIT
    }    

    // LoadButton_OnClick()
    /* is there a save?
     * Okay, i have a save | loadGame()
     * so i load to that save
     * but if dont have a save
     * create a new save | newSave()
     */

    public void LoadGame()
    {
        // This function Loads a saved game
        if(SaveSystem.saveExist())
        {
            LoadButtonUI.SetActive(true);
            PlayerData playerdata = SaveSystem.LoadGame();
            // SaveSystem.LoadGame(SceneManager.GetActiveScene().path);
            SceneManager.LoadScene("", LoadSceneMode.Single);
        }
        else
        {
            LoadButtonUI.SetActive(false);
            Debug.Log("No saved game found.");
        }
    }

}
