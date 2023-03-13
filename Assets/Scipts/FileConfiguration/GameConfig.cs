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
   private Save save              { get; set; }

   private int saveIndex          { get; set; } = 0;

  // private string currentSavePath { get; set; } = string.Empty;

   private int masterVolume       { get; set; } = 100;

   private int musicVolume        { get; set; } = 100;

   private int sfxVolume          { get; set; } = 100;

   public GameConfig()
   {
        Save save = new Save();
   }

   // public bool deleteSave(int index)
   // {
   //      // saveSystem.removefile(path)
   //      // remove index from array
   //      // sort the array
   //      // return if successful 
   //    if (index < 0 || index >= save.Count)
   //    {
   //          return false;
   //    }
   // 
   //    string configPath = Application.persistentDataPath + "/game.config";
   //     
   //    if(File.Exists(configPath))
   //    {
   //        File.Delete(configPath);
   // 
   //        return true;
   //    }
   //    else
   //    {
   //        Debug.LogWarning($"Failed to Delete save file: {configPath}");
   //        return false;
   //    }
   // }   
}