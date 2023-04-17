using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using static SaveSystem;
using static Constants.Scenes;
using UnityEngine.SceneManagement;

public class levelFourHandler : MonoBehaviour
{
    // Camera
    [SerializeField] CinemachineConfiner2D cameraConfine;
    [SerializeField] PlayableDirector endTimeline;
    // Player
    [SerializeField] Player player;
    [SerializeField] PlayerMovement playerMovement;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        // transition background music into level one music
        FindObjectOfType<audioManager>().musicFadeOut("menuMusic");
        FindObjectOfType<audioManager>().musicFadeIn("levelFourMusic");
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement.DisableMovement();
        endTimeline.Play();
    }

    public void endLevelFour()
    {
        SceneManager.LoadScene(LevelFive, LoadSceneMode.Single);
    }
}
