using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class HatchInteractable : MonoBehaviour, Interactable
{
   public bool interactEnabled = false;

   [SerializeField] PlayableDirector readyTimeline;
   [SerializeField] PlayerMovement playerMovement;
   [SerializeField] SpriteRenderer glowRenderer;
   [SerializeField] Level5Handler handler;

   private Material glowMaterial;
   private bool isFade = false;
   private bool fadeIn = false;
   private float fade = 0f;

   public void Start()
   {
      glowMaterial = glowRenderer.material;
      glowMaterial.SetFloat("_Fade", 0f);
   }

   public void Update()
   {
      if (isFade)
      {
         if (!fadeIn)
         {
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
            if (fade >= 1f)
            {
               fade = 1f;
               isFade = false;
            }
         }
         glowMaterial.SetFloat("_Fade", fade);
      }
   }

   // not used
   public TYPE getType()
   {
      throw new System.NotImplementedException();
   }

   public bool isEnabled()
   {
      return interactEnabled;
   }

   public void onEnter()
   {
      isFade = true;
      fadeIn = true;
   }

   public void onLeave()
   {
      isFade = true;
      fadeIn = false;
   }

   public bool run(Player player)
   {
      if (!handler.isComplete()) return true;
      readyTimeline.Play();
      playerMovement.DisableMovement();
      return true;
   }

   public void setEnabled(bool enabled)
   {
      interactEnabled = enabled;
   }

   public void OnTriggerEnter2D(Collider2D collision)
   {
      onEnter();
   }

   public void OnTriggerExit2D(Collider2D collision)
   {
      onLeave();
   }
}
