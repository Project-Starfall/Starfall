using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int seed;
    public int level = 1; // Current level of the game

    public DateTime time { get; internal set; }

    public bool hasMap { get; set; }
    public bool hasGrapple { get; set; }

    public void LoadGame()
    {
        // Call the player data and saves it
        PlayerData data = SaveSystem.LoadGame();

        // The number/stage of the level
        level = data.level;

        // Save the player position
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

        hasMap = data.hasMap;

        hasGrapple = data.hasGrapple;
    }
}