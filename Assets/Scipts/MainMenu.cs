using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SaveSystem;
using static Constants.Scenes;
using static GameTimer;

public class MainMenu : MonoBehaviour
{
    // This is the pause menu
    public GameObject LoadButtonUI;
   

    [SerializeField] PlayableDirector newGameStart;
  // [SerializeField] GameObject timer;
   /* is there a save -> set load button active
    * if not -> disable load button
    * 
    */
   public void Awake()
   {
     // Don
   }

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
       // timer.Onload
        instance.beginTimer();
        newGameStart.Play();
       
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

    public void playTutorial()
    {
        // load game scene
        SceneManager.LoadScene(Tutorial, LoadSceneMode.Single);
    }
}