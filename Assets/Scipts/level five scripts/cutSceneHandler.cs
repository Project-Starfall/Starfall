using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Constants.Scenes;

public class cutSceneHandler : MonoBehaviour
{
   [SerializeField] GameTimer timer;

    private void Awake()
    {
        FindObjectOfType<audioManager>().musicFadeOut("levelFiveMusic");
        FindObjectOfType<audioManager>().stop("levelFiveMusic");
        FindObjectOfType<audioManager>().play("endingMusic");
        FindObjectOfType<audioManager>().musicFadeIn("endingMusic");
    }

    public void showTime()
   {

   }

   public void returnToMenu()
   {
        FindObjectOfType<audioManager>().play("menuMusic");
        FindObjectOfType<audioManager>().musicFadeIn("menuMusic");
        SceneManager.LoadScene(Menu, LoadSceneMode.Single);
   }
}
