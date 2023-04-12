using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
   [SerializeField] TMP_Text number;               // Number displayed on component
   [SerializeField] SpriteRenderer spriteRenderer; // Sprite Renderer
   [SerializeField] MeshRenderer textRenderer;     // Text Renderer
   private bool moving = false;     // If the component is currently being moved
   private float startPosX;         // The change in where the mouse moved from
   private float startPosY;         // The change in where the mouse moved from but in y
   private Transform componentTransform; // The position of the component
   private Vector3 resetPosition;        // The position where the component defaults to
   public float toleranceX; // Cell fit tolerance
   public float toleranceY; // Cell fit tolerance but Y
   private ComponentHandler handler; // The puzzle handler
   private Cell currentCell = null;  // The current cell the component is in
   public int componentValue {get; set;} // The value of the component
   public int operation { get; set;} // The operation of the component

   private Material colorBandMaterial;
    private Color[] colors = {new Color(0f,0f,0f), new Color(0.50f, 0.24f, 0.09f), new Color(1f, 0f, 0f), new Color(1f, 0.45f, 0f), new Color(1f, 1f, 0f), new Color(0f, 1f, 0f), new Color(0f, 0f, 1f), new Color(0.498f,0f,1f), new Color(0.7f, 0.7f, 0.7f), new Color(1f,1f,1f)};

   /**********************************************************************
    * Unity's Start function, just setting up the reset position and 
    * setting component transform to stop unecessary calls of the
    * transform => GetComponent()
    *********************************************************************/
   private void Start()
   {
      
      componentTransform = transform;
      resetPosition = transform.position;
   }

   /**********************************************************************
    * Initializes the part's reference to the puzzle handler, its value
    * and operation. Sets the number to value and operation.
    *********************************************************************/
   public void initPart(ComponentHandler handler, int value, int operation)
   {
      colorBandMaterial = spriteRenderer.material;
      this.handler   = handler;
      componentValue = value;
      this.operation = operation;
      string text = "";
      text += ((operation == 0) ? "-" : "+");
      text += $"{value}";
      number.text = text;
      colorBandMaterial.SetColor("_Color", colors[componentValue]);
    }

   /**********************************************************************
    * Unity's Update function. Make the part follow the pointer when
    * being moved.
    *********************************************************************/
   void Update()
   {
      if (moving)
      {
         Vector3 mousePos = Input.mousePosition;
         mousePos = Camera.main.ScreenToWorldPoint(mousePos);

         componentTransform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, componentTransform.position.z);
      }
   }

   /**********************************************************************
    *Is called when the mouse button is pressed over this component
    *********************************************************************/
   private void OnMouseDown()
   {
      if (Input.GetMouseButtonDown(0))
      {
         Vector3 mousePos = Input.mousePosition;
         mousePos = Camera.main.ScreenToWorldPoint(mousePos);
         spriteRenderer.sortingOrder = 20;
         textRenderer.sortingOrder = 21;
         startPosX = mousePos.x - transform.position.x;
         startPosY = mousePos.y - transform.position.y;

         moving = true;
      }
   }

   /**********************************************************************
    * Is called when the mouse button is released 
    * there might be a better way to do this. But this seems the least
    * complicated without having to deal with collisions.
    *********************************************************************/
   private void OnMouseUp()
   {
      // Reset the render layers and stop moving
      moving = false;
      spriteRenderer.sortingOrder = 10;
      textRenderer.sortingOrder = 11;
      
      // Loop through each cell and determine if the component is close enough to be dropped into it
      foreach (Cell cell in handler.componentCells.GetCells())
      {
         // Proximity check
         if (Mathf.Abs(cell.cellTransform.position.x - componentTransform.position.x) <= toleranceX &&
             Mathf.Abs(cell.cellTransform.position.y - componentTransform.position.y) <= toleranceY)
         {
            // If cell is empty
            if (cell.component == null)
            {
               // Set the cell it moved from's component to empty and set the current cell to new component
               if (currentCell != null) currentCell.component = null;
               currentCell = cell;
               cell.component = this;

               // Set the component into the cell and check for completness
               componentTransform.position = new Vector2(cell.cellTransform.position.x, cell.cellTransform.position.y);
               handler.evaluateCells();
               return; // stop looking through cells
            }
         }
      }
      // Not close enough to any cells send to reset position and clean up if was previously in a cell
      if (currentCell != null) currentCell.component = null;
      currentCell = null;
      componentTransform.position = resetPosition;
      // check for completness
      handler.evaluateCells();
   }
}
