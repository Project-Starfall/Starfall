using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePuzzleScript : MonoBehaviour, Interactable
{
   [SerializeField] Level1Handler handler;
   [SerializeField] PipePuzzleGameHandler pipegame;
   [SerializeField] int puzzleNumber;
   [SerializeField] bool completed;
   private bool interactEnabled = true;
   private TYPE interactableType = TYPE.Puzzle;

   public TYPE getType()
   {
      return interactableType;
   }

   public bool isEnabled()
   {
      return interactEnabled;
   }

   public void onEnter()
   {
      // start glowing
   }

   public void onLeave()
   {
      // stop glowing
   }

   public bool run(Player player)
   {
      switch (puzzleNumber)
      {
         case 1:
            return true;
         case 2:
            return true;
         case 3:
            return true;
         case 4:
            return true;
         default:
            return false;
      }
   }

   public void setEnabled(bool enabled)
   {
      this.interactEnabled = enabled;
   }
}
