using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderPulldown : MonoBehaviour, Interactable
{
   // Interface methods
   private bool interactEnabled = true;
   private readonly TYPE interactableType = TYPE.Powerup;

   //Ladder
   [SerializeField]
   private GameObject ladder;
   [SerializeField]
   private Animator ladderAnimator;

   #region interfaceMethods
   public bool run(Player player)
   {
      //
      return true;
   }

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

   public void setEnabled(bool enabled)
   {
      interactEnabled= enabled;
   }
#endregion
}
