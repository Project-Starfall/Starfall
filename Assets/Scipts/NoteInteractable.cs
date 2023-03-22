using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class NoteInteractable : MonoBehaviour, Interactable
{
   // Note fields
   [SerializeField] GameObject note; 
   private bool active = false; // if UI is active

   // Interface fields
   private readonly TYPE interactableType = TYPE.Item;
   private bool interactEnabled = true;
   [SerializeField] private SpriteRenderer spriteRenderer;
   [SerializeField] PauseMenu menu;
   [SerializeField] PlayerMovement playerMovement;

   private Material glowMaterial;
   private bool isFade = false;
   private bool fadeIn = false;
   private float fade = 0f;

   public void Start()
   {
      glowMaterial = spriteRenderer.material;
      glowMaterial.SetFloat("_Fade", 0f);
   }

   public void OnTriggerEnter2D(Collider2D collision)
   {
      onEnter();
   }

   public void OnTriggerExit2D(Collider2D collision)
   {
      onLeave();
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

   /*******************************************************************
    * Update gets if escape is pressed and closes UI
    ******************************************************************/
   private void Update()
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
      if (!active) return;

      if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Action"))
      {
         note.SetActive(false);
         StartCoroutine(delayedDeactive());
         menu.isUIOpen = false;
         setEnabled(true);
         playerMovement.EnableMovement();
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
    * Call when the player presses interactable key in range
    ******************************************************************/
   public bool run(Player player)
   {
      note.SetActive(true);
      menu.isUIOpen = true;
      setEnabled(false);
      StartCoroutine(delayedActive());
      playerMovement.DisableMovement();
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

   public IEnumerator delayedActive()
   {
      yield return new WaitForSeconds(0.1f);
      active = true;
      yield return null;
   }

   public IEnumerator delayedDeactive()
   {
      yield return new WaitForSeconds(0.1f);
      active = false;
      yield return null;
   }
}
