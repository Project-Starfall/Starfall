using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

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
}
