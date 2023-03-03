using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;


// Converts from binary
[System.Serializable]
public class GameConfig
{
   private List<Save> saves       { get; set; } = new List<Save>();

   private int saveIndex          { get; set; } = 0;

   private string currentSavePath { get; set; } = string.Empty;

   private int masterVolume       { get; set; } = 100;

   private int musicVolume        { get; set; } = 100;

   private int sfxVolume          { get; set; } = 100;

   public GameConfig()
   {
        Save save = new Save();
   }

   public bool deleteSave(int index)
   {
        // saveSystem.removefile(path)
        // remove index from array
        // sort the array
        // return if successful 
      if (index < 0 || index >= saves.Count)
      {
            return false;
      }

      string configPath = Application.persistentDataPath + "/game.config";
       
      if(File.Exists(configPath))
      {
          File.Delete(configPath);

          return true;
      }
      else
      {
          Debug.LogWarning($"Failed to Delete save file: {configPath}");
          return false;
      }
   }

   public Save newSave()
   {
      SaveSystem saveSystem= new SaveSystem();
      saveIndex++;
      string name = $"save{saveIndex}";
      // path = "{persistantpath}\saves\save{name}.starfall"
      // new save(path, name);
      // savesystem.saveGame(new path, save);
      
      // add to array
      // 
      return new Save();
   }


    
}