using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   private SaveSystem saveSystem;
   private GameConfig gameConfig;
   // button arary

   public void Start()
   {
        // for (#save)
      //  button ()
      // button.name = save[]name
      //
   }

   public void PlayGame ()
   {
      // If no save exists create one 
      // if (config # saves == 0)
      //    gameConfig.save[#saves] = newSave()

      // load save
      // save[#saves - 1]


      // gameconfig.currentPath = save[#save - 1].path;
        
      // This function starts the game
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

    public void Quit ()
    {
       Application.Quit(); // Quit the Game
       Debug.Log("QUIT"); // Show QUIT
    }

      
}
