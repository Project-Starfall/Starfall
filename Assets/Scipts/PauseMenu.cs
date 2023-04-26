using UnityEngine;
using UnityEngine.SceneManagement;
using static SaveSystem;
using static GameTimer;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    // player field
    [SerializeField]
    Player player;
    [SerializeField] PlayerMovement playerMovement;

    // Set game to false
    public static bool GameIsPaused = false;
    public bool isUIOpen { get; set; } = false;
    // This is the pause menu
    public GameObject PauseMenuUI;
    [SerializeField] TMP_Text timerText; 

    // Update is called once per frame
    void Update()
    {
      if (isUIOpen) return;
        // Pause if escape is pressed down
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // if game is pause resume game
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // This function resumes the game
    public void Resume()
    {
        // The game is not paused
        PauseMenuUI.SetActive(false);
        // Resume time
        playerMovement.EnableMovement();
        Time.timeScale = 1f;
        // Play the game
        GameIsPaused = false;
    }

    // This function pauses the game
    void Pause()
    {
        // Pause the game
        PauseMenuUI.SetActive(true);
        playerMovement.DisableMovement();
        // Freeze the game objects
        Time.timeScale = 0f;
        // The game is paused
        timerText.text = instance.timeText;
        GameIsPaused = true;
    }

    public void loadMenu()
    {
        //Time.timeScale = 0f;
        // This function starts the game
        Time.timeScale = 1f;
        FindObjectOfType<audioManager>().play("menuMusic");
        FindObjectOfType<audioManager>().musicFadeIn("menuMusic");
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        Debug.Log("Loading Menu...");
    }

    public void QuitGame()
    {
        SaveGame(player);
        Debug.Log("Saving the Player...");
        Debug.Log("Quitting Game...");
        Application.Quit(); // Quit the Game
    }
}
