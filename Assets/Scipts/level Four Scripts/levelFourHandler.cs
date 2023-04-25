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
    [SerializeField] PlayableDirector endTimeline;
    [SerializeField] PlayableDirector startTimeline;
    // Player
    [SerializeField] Player player;
    [SerializeField] PlayerMovement playerMovement;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        // transition background music into level four music
        //FindObjectOfType<audioManager>().musicFadeOut("menuMusic"); // only if save game feature is added
        FindObjectOfType<audioManager>().play("levelFourMusic");
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
        playerMovement.DisableMovement();
        startTimeline.Play();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement.DisableMovement();
        endTimeline.Play();
    }

    public void endLevelFour()
    {
        SceneManager.LoadScene(Transistion, LoadSceneMode.Single);
    }

    public void allowMove()
    {
        playerMovement.EnableMovement();
    }
}
