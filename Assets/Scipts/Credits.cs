using UnityEngine;

public class Credits : MonoBehaviour
{
    public void Quit ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Get the Escape key via keycode emun
            Application.Quit(); // Quit the Game
        Debug.Log("QUIT"); // Show QUIT
    }
}
