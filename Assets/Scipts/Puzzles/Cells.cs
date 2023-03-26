using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cells : MonoBehaviour
{
    
    [SerializeField] List<Cell> cellsList  = new List<Cell>();
    [SerializeField] int cellNumber;


   public List<Cell> GetCells() { return cellsList; }

   public int getCellNumber()
   {
      return cellNumber;
   }

   public void setCellNumber(int cellNumber)
   {
      this.cellNumber = cellNumber;
   }
}
