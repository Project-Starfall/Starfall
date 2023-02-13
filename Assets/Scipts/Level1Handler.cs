using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class Level1Handler : MonoBehaviour
{
   // Puzzle Information
   [SerializeField]
   GameObject puzzle1;
   [SerializeField]
   GameObject puzzle2;
   [SerializeField]
   GameObject puzzle3;
   [SerializeField]
   GameObject puzzle4;
   [SerializeField]
   GameObject pinpad;
   int[] pinpadCode = new int[4];
   int pipe1seed;
   int pipe2seed;
   int pipe3seed;
   int pipe4seed;

   // Puzzle Completion
   int puzzle1comp { get; set; } = 0;
   int puzzle2comp { get; set; } = 0;
   int puzzle3comp { get; set; } = 0;
   int puzzle4comp { get; set; } = 0;
   int pinpadcomp { get; set; } = 0;
   int currentPuzzle { get; set; } = 0;

   // Player
   [SerializeField]
   Player player;
   [SerializeField]
   SpriteRenderer playerRenderer;
   int playerSeed;

   [SerializeField]
   PlayableDirector timeline;

   // Start is called before the first frame update
   void Start()
   {
      playerSeed = player.seed;
      System.Random random = new System.Random(playerSeed);

      // generate the pincode
      for(int i = 0; i < 4; i++)
      {
         pinpadCode[i] = random.Next(0, 10);
      }

   }

   /*******************************************************************
    * runs when the front door puzzle is complete
    ******************************************************************/
   public void openExteriorDoorSequence()
   {
      timeline.Play();
      currentPuzzle += 1;
      puzzle1comp = 1;
      return;
   }

   /*******************************************************************
    * runs when the pinpad puzzle is complete
    ******************************************************************/
   public void openOfficeDoorSequence()
   {
      return;
   }
}
