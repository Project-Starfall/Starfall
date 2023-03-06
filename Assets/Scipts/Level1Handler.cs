using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

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
   [SerializeField] PolygonCollider2D[] cameraConfiners;

   // Player
   [SerializeField]
   Player player;
   [SerializeField]
   SpriteRenderer playerRenderer;
   [SerializeField]
   SpriteRenderer dashRenderer;
   int playerSeed;

   // Camera
   [SerializeField] CinemachineConfiner2D cameraConfine;

  // [SerializeField] GameObject pipecanvas;
   [SerializeField] PlayableDirector openExterior;
   [SerializeField] PlayableDirector lightsOut;
   [SerializeField] PlayableDirector openOffice;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
   {
      // load player save

      //pipecanvas.SetActive(false);
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
      // save gamestate
      // disable player movement
      note.setEnabled(true);
      puzzle2.setEnabled(true);
      puzzle3.setEnabled(true);
      puzzle4.setEnabled(true);
      cameraConfine.m_BoundingShape2D = cameraConfiners[1];
      variableColliders[0].enabled = true;
      variableColliders[1].enabled = false;
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
      return;
   }

   /*******************************************************************
    * Runs when the pinpad puzzle is complete
    ******************************************************************/
   IEnumerator waitForTimeline()
   {
      yield return new WaitForSeconds(6.5f);
      playerRenderer.sortingLayerName = "Player & Platforms";
      dashRenderer.sortingLayerName = "Player & Platforms";
      Destroy(exterior);
      Destroy(outside);
      // Enable Player movement
      yield return null;
   }
}
