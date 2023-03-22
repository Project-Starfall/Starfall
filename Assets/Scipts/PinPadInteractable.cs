using System.Collections;
using UnityEngine;

public class PinPadInteractable : MonoBehaviour, Interactable
{
   // Pinpad fields
   [SerializeField] GameObject pinpadCanvas;
   [SerializeField] Level1Handler levelHandler;
   [SerializeField] SpriteRenderer spriteRenderer;
   [SerializeField] PauseMenu menu;
   [SerializeField] PlayerMovement playerMovement;
   private bool active = false; // If UI is active
   
   // Interface fields
   private bool interactEnabled = true;
   private readonly TYPE interactableType = TYPE.Puzzle;

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
      isFade = true;
      fadeIn = true;
   }

   /*******************************************************************
    * Call when the player leaves interactable range
    ******************************************************************/
   public void onLeave()
   {
      isFade = true;
      fadeIn = false;
   }

   /*******************************************************************
    * Call when the player presses interactable key in range
    ******************************************************************/
   public bool run(Player player)
   {
      if (!interactEnabled) return true;
      menu.isUIOpen = true;
      interactEnabled = false;
      playerMovement.DisableMovement();
      pinpadCanvas.SetActive(true);
      StartCoroutine(delayedActive());
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
      menu.isUIOpen = false;
      playerMovement.EnableMovement();
      pinpadCanvas.SetActive(false);
      StartCoroutine(delayedDeactive());
   }

   /*******************************************************************
    * Update gets if escape is pressed and closes UI
    ******************************************************************/
   void Update()
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
         interactEnabled = true;
         close();
      }
   }

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
