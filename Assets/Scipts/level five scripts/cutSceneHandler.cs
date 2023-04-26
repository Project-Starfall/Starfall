using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Constants.Scenes;
using static GameTimer;

public class cutSceneHandler : MonoBehaviour
{
   [SerializeField] TMP_Text timeText;
   private bool captureInput = false;
   audioManager audioManager;
   float volume;
   
    private void Awake()
    {
        FindObjectOfType<audioManager>().musicFadeOut("levelFiveMusic");
        FindObjectOfType<audioManager>().stop("levelFiveMusic");
        FindObjectOfType<audioManager>().play("endingMusic");
        FindObjectOfType<audioManager>().musicFadeIn("endingMusic");
        audioManager = FindObjectOfType<audioManager>();
    }

   private void Start()
   {

   }

   private void Update()
   {
      if(captureInput)
      {
         if(Input.anyKeyDown)
         {
            returnToMenu();
         }
      }
   }

   public void fadeMusic()
   {
      
   }

   public void getTime()
   {
      timeText.text = instance.timeText;
      instance.endTimer();
   }

   public void showTime()
    {
        audioManager.musicFadeOut("endingMusic");
        audioManager.stop("endingMusic");
        captureInput = true;
    }

   public void returnToMenu()
   {
        audioManager.play("menuMusic");
        audioManager.musicFadeIn("menuMusic");
        SceneManager.LoadScene(Menu, LoadSceneMode.Single);
   }

   // actually fade the music
   public IEnumerator fader()
   {
      yield return null;
   }
}
