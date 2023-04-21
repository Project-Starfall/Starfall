using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Converts from binary
[System.Serializable]
public class PlayerData
{
    public int Level        { get; set; }
    public bool hasMap      { get; set; }
    public bool hasGrapple  { get; set; }
    public float[] Position { get; set; }

    public static PlayerData FromPlayer(Player player)
    {
        return new PlayerData
        {
            Level = player.level,

            Position = new float[]
            {
                player.transform.position.x,
                player.transform.position.y,
                player.transform.position.z
            }
        };
    }
}
