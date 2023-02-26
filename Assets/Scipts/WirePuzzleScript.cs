using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WirePuzzleScript : MonoBehaviour, Interactable
{
   [SerializeField] GameObject pipecanvas; // Pipegame UI
   [SerializeField] Level1Handler handler; // Level 1 handler
   [SerializeField] PipePuzzleGameHandler pipegame; // Pipegame handler
   [SerializeField] int puzzleNumber; // The number of the attached puzzle
   [SerializeField] bool completed; // If the puzzle is completed

   // Interface methods
   private bool interactEnabled = true;
   private readonly TYPE interactableType = TYPE.Puzzle;

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
    * Call when the player enters interactable range
    ******************************************************************/
   public void onEnter()
   {
      // start glowing
   }

   /*******************************************************************
    * Call when the player leaves interactable range
    ******************************************************************/
   public void onLeave()
   {
      // stop glowing
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
                  pipegame.generateGrid(handler.pipe1seed);
                  pipegame.startGame(this);
                  break;
               case 2:
                  pipegame.generateGrid(handler.pipe2seed);
                  pipegame.startGame(this);
                  break;
               case 3:
                  pipegame.generateGrid(handler.pipe3seed);
                  pipegame.startGame(this);
                  break;
               case 4:
                  pipegame.generateGrid(handler.pipe4seed);
                  pipegame.startGame(this);
                  break;
               default:
                  return false;
            }
      //disable player movement
      
      return true;
   }
   #endregion

   /*******************************************************************
    * Enable and disable puzzle UI
    ******************************************************************/
   public void showPuzzle(bool state)
   {
      if (state)
      {
         pipecanvas.transform.localScale = new Vector2(100,100);
      }
      else
      {
         pipecanvas.transform.localScale = new Vector2(0, 0);
      }
   }

   /*******************************************************************
    * Called when the puzzle is completed
    ******************************************************************/
   public void completeCheck(bool completed)
   {
      // Enable player movement
      if (!completed) return;
      switch(puzzleNumber)
      {
         case 1:
            Debug.Log("Copmleted Puzzle 1");
            showPuzzle(false);
            pipegame.generateGrid(0);
            handler.openExteriorDoorSequence();
            handler.copmletePipe(1);
            setEnabled(false);
            break;
         case 2:
            Debug.Log("Copmleted Puzzle 2");
            showPuzzle(false);
            pipegame.generateGrid(0);
            handler.copmletePipe(2);
            setEnabled(false);
            break;
         case 3:
            Debug.Log("Copmleted Puzzle 3");
            showPuzzle(false);
            pipegame.generateGrid(0);
            handler.copmletePipe(3);
            setEnabled(false);
            break;
         case 4:
            Debug.Log("Copmleted Puzzle 4");
            showPuzzle(false);
            handler.copmletePipe(4);
            pipegame.generateGrid(0);
            setEnabled(false);
            break;
      }
      
   }
}
