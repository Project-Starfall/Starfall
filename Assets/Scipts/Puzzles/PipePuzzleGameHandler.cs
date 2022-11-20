using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.Equals(object o)
public class Point
{
   public int x { get; set; }
   public int y { get; set; }

   public Point(int x, int y)
   {
      this.x = x;
      this.y = y;
   }

   public string toString() //for debugging
   {
      return $"({this.x},{this.y})";
   }

   public void gets(Point point)
   {
      this.x = point.x;
      this.y = point.y;  
   }

   public static bool operator ==(Point a, Point b)
   {
      return a.x == b.x && a.y == b.y;
   }
   public static bool operator !=(Point a, Point b)
   {
      return !(a == b);
   }
}

public class PipePuzzleGameHandler : MonoBehaviour
{
    private int grid_max = 8;                      // The max size of the playable grid
    private int grid_min = 1;                      // The min size of the playable grid
    private Pipe[,] puzzleGrid = new Pipe[10, 10]; // The pipe puzzle grid
    [SerializeField] Player player;                // Reference to the Player
    [SerializeField] GameObject pipeContainer;     // The container with the pipe gameobjects
    [SerializeField] Sprite[] sprites;             // Pipe sprite sheet
    
    // Start is called before the first frame update
    void Start()
    {
      // Take pipe gameobjects and put them into the game manager grid
      Pipe[] pipesInGrid = pipeContainer.GetComponentsInChildren<Pipe>();
      foreach(Pipe pipe in pipesInGrid) {
         puzzleGrid[pipe.posX, pipe.posY] = pipe;
      }

      // Generate the path and fill the grid with random pipes
      generate();

      // Spin the pipes :3
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void generate() {
      System.Random random = new System.Random() /* Get the seed from player */;
      int dirCurrent,
          dirNew; // Direction the pipe last went [wentFoward 1 ,wentUp 2, wentDown 3]
      Point currentPoint,     // Current calculated or last generated point
            endPoint,         // The point before the end generation aims at
            nextPoint,        // The point being calulated
            startPoint;       // The point right after start that generation begins
     

      // fill the grid with random pipes to start
      for (int x = 1; x <= 8; x++)
      {
         for (int y = 1; y <= 8; y++)
         {
            var roll = random.Next(101);
            if (roll <= 15) {
               puzzleGrid[x, y].setType(PIPE_TYPE.Straight);
               puzzleGrid[x, y].GetComponentInParent<SpriteRenderer>().sprite = sprites[2];
            } else if (roll <= 20) {
               puzzleGrid[x, y].setType(PIPE_TYPE.Cross);
               puzzleGrid[x, y].GetComponentInParent<SpriteRenderer>().sprite = sprites[3];
            } else if (roll <= 35) {
               puzzleGrid[x, y].setType(PIPE_TYPE.Corner);
               puzzleGrid[x, y].GetComponentInParent<SpriteRenderer>().sprite = sprites[4];
            } else if (roll <= 55) {
               puzzleGrid[x, y].setType(PIPE_TYPE.Junction);
               puzzleGrid[x, y].GetComponentInParent<SpriteRenderer>().sprite = sprites[5];
            } else {
               puzzleGrid[x, y].setType(PIPE_TYPE.Empty);
               puzzleGrid[x, y].GetComponentInParent<SpriteRenderer>().sprite = null;
            }
         }
      }

      // Determine the positions of start and end
      startPoint = new Point(1, random.Next(grid_min, grid_max + 1));
      endPoint   = new Point(8, random.Next(grid_min, grid_max + 1));
      puzzleGrid[startPoint.x - 1, startPoint.y].setType(PIPE_TYPE.Start);
      puzzleGrid[startPoint.x - 1, startPoint.y].setIsPowered(true);
      puzzleGrid[startPoint.x - 1, startPoint.y].GetComponentInParent<SpriteRenderer>().sprite = sprites[0];
      puzzleGrid[endPoint.x + 1, endPoint.y].setType(PIPE_TYPE.End);
      puzzleGrid[endPoint.x + 1, endPoint.y].setIsPowered(false);
      puzzleGrid[endPoint.x + 1, endPoint.y].GetComponentInParent<SpriteRenderer>().sprite = sprites[1];
      //setup the paramaters
      currentPoint = new Point(startPoint.x, startPoint.y);
      nextPoint = new Point(currentPoint.x, currentPoint.y);
      dirNew = 1;
      dirCurrent = 1;

      //loop until first path is generated
      for (int x = grid_min; x <= grid_max;)
      {
         if (endPoint.x != currentPoint.x) // protect against division by 0 for verticle lines
         {
            //find the equation of the line
            double slope = (endPoint.y - currentPoint.y) / (endPoint.x - currentPoint.x);
            int nextY = (int) Math.Round((slope * (x - endPoint.x)) + (double) endPoint.y);
            // Determine if X moves foward
            // disparity between next & end pt      20% chance to not        not the end of grid
            if (Math.Abs(nextY - currentPoint.y) < 2 && random.Next(101) > 20 && x < grid_max)
            {
               nextPoint.x += 1;
               nextPoint.y = currentPoint.y;
               x++;
               dirNew = 1;
            }
            else if (currentPoint.y == grid_min && dirCurrent == 3) // if cant go back up move foward regardless
            {
               nextPoint.x += 1;
               nextPoint.y = currentPoint.y;
               x++;
               dirNew = 1;
            }
            else if (currentPoint.y == grid_max && dirCurrent == 2) // if cant go back up move foward regardless
            {
               nextPoint.x += 1;
               nextPoint.y = currentPoint.y;
               x++;
               dirNew = 1;
            }
            else
            {
               nextPoint.x = currentPoint.x;

               // Determine if y goes up, or down
               if (currentPoint.y == grid_min) // forced up by bottom of grid
               {
                  nextPoint.y += 1;
                  dirNew = 2;
               }
               else if (currentPoint.y == grid_max) // forced down by top of grid
               {
                  nextPoint.y -= 1;
                  dirNew = 3;
               }
               else
               {
                  var roll = random.Next(101);
                  if (dirCurrent == 3) roll -= 50;      // force down because pipe above
                  else if (dirCurrent == 2) roll += 50; // force up because pipe below
                  else roll += ((nextY - currentPoint.y) * 15); // shift of 15% by y disparity
                  if (roll <= 50) // Go down: 50% base chance
                  {
                     nextPoint.y -= 1;
                     dirNew = 3;
                  }
                  else // Go up: 50% base chance
                  {
                     nextPoint.y += 1;
                     dirNew = 2;
                  }
               }
            }
         }
         else
         {
            if (currentPoint.y < endPoint.y)
            {
               nextPoint.x = currentPoint.x;
               nextPoint.y += 1;
               dirNew = 2;
            }
            else
            {
               nextPoint.x = currentPoint.x;
               nextPoint.y -= 1;
               dirNew = 3;
            }
         }

         // Evaluate next point and choose pipe appropriate pipe type
         if (dirCurrent == 1 && dirNew == 1 || dirCurrent != 1 && dirNew != 1)
         {
            puzzleGrid[currentPoint.x, currentPoint.y].setType(PIPE_TYPE.Straight);
            puzzleGrid[currentPoint.x, currentPoint.y].GetComponentInParent<SpriteRenderer>().sprite = sprites[2];
         } 
         else if (dirCurrent == 1 && dirNew > 1 || dirCurrent != 1 && dirNew == 1)
         {
            puzzleGrid[currentPoint.x, currentPoint.y].setType(PIPE_TYPE.Corner);
            puzzleGrid[currentPoint.x, currentPoint.y].GetComponentInParent<SpriteRenderer>().sprite = sprites[4];
         }
         
         if (nextPoint == endPoint) // reached end of path
         {
            if(dirNew != 1)
            {
               puzzleGrid[nextPoint.x, nextPoint.y].setType(PIPE_TYPE.Corner);
               puzzleGrid[nextPoint.x, nextPoint.y].GetComponentInParent<SpriteRenderer>().sprite = sprites[4];
            }
            else
            {
               puzzleGrid[nextPoint.x, nextPoint.y].setType(PIPE_TYPE.Straight);
               puzzleGrid[nextPoint.x, nextPoint.y].GetComponentInParent<SpriteRenderer>().sprite = sprites[2];
            }
            break;
         } 
         currentPoint.gets(nextPoint);
         dirCurrent = dirNew;
      }

      // Rotate the pieces randomly
      int[] rotations = new int[4] {0, 90, 180, 270};
      for (int x = 1; x <= 8; x++)
      {
         for (int y = 1; y <= 8; y++)
         {
            var transform = (Transform) puzzleGrid[x, y].GetComponentInParent<Transform>();
            transform.Rotate(new Vector3(0,0, rotations[random.Next(4)]));
         }
      }
   }
}
