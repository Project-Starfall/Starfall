using Mono.Cecil;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PinPadHandler : MonoBehaviour
{

   [SerializeField] TMP_Text pinPadScreen; // The text on the pinpad screen
   [SerializeField] Level1Handler handler; // The handler for level 1
   [SerializeField] PinPadInteractable pinpad; // The pinpad
   [SerializeField] Light2D[] lights; // Interior warehouse lights
   private char[] num = {'_', '_', '_', '_'}; // digits buffer to load on screen
   private int currentIndex = 0; // where the cursor is in the buffer
   bool funnyflag = false; // flash semaphore 
   
   // Start is called before the first frame update
    void Start()
    {
      InvokeRepeating("flashCursor",0.5f ,0.5f);
      lights[0].enabled= true;
      lights[1].enabled = false;
    }

   /*******************************************************************
    * Screen refresh, puts buffer onto the screen
    ******************************************************************/
   void FixedUpdate()
   {
      pinPadScreen.text = $"{num[0]} {num[1]} {num[2]} {num[3]}";
   }

   /*******************************************************************
    * Processes the pressed button and updates the buffer
    ******************************************************************/
   public void numberInput(char key)
   {
      // DEL key was pressed
      if(key == 'd')
      {
         if (currentIndex == 0) return;
         num[currentIndex - 1] = '_';
         if(currentIndex != 4) num[currentIndex] = '_';
         currentIndex--;
      }
      // CLR key was pressed
      else if (key == 'c')
      {
         num[0] = '_';
         num[1] = '_';
         num[2] = '_';
         num[3] = '_';
         currentIndex = 0;
      }
      //Enter key was pressed
      else if(key == 'e')
      {
         if (currentIndex != 4) return;
         if (checkCode()) pinSuccess();
         //else ; TODO: SOUND: Play Access Denied sound
         num[0] = '_';
         num[1] = '_';
         num[2] = '_';
         num[3] = '_';
         currentIndex = 0;
      }
      // input a number if space available
      else
      {
         if (currentIndex == 4) return;
         num[currentIndex] = key;
         currentIndex++;
      }
   }

   /*******************************************************************
    * Compares the generated pin to the entered one
    ******************************************************************/
   public bool checkCode()
   {
      char[] pin = handler.getPinpad();
      for(int i = 0; i < 4; i++)
      {
         if (pin[i] != num[i]) return false;
      }
      return true;
   }

   /*******************************************************************
    * Called when the pin is succesfully entered
    ******************************************************************/
   void pinSuccess()
   {
      lights[0].enabled = false;
      lights[1].enabled = true;
      // TODO: SOUND: play success noise
      StartCoroutine(delayedClose());
      handler.openOfficeDoorSequence();
      
   }

   /*******************************************************************
    * Delays closing the pinpad by 1 second
    ******************************************************************/
   IEnumerator delayedClose()
   {
      yield return new WaitForSeconds(1);
      pinpad.close();
      yield return null;
   }

   /*******************************************************************
    * Coroutine to flash the cursor
    ******************************************************************/
   void flashCursor()
   {
      if (currentIndex == 4) return;
      if (funnyflag)
      {
         num[currentIndex] = '_';
         funnyflag = false;
      }
      else
      {
         num[currentIndex] = ' ';
         funnyflag   = true;
      }
   }
}
