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

   //Pulldown
   [SerializeField]
   private Transform pulldownTransform;
   [SerializeField]
   private SpriteRenderer pulldownRenderer;
   [SerializeField]
   private GameObject fallingHookFab;

   //Falling hook
   private GameObject fallingHook;

   public void ladderDown()
   {
      fallingHook = Instantiate(fallingHookFab);
      fallingHook.transform.position = pulldownTransform.position;
      pulldownRenderer.enabled= false;
   }

   public void OnCollisionEnter(Collision collision)
   {
      onEnter();
   }

   public void OnCollisionExit(Collision collision)
   {
      onLeave();
   }


   #region interfaceMethods
   public bool run(Player player)
   {
      ladderAnimator.SetTrigger("pulldown");
      setEnabled(false);
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
