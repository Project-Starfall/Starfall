using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Defines a system for saving/loading player information.
/// </summary>
public static class SaveSystem
{

    public static string path { get; set; } = Application.persistentDataPath + "/SaveGame.StarFall";

    /// <summary>
    /// Saves a player's data.
    /// </summary>
    /// <param name="player">Current Player object.</param>
    public static void SaveGame(Player player)
    {
        // Convert to binary and create a file
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

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
        if (File.Exists(path))
        {
            // Convert to binary and open file
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close(); // close the stream

            return data;
        }
        else
        {
            Debug.LogError("Save game not found in the " + path);
            return null;
        }
    }

    public static void SaveData(GameConfig gameConfig)
    {
        // Convert to binary and create a file
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        //GameConfig data = new GameConfig(gameConfig);

        // Close the file
        formatter.Serialize(stream, gameConfig);
        stream.Close();
    }

    public static GameConfig LoadData()
    {
        // Check if the file exists
        if (File.Exists(path))
        {
            // Convert to binary and open file
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameConfig data = formatter.Deserialize(stream) as GameConfig;

            stream.Close(); // close the stream

            return data;
        }
        else
        {
            Debug.LogError("Save game files not found in the " + path);
            return null;
        }
    }

}
