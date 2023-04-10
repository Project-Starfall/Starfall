using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using static SaveSystem;

public class LevelThreeHandler : MonoBehaviour
{
    // Camera
    [SerializeField] CinemachineConfiner2D cameraConfine;

    // Player
    [SerializeField] Player player;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        // transition background music into level one music
        FindObjectOfType<audioManager>().musicFadeOut("menuMusic");
        FindObjectOfType<audioManager>().musicFadeOut("levelTwoMusic");
    }

    // Start is called before the first frame update
    void Start()
    {
        // save the player data
        SaveGame(player);
        Debug.Log("Saving the player...");
        // load player save
        LoadGame();
        Debug.Log("Loading the game...");
    }
}
