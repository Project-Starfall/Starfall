using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   private SaveSystem saveSystem;
   private GameConfig gameConfig;
   // button arary

   public void Start()
   {
      gameConfig = saveSystem.LoadConfig();
        // for (#save)
      foreach (Save saveIndex in );
      //  button ()
      // button.name = save[]name
      //
   }

   public void PlayGame ()
    {
       if (saveSystem.LoadGame(File.Exits(filePath)))
       {
           SaveSystem saveSystem = new Save;
       }

      // If no save exists create one 
      if (gameConfig.saveIndex == 0)
        {
            gameConfig.save[Index] = new save();
        }
        // if (config # saves == 0)
        //    gameConfig.save[#saves] = newSave()

        // load save
        SaveSystem.loadGame(this);
        // save[#saves - 1]


        // gameconfig.currentPath = save[#save - 1].path;
        gameConfig.currentPath = Save[saveIndex - 1].configPath;
        
        // This function starts the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit ()
    {
        Application.Quit(); // Quit the Game
        Debug.Log("QUIT"); // Show QUIT
    }

      
}
