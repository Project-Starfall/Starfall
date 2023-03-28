using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FishingSpotInteractable : MonoBehaviour, Interactable
{
   // Interactable Fields
   private bool interactEnabled;

   // References to game objects
   [SerializeField] GameObject fishingGame; // REFERENCE TO YOUR PARENT FISHING GAME OBJECT
   [SerializeField] FishingGameMovement fishingGameMovement;
   [SerializeField] FishingHandler fishingHandler;
   [SerializeField] SpriteRenderer spriteRenderer;
   [SerializeField] PauseMenu menu;
   [SerializeField] PlayerMovement playerMovement;

   // Data
   private bool active = false; // if UI is active
   [SerializeField] private int gameInteractNum;
   private bool isComplete;
   private bool fishAdded;

   // InteractGlow data
   private Material glowMaterial;
   private bool isFade = false;
   private bool fadeIn = false;
   private float fade = 0f;

   // Setup the glow material at very first start tick
   public void Start()
   {
        glowMaterial = spriteRenderer.material;
        // glowMaterial.SetFloat("_Fade", 0f); not enabled until glow material is made

        interactEnabled = true;
        isComplete = false;
        fishAdded = false;
   }

   // Called to start the fishing game
   private void startFishingGame()
   {
        // TODO: Start the fishing game from here
        // Initialize Game
        fishingGameMovement.SetHorizontal(0f);
        fishingGameMovement.DisableMovement(false);
        fishingGameMovement.transform.localPosition = new Vector3(0f, 0f, 0f);
        fishingGameMovement.FishOnHook(false);
        fishingGameMovement.SetComplete(false);
        fishingGameMovement.SetGameNum(gameInteractNum);

        openGame();
   }

   // Used to enable the game canvas
   private void openGame()
   {
      fishingGame.SetActive(true); // SHOWS THE FISHING GAME
      menu.isUIOpen = true; // Tells the pause menu to ignore esc;
      setEnabled(false);    // prevents opening twice
      StartCoroutine(delayedActive());  // prevents opening before important systems
      playerMovement.DisableMovement(); // Stop the player :3
   }

   // Used to disable the game canvas and disable the interact spot if completed
   private void closeGame(bool completed)
   {
      if (completed)
      {
         fishingGame.SetActive(false); // closes the game on the canvas
         StartCoroutine(delayedDeactive());
         menu.isUIOpen = false;
         playerMovement.EnableMovement();
         AddFish();
      }
      else
      {
         fishingGame.SetActive(false); // closes the game on the canvas
         StartCoroutine(delayedDeactive());
         menu.isUIOpen = false;
         setEnabled(true);
         playerMovement.EnableMovement();
      }
   }

   /*******************************************************************
    * Update gets if escape is pressed and closes UI
    ******************************************************************/
   private void Update()
   {
      // Handles the fade of the glow
      // Not enabled until glow material is made
      /*if (isFade)
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
      */ 

      // Dont even bother checking the inputs, the game isnt active
      if (!active) return;

      if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Action"))
      {
          closeGame(false);
      }

      if (isComplete)
      {
          closeGame(true);      
      }
   }

   #region Interactable Methods
   public bool run(Player player)
   {
      startFishingGame();
      return true;
   }

   // To be removed from interface
   public TYPE getType()
   {
      // We arnt even using this. TO BE REMOVED
      throw new System.NotImplementedException();
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

   // Returns if the interactable is enabled
   public bool isEnabled()
   {
      return interactEnabled;
   }

   // Sets if the intertactable is enabled
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

   public void SetComplete(bool v)
   {
        isComplete = v;
        Debug.Log("GAME SET TO COMPLETE");
   }

    private void AddFish()
    {
        if (!fishAdded)
            fishingHandler.fishCount += 1;
        fishAdded = true;
    }

   // Calls the on enter and on leave functions from the interface when the collider is entered or left
   public void OnTriggerEnter2D(Collider2D collision)
   {
      onEnter();
   }

   public void OnTriggerExit2D(Collider2D collision)
   {
      onLeave();
   }
}
