using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
   public int x { get; set; }
   public int y { get; set; }

   public Point(int x, int y)
   {
      this.x = x;
      this.y = y;
   }

   public static bool operator ==(Point a, Point b)
   {
      return a.x == b.x && a.y == b.y;
   }
   public static bool operator !=(Point a, Point b)
   {
      return !(a==b);
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

   public bool generate() {
      System.Random random = new System.Random()/* Get the seed from player */;
      Point currentPoint, // current calculated or last generated point
            endPoint,     // the point before the end generation aims at
            nextPoint,    // the point being calulated
            startPoint;   // the point right after start that generation begins
      bool wentForward,
           wentUp;
     

      // fill the grid with random pipes to start
      for (int x = 1; x <= 8; x++)
      {
         for (int y = 1; y <= 8; y++)
         {
            switch(4)//random.Next(5))
            {
               case 0:
                  puzzleGrid[x, y].setType(PIPE_TYPE.Straight);
                  puzzleGrid[x, y].GetComponentInParent<SpriteRenderer>().sprite = sprites[2];
                  break;
               case 1:
                  puzzleGrid[x, y].setType(PIPE_TYPE.Cross);
                  puzzleGrid[x, y].GetComponentInParent<SpriteRenderer>().sprite = sprites[3];
                  break;
               case 2:
                  puzzleGrid[x, y].setType(PIPE_TYPE.Corner);
                  puzzleGrid[x, y].GetComponentInParent<SpriteRenderer>().sprite = sprites[4];
                  break;
               case 3:
                  puzzleGrid[x, y].setType(PIPE_TYPE.Junction);
                  puzzleGrid[x, y].GetComponentInParent<SpriteRenderer>().sprite = sprites[5];
                  break;
               case 4:
                  puzzleGrid[x, y].setType(PIPE_TYPE.Empty);
                  puzzleGrid[x, y].GetComponentInParent<SpriteRenderer>().sprite = null;
                  break;
            }
         }
      }

      // Determine the positions of start and end 
      startPoint = new Point(1, random.Next(grid_min, grid_max + 1));
      endPoint   = new Point(8, random.Next(grid_min, grid_max + 1));
      puzzleGrid[startPoint.x - 1, startPoint.y].setType(PIPE_TYPE.Start);
      puzzleGrid[startPoint.x - 1, startPoint.y].GetComponentInParent<SpriteRenderer>().sprite = sprites[0];
      puzzleGrid[startPoint.x, startPoint.y].setType(PIPE_TYPE.Straight);
      puzzleGrid[startPoint.x, startPoint.y].GetComponentInParent<SpriteRenderer>().sprite = sprites[2];
      puzzleGrid[endPoint.x + 1, endPoint.y].setType(PIPE_TYPE.End);
      puzzleGrid[endPoint.x + 1, endPoint.y].GetComponentInParent<SpriteRenderer>().sprite = sprites[1];
      currentPoint = startPoint;
      nextPoint = currentPoint;
      wentForward = true;
      wentUp = false;

      //loop until first path is generated
      for (int x = grid_min; x <= grid_max;)
      {
         if (endPoint.x != currentPoint.x) // protect against division by 0 for verticle lines
         {
            //find the equation of the line
            double slope = (endPoint.y - currentPoint.y) / (endPoint.x - currentPoint.x);
            int nextY = (int) Math.Round((slope * (x - endPoint.x)) + (double) endPoint.y);

            // Determine if X moves foward
            // disparity between next & end pt      33% chance to not        not the end of grid
            if (Math.Abs(nextY - currentPoint.y) < 2 && random.Next(101) > 33 && x < grid_max)
            {
               nextPoint.x += 1;
               nextPoint.y = currentPoint.y;
               x++;
               wentUp = false;
               wentForward = true;
            }
            else
            {
               nextPoint.x = currentPoint.x;

               // Determine if y goes up, or down
               if (currentPoint.y == grid_min) // forced up by bottom of grid
               {
                  nextPoint.y += 1;
                  wentUp = true;
                  wentForward = false;
               }
               else if (currentPoint.y == grid_max) // forced down by top of grid
               {
                  nextPoint.y -= 1;
                  wentUp = false;
                  wentForward = false;
               }
               else
               {
                  var roll = random.Next(101);
                  if (wentUp) roll += 50; // goes up if last pipe was from under
                  else if (!wentForward) roll -= 50; // goes down if last pipe was from above
                  roll += ((nextY - currentPoint.y) * 25); // shift of 25% by y disparity
                  if (roll <= 50) // Go down: 50% base chance
                  {
                     nextPoint.y -= 1;
                     wentUp = false;
                     wentForward = false;
                  }
                  else // <= 100 but supports >100 // Go up: 50% base chance
                  {
                     nextPoint.y += 1;
                     wentUp = true;
                     wentForward = false;
                  }
               }
            }
         }
         else
         {
            if(currentPoint.y < endPoint.y)
            {
               nextPoint.x = currentPoint.x;
               nextPoint.y += 1;
               wentUp=true;
               wentForward = false;
            } 
            else
            {
               nextPoint.x = currentPoint.x;
               nextPoint.y -= 1;
               wentUp = false;
               wentForward = false;
            }

         }
         // Evaluate next point
         puzzleGrid[nextPoint.x, nextPoint.y].setType(PIPE_TYPE.Straight);
         puzzleGrid[nextPoint.x, nextPoint.y].GetComponentInParent<SpriteRenderer>().sprite = sprites[2];
         if (nextPoint == endPoint) // reached end of path
         {
            break;
         }
            
      }
      return true;
   }
}
