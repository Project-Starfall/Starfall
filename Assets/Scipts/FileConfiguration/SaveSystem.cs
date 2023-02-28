using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

/// <summary>
/// Defines a system for saving/loading player information.
/// </summary>
public class SaveSystem
{

    readonly string configPath = Application.persistentDataPath + "/game.config";

    /// <summary>
    /// Saves a player's data.
    /// </summary>
    /// <param name="player">Current Player object.</param>
    public void SaveGame(Player player, string savePath)
    {
        // Convert to binary and create a file
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(savePath, FileMode.Create);

        PlayerData data = new PlayerData(player);

        // Close the file
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// Loads a game from a save file.
    /// </summary>
    /// <returns></returns>
    public PlayerData LoadGame(string savePath)
    {
        // Check if the file exists
        if (File.Exists(savePath))
        {
            // Convert to binary and open file
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(savePath, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close(); // close the stream

            return data;
        }
        else
        {
            Debug.LogError($"Save game not found in the {savePath}");
            return null;
        }
    }


    public void SaveConfig(GameConfig gameConfig)
    {
      //TODO: put in try catches
        // Convert to binary and create a file
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(configPath, FileMode.Create);

        //GameConfig data = new GameConfig(gameConfig);

        // Close the file
        formatter.Serialize(stream, gameConfig);
        stream.Close();
    }

    public GameConfig LoadConfig()
    {
        // Check if the file exists
        if (File.Exists(configPath))
        {
            // Convert to binary and open file
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(configPath, FileMode.Open);

            GameConfig data = formatter.Deserialize(stream) as GameConfig;

            stream.Close(); // close the stream

            return data;
        }
        else
        {
         try
         {
            FileStream stream = new FileStream(configPath, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            GameConfig gameConfig = new GameConfig();
            formatter.Serialize(stream, gameConfig);
            stream.Close();

            return gameConfig;
         }
         catch (Exception)
         {
            Debug.LogError("Failed to create new game config!");
            return null;
         }
        }
    }

}
