using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

/// <summary>
/// Defines a system for saving/loading player information.
/// </summary>
public static class SaveSystem
{
    public static readonly string path = Path.Combine(Application.persistentDataPath, "/SaveGame.config");

    /// <summary>
    /// Checks if a save file exists.
    /// </summary>
    /// <returns></returns>
    public static bool saveExist()
    {
        return File.Exists(path);
    }

    /// <summary>
    /// Saves a player's data.
    /// </summary>
    /// <param name="player">Current Player object.</param>
    public static void SaveGame(Player player)
    {
        // Convert to binary and create a file
        FileStream stream   = new FileStream(path, FileMode.Create);
        BinaryWriter writer = new BinaryWriter(stream);

        PlayerData data = PlayerData.FromPlayer(player);
        writer.Write(data.Level);
        writer.Write(data.hasMap);
        writer.Write(data.hasGrapple);

        if (data.Position == null)
        {
            writer.Write(0);
        }
        else
        {
            writer.Write(data.Position.Length);
            foreach (var value in data.Position)
            {
                writer.Write(value);
            }
        }

        // Close the file
        stream.Close();
    }

    /// <summary>
    /// Loads a game from a save file.
    /// </summary>
    /// <returns>The PlayerData object containing the saved data.</returns>
    public static PlayerData LoadGame()
    {
        // Check if the file exists
        if (!File.Exists(path))
        {
            Debug.LogError("Save game not found in the " + path);
            return null;
        }

            // Convert to binary and open file
            FileStream stream   = new FileStream(path, FileMode.Open);
            BinaryReader reader = new BinaryReader(stream);

            PlayerData data = new PlayerData();
            data.Level      = reader.ReadInt32();
            data.hasGrapple = reader.ReadBoolean();
            data.hasMap     = reader.ReadBoolean();

            int length = reader.ReadInt32();
            data.Position = new float[length];
            for (int index = 0; index < length; index++)
            {
                data.Position[index] = reader.ReadSingle();
            }

            stream.Close(); // close the stream

            return data;
    }

    /// <summary>
    /// Resumes the players latest saved data
    /// </summary>
    /// <param name="player">The current Player object.</param>
    public static void ResumeGame(Player player)
    {
        if(saveExist())
        {
            PlayerData data = LoadGame();

            // Restore the player data
            player.transform.position = new Vector3(data.Position[0], data.Position[1], data.Position[2]);
        }
        else
        {
            Debug.LogError("Cannot resume game. Save game not found at " + path);
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
