
using System.Collections;
using UnityEngine;

public class WirePuzzleScript : MonoBehaviour, Interactable
{
   [SerializeField] GameObject pipecanvas; // Pipegame UI
   [SerializeField] Level1Handler level1handler; // Level 1 handler
    [SerializeField] Level5Handler level5Handler;
   [SerializeField] PipePuzzleGameHandler pipegame; // Pipegame handler
    [SerializeField] PlayerMovement playerMovement;
   [SerializeField] int puzzleNumber; // The number of the attached puzzle
   [SerializeField] bool completed; // If the puzzle is completed
   [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem particles;
    [SerializeField] PauseMenu menu;
   private bool active = false; // if UI is active

   // Interface methods
   private bool interactEnabled = true;
   private readonly TYPE interactableType = TYPE.Puzzle;

   private Material glowMaterial;
   private bool isFade = false;
   private bool fadeIn = false;
   private float fade = 0f;

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

   public void Start()
   {
      glowMaterial = spriteRenderer.material;
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
            if (interactEnabled)
            {
               fade += Time.deltaTime;
               if (fade >= 1f)
               {
                  fade = 1f;
                  isFade = false;
               }
            }
         }
         glowMaterial.SetFloat("_Fade", fade);
      }

      if (!active) return;

      if (Input.GetButtonDown("Action") || Input.GetKeyDown(KeyCode.Escape))
      {
         StartCoroutine(delayedDeactive());
         playerMovement.EnableMovement();
         pipecanvas.SetActive(false);
         menu.isUIOpen = false;
      }

   }

   #region interfaceMethods
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
    * Set if the interactable is enabled
    ******************************************************************/
   public void setEnabled(bool enabled)
   {
      this.interactEnabled = enabled;
   }

   /*******************************************************************
    * Call when the player presses interactable key in range
    ******************************************************************/
   public bool run(Player player)
   {
      if (!interactEnabled) return true;
      showPuzzle(true);
            switch (puzzleNumber)
            {
               case 1:
                  player.transform.position = new Vector2(-32, 1);
                  playerMovement.manualFlip(true);
                  pipegame.startGame(this);
                  pipegame.generateGrid(level1handler.pipe1seed);
                  break;
               case 2:
                  pipegame.startGame(this);
                  pipegame.generateGrid(level1handler.pipe2seed);
                  break;
               case 3:
                  pipegame.startGame(this);
                  pipegame.generateGrid(level1handler.pipe3seed);
                  break;
               case 4:
                  pipegame.startGame(this);
                  pipegame.generateGrid(level1handler.pipe4seed);
                  break;
               case 5:
                  pipegame.startGame(this);
                  pipegame.generateGrid(level5Handler.pipe5Seed);
                  break;
               default:
                  return false;
            }

        return true;
   }
   #endregion

   /*******************************************************************
    * Enable and disable puzzle UI
    ******************************************************************/
   public void showPuzzle(bool state, bool completed = false)
   {
      if(completed)
      {
         setEnabled(false);
         
         StartCoroutine(completedDelay());
         playerMovement.EnableMovement();
         particles.Stop();
         // make light go green
         // play victory noise
         FindObjectOfType<audioManager>().play("pipeComplete");
         return;
      }
      if (state)
      {
         setEnabled(false);
         menu.isUIOpen = true;
         StartCoroutine(delayedActive());
         playerMovement.DisableMovement();
         pipecanvas.SetActive(true);
      }
      else
      {
         menu.isUIOpen = false;
         playerMovement.EnableMovement();
         StartCoroutine(delayedDeactive());
         pipecanvas.SetActive(false);
      }
   }

   /*******************************************************************
    * Called when the puzzle is completed
    ******************************************************************/
   public void completeCheck(bool completed)
   {
      if (!completed) return;
      switch(puzzleNumber)
      {
         case 1:
            Debug.Log("Copmleted Puzzle 1");
            
            level1handler.copmletePipe(1);
            showPuzzle(false, true);
            level1handler.openExteriorDoorSequence();
            break;
         case 2:
            Debug.Log("Copmleted Puzzle 2");
            level1handler.copmletePipe(2);
               showPuzzle(false, true);
                break;
         case 3:
            Debug.Log("Copmleted Puzzle 3");
            
            level1handler.copmletePipe(3);
            showPuzzle(false, true);
                break;
         case 4:
            Debug.Log("Copmleted Puzzle 4");
            
            level1handler.copmletePipe(4);
            showPuzzle(false, true);
            break;
         case 5:
            Debug.Log("Completed Puzzle 5");
            level5Handler.completePuzzlePipe();
            showPuzzle(false, true);
            break;
      }
      
   }
   public IEnumerator delayedActive()
   {
      yield return new WaitForSeconds(0.10f);
        active = true;
      yield return null;
   }

   public IEnumerator delayedDeactive()
   {
      yield return new WaitForSeconds(0.10f);
        setEnabled(true);
        active = false;
      yield return null;
   }

   public IEnumerator completedDelay()
   {
      yield return new WaitForSeconds(1f);
      menu.isUIOpen = false;
      active = false;
      pipecanvas.SetActive(false);
      yield return null;
   }
}
