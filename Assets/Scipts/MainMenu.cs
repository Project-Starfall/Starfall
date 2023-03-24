using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SaveSystem;
using static Constants.Scenes;

public class MainMenu : MonoBehaviour
{
    // This is the pause menu
    public GameObject LoadButtonUI;

    // Load button fields
    [SerializeField]
    Button loadButton;
    [SerializeField]
    Image loadBkgrdImage;
    [SerializeField]
    TMP_Text loadButtonTxt;


    // Start
    public void Start()
    {
        if (!saveExist())
        {
            loadButton.enabled = false;
            loadBkgrdImage.enabled = false;
            loadButtonTxt.faceColor = new Color(156,156,156);
        }
        else
        {
            loadButton.enabled = true;
            loadBkgrdImage.enabled = true;
            loadButtonTxt.faceColor = new Color(255, 255, 255);
        }
    }
    /* is there a save -> set load button active
     * if not -> disable load button
     * 
     */

    // newgame()
    public void NewGame ()
    {
        // Resets the player's postions to zero
        // transform.position = new Vector3(0,0,0);
        // Reset map

        // Reset grapple

        if (File.Exists(SaveSystem.Path))
        {
            File.Delete(SaveSystem.Path);
        }

        // load game scene
        SceneManager.LoadScene(Tutorial, LoadSceneMode.Single);
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

    public void LoadGameMenu()
    {
        PlayerData playerData;
        // This function Loads a saved game
        if(saveExist())
        {
            LoadButtonUI.SetActive(true);
            playerData = LoadGame();
            // SaveSystem.LoadGame(SceneManager.GetActiveScene().path);
            // load scene ()
            SceneManager.LoadScene(playerData.level);
        }
        else
        {
            LoadButtonUI.SetActive(false);
            Debug.Log("No saved game found.");
        }
    }

}