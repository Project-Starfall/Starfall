using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentHandler : MonoBehaviour
{
   [SerializeField] public Cells componentCells;
   [SerializeField] DragNDrop[] componentPart;

   // Start is called before the first frame update
   void Start()
   {
      componentPart[0].startGame(this, 0, 0);
      componentPart[1].startGame(this, 1, 1);
   }

   public Cells getCells()
   {
      return componentCells;
   }

   public void evaluateCells()
   {
      string testPrint = "Cells: ";
      foreach(Cell cell in componentCells.GetCells())
      {
         testPrint += $"{cell.component}, ";
      }
      Debug.Log(testPrint);
   }
}
