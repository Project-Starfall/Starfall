using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PinPadInteractable : MonoBehaviour, Interactable
{
   [SerializeField] GameObject pinpadCanvas;
   [SerializeField] Level1Handler levelHandler;
   private bool active = false;
   private bool interactEnabled = true;
   private readonly TYPE interactableType = TYPE.Puzzle;
   public TYPE getType()
   {
      return interactableType;
   }

   public bool isEnabled()
   {
      return interactEnabled;
   }

   public void onEnter()
   {
      throw new System.NotImplementedException();
   }

   public void onLeave()
   {
      throw new System.NotImplementedException();
   }

   public bool run(Player player)
   {
      if (!interactEnabled) return true;
      pinpadCanvas.transform.localScale = new Vector3(70, 70, 1);
      active = true;
      return true;
   }

   public void close()
   {
      pinpadCanvas.transform.localScale = new Vector3(0, 0, 0);
      active = false;
   }

   public void setEnabled(bool enabled)
   {
      interactEnabled = enabled;
   }

    // Update is called once per frame
    void Update()
    {
      if (!active) return;

      if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.RightShift))
      {
         pinpadCanvas.transform.localScale = new Vector3(0, 0, 0);
         active = false;
      }
   }
}
