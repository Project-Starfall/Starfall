using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInteractable : MonoBehaviour, Interactable
{
   // Note fields
   [SerializeField] GameObject note; 
   private bool active = false; // if UI is active

   // Interface fields
   private readonly TYPE interactableType = TYPE.Item;
   private bool interactEnabled = true;

   /*******************************************************************
    * Update gets if escape is pressed and closes UI
    ******************************************************************/
   private void Update()
   {
      if (!active) return;

      if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.RightShift))
      {
         note.transform.localScale = new Vector2(0, 0);
         active = false;
      }

   }

   #region InterfaceMethods
   /*******************************************************************
    * Returns the type of the Interactable
    ******************************************************************/
   public TYPE getType()
   {
      return interactableType;
   }

   /*******************************************************************
    * Returns if the Interactable is enabled
    ******************************************************************/
   public bool isEnabled()
   {
      return interactEnabled;
   }

   /*******************************************************************
    * Call when the player enters interactable range
    ******************************************************************/
   public void onEnter()
   {
      throw new System.NotImplementedException();
   }

   /*******************************************************************
    * Call when the player leaves interactable range
    ******************************************************************/
   public void onLeave()
   {
      throw new System.NotImplementedException();
   }

   /*******************************************************************
    * Call when the player presses interactable key in range
    ******************************************************************/
   public bool run(Player player)
   {
      note.transform.localScale = new Vector2(28, 28);
      active = true;
      return true;
   }

   /*******************************************************************
    * Set if the interactable is enabled
    ******************************************************************/
   public void setEnabled(bool enabled)
   {
      interactEnabled = enabled;
   }
   #endregion
}
