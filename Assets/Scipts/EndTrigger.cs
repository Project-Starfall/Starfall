using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public EndTrigger gameManager;

    public void CompleteLevel ()
    {
        Debug.Log("LEVEL WON!");
        Debug.Log("LEVEL WON!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("DETECTS TRIGGER");
        SceneManager.LoadScene(2);
        gameManager.CompleteLevel();
    }
}
