using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinPadButton : MonoBehaviour
{

   [SerializeField] char number;
   [SerializeField] PinPadHandler pinPadHandler;

   private void OnMouseDown()
   {
      pinPadHandler.numberInput(number);
      //play button click audio
   }
}
