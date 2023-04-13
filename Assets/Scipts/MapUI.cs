using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    [SerializeField] public PauseMenu PauseMenu;

    // Set game to false
    public static bool MapIsOpen = false;
    public bool isUIOpen { get; set; } = false;
    // This is the Map Popup
    public GameObject Map;

    // Update is called once per frame
    void Update()
    {
        if (isUIOpen) return;
        // Close map if open when escape is pressed down
        if (Input.GetKeyDown(KeyCode.Escape) && PauseMenu.isUIOpen)
        {
            HideMap();
        }
        // Pause if "M" is pressed down
        if (Input.GetKeyDown(KeyCode.M))
        {
            // if map is opened resume game
            if (MapIsOpen)
            {
                HideMap();
            }
            else
            {
                ShowMap();
            }
        }
    }

    // This function resumes the game
    public void HideMap()
    {
        // The Map is on showing
        Map.SetActive(false);
        // Resume time
        Time.timeScale = 1f;
        // Play the game
        MapIsOpen = false;
    }

    // This function pauses the game and open's the map
    void ShowMap()
    {
        // Pause the game
        Map.SetActive(true);
        // Freeze the game objects
        Time.timeScale = 0f;
        // The game is paused
        MapIsOpen = true;
    }
}