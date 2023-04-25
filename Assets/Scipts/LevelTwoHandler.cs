using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using static SaveSystem;
using static Constants.Scenes;

public class LevelTwoHandler : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    private PlayableDirector startTimeline;
    [SerializeField]
    private PlayableDirector endTimeline;

    [SerializeField]
    private PlayerMovement playerMovement;


    public void Awake()
    {
        Application.targetFrameRate = 60;

        // transition background music into level two music
        //FindObjectOfType<audioManager>().musicFadeOut("menuMusic"); // only if save game feature is added
        FindObjectOfType<audioManager>().musicFadeOut("levelOneMusic");
        FindObjectOfType<audioManager>().stop("levelOneMusic");
        FindObjectOfType<audioManager>().play("levelTwoMusic");
        FindObjectOfType<audioManager>().musicFadeIn("levelTwoMusic");
    }

    // Start is called before the first frame update
    void Start()
    {
        // save the player data
        SaveGame(player);
        Debug.Log("Saving the player level 2...");
        // load player save
        LoadGame();
        Debug.Log("Loading the game in level 2...");
        playerMovement.disableMovement = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement.disableMovement = true;
        endTimeline.Play();
        StartCoroutine(delayClose());

    }

    public void allowMove()
    {
        playerMovement.disableMovement = false;
    }

    private IEnumerator delayClose()
    {
        SaveGame(player); // Saving the player data
        Debug.Log("Saving the Player...");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(Transistion, LoadSceneMode.Single);
        yield return null;
    }
}
