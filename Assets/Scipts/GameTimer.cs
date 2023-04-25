using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
   public static GameTimer instance;

   private TimeSpan time;
   private Text timeText;
   private bool timerGoing;
   private float elapsedTime;

   private void Awake()
   {
      instance = this;
   }

   private void Start()
   {
      timeText.text = "Time: 00:00:00";
      timerGoing = false;
   }

   public void beginTimer()
   {
      timerGoing = true;
      elapsedTime = 0f;

      StartCoroutine(updateTimer());
   }

   public void endTimer()
   {
      timerGoing = false;
   }

   private IEnumerator updateTimer()
   {
      while (timerGoing)
      {
         elapsedTime += Time.deltaTime;
         time = TimeSpan.FromSeconds(elapsedTime);
         string timePlayingStr = $"Time: {time:mm':'ss'.'ff}";
         timeText.text = timePlayingStr;

         yield return null;
      }
   }
}
