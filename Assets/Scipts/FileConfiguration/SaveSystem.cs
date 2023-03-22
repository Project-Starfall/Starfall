using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

/// <summary>
/// Defines a system for saving/loading player information.
/// </summary>
public static class SaveSystem
{
    public static string Path { get; set; } = Application.persistentDataPath + "/SaveGame.config";

    public static bool saveExist()
    {
        return File.Exists(Path);
    }

    /// <summary>
    /// Saves a player's data.
    /// </summary>
    /// <param name="player">Current Player object.</param>
    public static void SaveGame(Player player)
    {
        // Convert to binary and create a file
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(Path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        // Close the file
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// Loads a game from a save file.
    /// </summary>
    /// <returns></returns>
    public static PlayerData LoadGame()
    {
        // Check if the file exists
        if (File.Exists(Path))
        {
            // Convert to binary and open file
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(Path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close(); // close the stream

            return data;
        }
        else
        {
            Debug.LogError("Save game not found in the " + Path);
            return null;
        }
    }

    /*
    public static void SaveConfig(GameConfig gameConfig, string configPath)
    {
        try
        {
            // Convert to binary and create a file
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(configPath, FileMode.Create);
    
            //GameConfig data = new GameConfig(gameConfig);
    
            // Close the file
            formatter.Serialize(stream, gameConfig);
            stream.Close();
        }
        catch (Exception)
        {
            Debug.LogError("Failed to save new game config!");
        }
        
    }
    
    public static GameConfig LoadConfig(string configPath)
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
    */
   // // Remove a save file from the disk
   // public static bool removeFile(string savePath)
   // {
   //     if(File.Exists(savePath))
   //     {
   //         File.Delete(savePath);
   //         return true;
   //     }
   //     else
   //     {
   //         Debug.LogError($"File not found: {savePath}");
   //         return false;
   //     }
   // }
}
