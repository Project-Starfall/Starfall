using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Constants.Scenes;

public class cutSceneHandler : MonoBehaviour
{
   [SerializeField] GameTimer timer;


   public void showTime()
   {

   }

   public void returnToMenu()
   {
      SceneManager.LoadScene(Menu, LoadSceneMode.Single);
   }
}
