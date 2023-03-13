using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LadderPulldown : MonoBehaviour, Interactable
{
   // Interface methods
   public bool interactEnabled = true;
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
   [SerializeField]
   private GrappleRope grappleRope;

   //Fade
   [SerializeField]
   private Material glowMaterial;
   private bool isFade = false;
   private bool fadeIn = false;
   private float fade = 1.0f;

   //Falling hook
   private GameObject fallingHook;

   public void ladderDown()
   {
      grappleRope.enabled = false;
      fallingHook = Instantiate(fallingHookFab);
      fallingHook.transform.position = pulldownTransform.position;
      pulldownRenderer.enabled= false;
   }

   public void Update()
   {
      if (isFade)
      {
         if (!fadeIn) {
            fade -= Time.deltaTime;
            if (fade <= 0f)
            {
               fade = 0f;
               isFade = false;
            }
         } 
         else
         {
            fade += Time.deltaTime;
            if(fade >= 1f)
            {
               fade = 1f;
               isFade = false;
            }
         }
         glowMaterial.SetFloat("_Fade", fade);
      }
   }

   public void OnTriggerEnter2D(Collider2D collision)
   {
      Debug.Log("Entered Collider");
      onEnter();
   }

   public void OnTriggerExit2D(Collider2D collision)
   {
      Debug.Log("Exited Collider");
      onLeave();
   }


   #region interfaceMethods
   public bool run(Player player)
   {
      grappleRope.grapplePoint = pulldownTransform;
      grappleRope.grappleDistanceVector = (Vector2) (grappleRope.grapplePoint.position - grappleRope.firePoint.position);
      grappleRope.enabled = true;
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
      fade += 0.01f;
      isFade = true;
      fadeIn = true;
   }

   public void onLeave()
   {
      fade -= 0.01f;
      isFade = true;
      fadeIn = false;
   }

   public void setEnabled(bool enabled)
   {
      interactEnabled= enabled;
   }
#endregion
}
