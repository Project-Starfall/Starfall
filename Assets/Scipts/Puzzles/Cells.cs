using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cells : MonoBehaviour
{
    /**********************************************************************
    * Totally useless class that could probably have been refactered into
    * something else entirely. It's only purpose is to get how many cells
    * there are track it using cellNumber. This could probably be put on
    * handler and just Serialize celllist to handler too.
    *********************************************************************/
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
