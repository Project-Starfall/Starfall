using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using static SaveSystem;

public class Level1Handler : MonoBehaviour
{
    // Fading assets destroy
    [SerializeField] GameObject exterior;
    [SerializeField] GameObject outside;

   // Puzzle Information
   [SerializeField] WirePuzzleScript puzzle1;
   [SerializeField] WirePuzzleScript puzzle2;
   [SerializeField] WirePuzzleScript puzzle3;
   [SerializeField] WirePuzzleScript puzzle4;
   [SerializeField] PinPadInteractable pinpad;
   [SerializeField] NoteInteractable note;

   int[] pinpadCode = new int[4];
   public int pipe1seed { get; private set; }
   public int pipe2seed { get; private set; }
   public int pipe3seed { get; private set; }
   public int pipe4seed { get; private set; }

   // Puzzle Completion
   int[] puzzlecomp = { 0, 0, 0, 0 };
   int pinpadcomp { get; set; } = 0;
   int currentPuzzle { get; set; } = 0;

   // Wall numbers
   [SerializeField] SpriteRenderer[] wallNumbers;
   [SerializeField] Sprite[] numberSprites;

   // Colliders
   [SerializeField] BoxCollider2D[] variableColliders;
   [SerializeField] BoxCollider2D animationCollider;
   [SerializeField] PolygonCollider2D[] cameraConfiners;

   // Player
   [SerializeField] Player player;
   [SerializeField] SpriteRenderer playerRenderer;
   [SerializeField] SpriteRenderer dashRenderer;
   [SerializeField] PlayerMovement playerMovement;
   int playerSeed;

   // Camera
   [SerializeField] CinemachineConfiner2D cameraConfine;

   // [SerializeField] GameObject pipecanvas;
   [SerializeField] PlayableDirector startTimeline;
   [SerializeField] PlayableDirector openExterior;
   [SerializeField] PlayableDirector lightsOut;
   [SerializeField] PlayableDirector openOffice;
   [SerializeField] PlayableDirector RescueAnimation;

   [SerializeField] OfficePopup popup;

    private void Awake()
    { 
        Application.targetFrameRate = 60;

        // transition background music into level one music
        FindObjectOfType<audioManager>().musicFadeOut("menuMusic");
        FindObjectOfType<audioManager>().musicFadeOut("levelTutorialMusic");
        FindObjectOfType<audioManager>().musicFadeIn("levelOneMusic");
    }

    // Start is called before the first frame update
    void Start()
   {
      // save the player data
      SaveGame(player);
      Debug.Log("Saving the player...");
      // load player save
      LoadGame();
      Debug.Log("Loading the game...");

      playerMovement.DisableMovement();
      startTimeline.Play();

      playerSeed = player.seed;
      System.Random random = new System.Random(playerSeed);

      // generate the pincode
      for(int i = 0; i < 4; i++)
      {
         pinpadCode[i] = random.Next(0, 10);
         wallNumbers[i].sprite = numberSprites[pinpadCode[i]];
         wallNumbers[i].enabled = false;
      }

      // generate the pipe seeds
      pipe1seed = random.Next(0, 10000);
      pipe2seed = random.Next(0, 10000);
      pipe3seed = random.Next(0, 10000);
      pipe4seed = random.Next(0, 10000);

      // change the scene colliders
      variableColliders[0].enabled = false;
      cameraConfine.m_BoundingShape2D = cameraConfiners[0];

      // Disable unreachable puzzles
      note.setEnabled(false);
      puzzle2.setEnabled(false);
      puzzle3.setEnabled(false);
      puzzle4.setEnabled(false);
   }

  /*******************************************************************
   * Rescue animation
   ******************************************************************/
   public void OnTriggerEnter2D(Collider2D collision)
   {
       playerMovement.DisableMovement();
       RescueAnimation.Play();
       waitForAmination();
        Destroy(animationCollider);
       return;
   }

   /*******************************************************************
    * Returns the current pinpad code as char array
    ******************************************************************/
   public char[] getPinpad()
   {
      char[] pinpadCode = new char[4];
      for(int i = 0; i < 4; i++)
      {
         pinpadCode[i] = this.pinpadCode[i].ToString().ToCharArray()[0];
      }
      return  pinpadCode;
   }

   /*******************************************************************
    * runs when the front door puzzle is complete
    ******************************************************************/
   public void openExteriorDoorSequence()
   {
      //save player
      SaveGame(player);
      Debug.Log("Saving the player...");
      // save gamestate
      // disable player movement
      note.setEnabled(true);
      puzzle2.setEnabled(true);
      puzzle3.setEnabled(true);
      puzzle4.setEnabled(true);
      cameraConfine.m_BoundingShape2D = cameraConfiners[1];
      variableColliders[0].enabled = true;
      variableColliders[1].enabled = false;
      playerMovement.DisableMovement();
      openExterior.Play();
      StartCoroutine(waitForTimeline());
      return;
   }

   /*******************************************************************
    * Runs when a pipe puzzle is completed
    ******************************************************************/
   public void copmletePipe(int number)
   {
      puzzlecomp[number - 1] = 1;
      currentPuzzle += 1;
      if (currentPuzzle == 4)
      {
         lastWireCompleted();
      }
      SaveGame(player);
      Debug.Log("Saving the player...");
    }

   /*******************************************************************
    * Runs when the last wire puzzle is completed
    ******************************************************************/
   public void lastWireCompleted()
   {
      lightsOut.Play();
      for (int i = 0; i < 4; i++) wallNumbers[i].enabled = true;
   }

   /*******************************************************************
    * Runs when the pinpad puzzle is complete
    ******************************************************************/
   public void openOfficeDoorSequence()
   {
      pinpadcomp = 1;
      variableColliders[2].enabled = false;
      openOffice.Play();
      playerMovement.DisableMovement();
      SaveGame(player); // save player
      Debug.Log("Saving the player...");
      return;
   }

   public void officeRescue()
   {
      cameraConfine.m_BoundingShape2D = cameraConfiners[2];
      variableColliders[3].enabled = true;
      popup.popup(true);
      SaveGame(player); // save player
      Debug.Log("Saving the player...");
    }

   /*******************************************************************
    * Runs when the pinpad puzzle is complete
    ******************************************************************/
   IEnumerator waitForTimeline()
   {
      yield return new WaitForSeconds(6f);
      playerRenderer.sortingLayerName = "Player & Platforms";
      dashRenderer.sortingLayerName = "Player & Platforms";
      Destroy(exterior);
      Destroy(outside);
      playerMovement.EnableMovement();
      SaveGame(player);
      Debug.Log("Saving the player...");
      yield return null;
   }

    /*******************************************************************
     * Re-enables player movement after an animation/timeline
     ******************************************************************/
    public void enablePlayerMove()
   {
      playerMovement.EnableMovement();
   }

    /*******************************************************************
     * Re-enables player movement after rescue animation
     ******************************************************************/
    IEnumerator waitForAmination()
    {
        yield return new WaitForSeconds(1f);
        playerMovement.EnableMovement();
        SaveGame(player);
        Debug.Log("Saving the player...");
        yield return null;
    }
}
