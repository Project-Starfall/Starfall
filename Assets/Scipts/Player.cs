using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level = 1; // Current level of the game

    public void SaveGame()
    {
        SaveSystem.SaveGame(this); // The save of the player
    }

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
    }
}