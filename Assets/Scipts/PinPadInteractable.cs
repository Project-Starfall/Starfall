using UnityEngine;

public class PinPadInteractable : MonoBehaviour, Interactable
{
   // Pinpad fields
   [SerializeField] GameObject pinpadCanvas;
   [SerializeField] Level1Handler levelHandler;
   private bool active = false; // If UI is active
   
   // Interface fields
   private bool interactEnabled = true;
   private readonly TYPE interactableType = TYPE.Puzzle;
   
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
      if (!interactEnabled) return true;
      pinpadCanvas.transform.localScale = new Vector3(70, 70, 1);
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

   /*******************************************************************
    * Close the pinpad UI
    ******************************************************************/
   public void close()
   {
      pinpadCanvas.transform.localScale = new Vector3(0, 0, 0);
      active = false;
   }

   /*******************************************************************
    * Update gets if escape is pressed and closes UI
    ******************************************************************/
   void Update()
    {
      if (!active) return;

      if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.RightShift))
      {
         close();
      }
   }
}
