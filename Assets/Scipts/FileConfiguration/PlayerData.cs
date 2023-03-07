using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using System;

// Converts from binary
[System.Serializable]
public class PlayerData
{
    public int level;        // The number of levels in the game
    public float[] position; // The position of the player
    public DateTime time { get; internal set; }
    public List<string> items;
    public List<string> powerUps;

    public PlayerData(Player player)
    {
        level    = player.level;    // The player in the level
        position = new float[3]; // The new position of the player
        time     = player.time;
        items    = new List<string>();
        powerUps = new List<string>();

        // Position is transformed to unity position
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
