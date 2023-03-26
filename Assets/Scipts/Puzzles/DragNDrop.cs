using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
   private bool moving = false;
   private float startPosX;
   private float startPosY;
   private Transform componentTransform;
   private Vector3 resetPosition;
   public float toleranceX;
   public float toleranceY;
   private ComponentHandler handler;
   private Cell currentCell = null;
   public int componentValue {get; set;}
   public int operation { get; set;}

   private void Start()
   {
      componentTransform = transform;
      resetPosition = transform.position;
   }

   public void startGame(ComponentHandler handler, int value, int operation)
   {
      this.handler   = handler;
      componentValue = value;
      this.operation = operation;   
   }

   // Update is called once per frame
   void Update()
   {
      if (moving)
      {
         Vector3 mousePos = Input.mousePosition;
         mousePos = Camera.main.ScreenToWorldPoint(mousePos);

         componentTransform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, componentTransform.position.z);
      }
   }

   private void OnMouseDown()
   {
      if (Input.GetMouseButtonDown(0))
      {
         Vector3 mousePos = Input.mousePosition;
         mousePos = Camera.main.ScreenToWorldPoint(mousePos);

         startPosX = mousePos.x - transform.position.x;
         startPosY = mousePos.y - transform.position.y;

         moving = true;
      }
   }

   private void OnMouseUp()
   {
      if (!moving) return; 
      moving = false;
      foreach (Cell cell in handler.componentCells.GetCells())
      {
         if (Mathf.Abs(cell.cellTransform.position.x - componentTransform.position.x) <= toleranceX &&
             Mathf.Abs(cell.cellTransform.position.y - componentTransform.position.y) <= toleranceY)
         {
            if (cell.component == null)
            {
               if (currentCell != null) currentCell.component = null;
               currentCell = cell;
               cell.component = this;

               componentTransform.position = new Vector2(cell.cellTransform.position.x, cell.cellTransform.position.y);
               handler.evaluateCells();
               return;
            }
         }
      }
      if (currentCell != null) currentCell.component = null;
      currentCell = null;
      componentTransform.position = resetPosition;
   }

   public override string ToString()
   {
      return $"{componentValue} {operation}";
   }
}
