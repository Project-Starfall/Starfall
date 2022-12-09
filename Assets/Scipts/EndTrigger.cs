using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public EndTrigger gameManager;

    public GameObject completeLevelUI;

    public void CompleteLevel ()
    {
        completeLevelUI.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("DETECTS TRIGGER");
        SceneManager.LoadScene(2);
        gameManager.CompleteLevel();
    }
}
