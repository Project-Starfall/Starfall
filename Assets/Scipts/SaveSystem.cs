using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame(Player player)
    {
        // Convert to binary and create a file
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SaveGame.StarFall";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        // Close the file
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadGame()
    {
        string path = Application.persistentDataPath + "/SaveGame.StarFall";

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
}
