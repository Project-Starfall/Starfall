using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save
{
   private string filePath  { get; set; }
   private string saveName  { get; set; }
   private int currentLevel { get; set; }
   private ulong time       { get; set; }
   
   public Save() {
      time         = 0;
      currentLevel = 0;
      saveName     = string.Empty;
      filePath     = string.Empty;
   }

   public Save(string filePath, string saveName, int currentLevel = 0, ulong time = 0)
   {
      this.filePath     = filePath;
      this.saveName     = saveName;
      this.currentLevel = currentLevel;
      this.time         = time;
   }


}
