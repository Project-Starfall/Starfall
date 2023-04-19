using UnityEngine;

public class PinPadButton : MonoBehaviour
{

   [SerializeField] char number; // The number on the button
   [SerializeField] PinPadHandler pinPadHandler; // The pinpad handler

   /*******************************************************************
    * Called when a pinpad button is pressed
    ******************************************************************/
   private void OnMouseDown()
   {
      pinPadHandler.numberInput(number);
      FindObjectOfType<audioManager>().play("pinPress");
      // TODO: play button click audio
    }
}
