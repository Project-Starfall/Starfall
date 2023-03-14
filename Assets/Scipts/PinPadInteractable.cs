using System.Collections;
using UnityEngine;

public class PinPadInteractable : MonoBehaviour, Interactable
{
   // Pinpad fields
   [SerializeField] GameObject pinpadCanvas;
   [SerializeField] Level1Handler levelHandler;
   [SerializeField] SpriteRenderer renderer;
   [SerializeField] PauseMenu menu;
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
      glowMaterial = renderer.material;
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
      levelHandler.disablePlayerMovement(true);
      pinpadCanvas.transform.localScale = new Vector3(70, 70, 1);
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
      levelHandler.disablePlayerMovement(false);
      pinpadCanvas.transform.localScale = new Vector3(0, 0, 0);
      active = false;
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

      if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.RightShift))
      {
         interactEnabled = true;
         close();
      }
   }

   public IEnumerator delayedActive()
   {
      yield return new WaitForSeconds(0.5f);
      active = true;
      yield return null;
   }
}
