using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Converts from binary
[System.Serializable]
public class GameConfig
{
    public int Level;           // The number of levels in the game
    public string SaveFileName; // The Name of the save file
    public DateTime Time;       // The amount of time it took the player to save the game
    
    public GameConfig(string saveFileName, DateTime time, int level = 0)
    {
        this.Level        = level;        // The player in the level
        this.SaveFileName = saveFileName; // The player save file
        this.Time         = time;         // The player time count
    }
}