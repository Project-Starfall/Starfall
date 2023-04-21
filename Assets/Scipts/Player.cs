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
        level = data.Level;

        // Save the player position
        Vector3 position;
        position.x = data.Position[0];
        position.y = data.Position[1];
        position.z = data.Position[2];
        transform.position = position;

        hasMap = data.hasMap;

        hasGrapple = data.hasGrapple;
    }
}