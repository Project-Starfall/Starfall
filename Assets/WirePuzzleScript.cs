using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePuzzleScript : MonoBehaviour, Interactable
{
   [SerializeField] Level1Handler handler;
   [SerializeField] int puzzleNumber;
   [SerializeField] bool completed;



   private TYPE interactableType = TYPE.Puzzle;

   public TYPE getType()
   {
      return interactableType;
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
      handler.openExteriorDoorSequence();
      return true;
   }

   // Start is called before the first frame update
   void Start()
    {
        
    }

}
