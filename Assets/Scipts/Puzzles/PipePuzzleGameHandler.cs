using System;
using UnityEngine;
using static Constants.Pipes;

#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.Equals(object o)
/**********************************************************************
 * A point containing an x coordinate and y coordinate relating to
 * a grid based puzzle.
 *********************************************************************/
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
   public bool canPlay { get; private set; } = true;
   private int grid_max = 8;                      // The max size of the playable grid
   private int grid_min = 1;                      // The min size of the playable grid
   private Pipe[,] puzzleGrid = new Pipe[10, 10]; // The pipe puzzle grid
   private Point start, end;                      // Starting and end points
   [SerializeField] Player player;                // Reference to the Player
   [SerializeField] GameObject pipeContainer;     // The container with the pipe gameobjects
   [SerializeField] Sprite[] sprites;             // Pipe sprite sheet
   public WirePuzzleScript wirebox;
    
   // Start is called before the first frame update
   void Start()
   {
     // Take pipe gameobjects and put them into the game manager grid

      generateGrid(0);

   }

   /*******************************************************************
    * Receives an instance of the wire box calling the game
    ******************************************************************/
   public void startGame(WirePuzzleScript wirebox)
   {
      this.wirebox = wirebox;
      canPlay = true;
      Pipe[] pipesInGrid = pipeContainer.GetComponentsInChildren<Pipe>();
      foreach(Pipe pipe in pipesInGrid) {
         pipe.loadRenderer();
         puzzleGrid[pipe.posX, pipe.posY] = pipe;
      }
   }

   /*******************************************************************
    * Runs every update to determin if the game is complete
    ******************************************************************//*
   public void FixedUpdate()
   {
      var endPipe = puzzleGrid[end.x - 1, end.y];
      if(endPipe.getIsPowered())
      {
         if (endPipe.getDefinition()[(RIGHT + endPipe.getOrientation()) % 4] == 1)
         {
            wirebox.completeCheck(true);
         }
      }
   }*/

   /*******************************************************************
    * Check if the pipes are powered or not.
    ******************************************************************/
   public void checkPower() 
   {
      //check from start path powered
      //set all pipes to unpowered,
      //ensures all unpowered pipes get properly unpowered prevents loops not connected to start
      for (int x = 1; x <= grid_max; x++)
      {
         for (int y = 1; y <= grid_max; y++)
         {
            puzzleGrid[x, y].setIsPowered(false);
         }
      }
      //iterate through valid path setting powered
      recursiveIsPowered(puzzleGrid[start.x + 1, start.y], 32, LEFT);

      //set pipe material to powered or unpowered.
      for (int x = 1; x <= grid_max; x++)
      {
         for (int y = 1; y <= grid_max; y++)
         {
            if(puzzleGrid[x, y].getIsPowered())
            {
               puzzleGrid[x, y].setPoweredMaterial(true);
            }
            else
            {
               puzzleGrid[x, y].setPoweredMaterial(false);
            }
         }
      }
        var endPipe = puzzleGrid[end.x - 1, end.y];
        if (endPipe.getIsPowered())
        {
            if (endPipe.getDefinition()[(RIGHT + endPipe.getOrientation()) % 4] == 1)
            {
                wirebox.completeCheck(true);
            canPlay = false;
            }
        }
    }

   /*******************************************************************
    * Recursivly traces the path of the pipe setting all pipes on 
    * the path to powered on.
    * @param Pipe:pipe - Instance of the current pipe in the path
    * @param Int:ttl   - The time to live of the traced path
    *                    prevents infinite loop
    * @param Int:from  - The side the trace entered from
    * @return bool     - Returns the power state
    ******************************************************************/
   public void recursiveIsPowered(Pipe pipe, int ttl, int from)
   {
      if (ttl <= 0) return;
      // determine if this pipe connects
      switch (from)
      {
         case TOP:
            if (pipe.getDefinition()[(TOP + pipe.getOrientation()) % 4] == 1)
            {
               pipe.setIsPowered(true);
            }
            break;
         case BOTTOM:
            if (pipe.getDefinition()[(BOTTOM + pipe.getOrientation()) % 4] == 1)
            {
               pipe.setIsPowered(true);
            }
            break;
         case LEFT:
            if (pipe.getDefinition()[(LEFT + pipe.getOrientation()) % 4] == 1)
            {
               pipe.setIsPowered(true);
            }
            break;
         case RIGHT:
            if (pipe.getDefinition()[(RIGHT + pipe.getOrientation()) % 4] == 1)
            {
               pipe.setIsPowered(true);
            }
            break;
         default:
            // Error with piped passed
            throw new Exception();
      }

      //send power
      if (pipe.getIsPowered())
      {
         if (pipe.getDefinition()[(TOP + pipe.getOrientation()) % 4] == 1 && from != TOP)
         {
            if (pipe.posY + 1 <= 8)
            {
               recursiveIsPowered(puzzleGrid[pipe.posX, pipe.posY + 1], ttl - 1, BOTTOM);
            }
         }
         if (pipe.getDefinition()[(BOTTOM + pipe.getOrientation()) % 4] == 1 && from != BOTTOM)
         {
            if (pipe.posY - 1 >= 1)
            {
               recursiveIsPowered(puzzleGrid[pipe.posX, pipe.posY - 1], ttl - 1, TOP);
            }
         }

         if (pipe.getDefinition()[(RIGHT + pipe.getOrientation()) % 4] == 1 && from != RIGHT)
         {
            if (pipe.posX + 1 <= 8)
            {
               recursiveIsPowered(puzzleGrid[pipe.posX + 1, pipe.posY], ttl - 1, LEFT);
            }
         }

         if (pipe.getDefinition()[(LEFT + pipe.getOrientation()) % 4] == 1 && from != LEFT)
         {
            if (pipe.posX - 1 >= 1)
            {
               recursiveIsPowered(puzzleGrid[pipe.posX - 1, pipe.posY], ttl - 1, RIGHT);
            }
         }
      }
   }

   /*******************************************************************
    * Gets the current puzzle grid
    * @return Pipe[,] - returns the current puzzle grid
    ******************************************************************/
   public Pipe[,] getPuzzleGrid()
   {
      return this.puzzleGrid;
   }

   /*******************************************************************
    * This is the algorithm to generate the pipe grid. A singular path
    * is generated to ensure the puzzle can be completed. The rest of
    * the space is filled in and rotated randomly. The generation
    * follows simple rules.
    * - The path must always go foward or not at all (never go back)
    *     - This prevents the path from looping back in on itself
    * - The path must travel toward the end goal when the path can
    *   no longer go foward
    * - The greater disparity between the next step and end goal
    *   forces the generation to travel more in the direciton of the 
    *   end goal
    * - The path cannot go on itself
    ******************************************************************/
   public void generateGrid(int seed) {
      System.Random random = new System.Random(seed) /* Get the seed from player */;
      int dirCurrent,
          dirNew; // Direction the pipe last went [wentFoward 1,wentUp 2, wentDown 3]
      Point currentPoint,     // Current calculated or last generated point
            endPoint,         // The point before the end generation aims at
            nextPoint,        // The point being calulated
            startPoint;       // The point right after start that generation begins
     

      // fill the grid with random pipes to start
      for (int x = 1; x <= grid_max; x++)
      {
         for (int y = 1; y <= grid_max; y++)
         {
            var roll = random.Next(101);
            if (roll <= 45) {
               puzzleGrid[x, y].setType(PIPE_TYPE.Straight);
               puzzleGrid[x, y].GetComponentInParent<SpriteRenderer>().sprite = sprites[2];
            } else if (roll <= 55) {
               puzzleGrid[x, y].setType(PIPE_TYPE.Cross);
               puzzleGrid[x, y].GetComponentInParent<SpriteRenderer>().sprite = sprites[3];
            } else if (roll <= 80) {
               puzzleGrid[x, y].setType(PIPE_TYPE.Corner);
               puzzleGrid[x, y].GetComponentInParent<SpriteRenderer>().sprite = sprites[4];
            } else if (roll <= 100) {
               puzzleGrid[x, y].setType(PIPE_TYPE.Junction);
               puzzleGrid[x, y].GetComponentInParent<SpriteRenderer>().sprite = sprites[5];
            }
         }
      }
      // Reset start and end points
      for(int i = 1; i <= 8; i++)
      {
         puzzleGrid[0, i].setType(PIPE_TYPE.Empty);
         puzzleGrid[0, i].setIsPowered(false);
         puzzleGrid[0, i].GetComponentInParent<SpriteRenderer>().sprite = null;
         puzzleGrid[9, i].setType(PIPE_TYPE.Empty);
         puzzleGrid[9, i].setIsPowered(false);
         puzzleGrid[9, i].GetComponentInParent<SpriteRenderer>().sprite = null;
      }


      // Determine the positions of start and end
      startPoint = new Point(1, random.Next(grid_min, grid_max + 1));
      endPoint   = new Point(grid_max, random.Next(grid_min, grid_max + 1));
      this.start = new Point(startPoint.x - 1, startPoint.y);
      this.end   = new Point(endPoint.x + 1, endPoint.y);
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
            //       is at the bottom of grid      last came from above
            else if (currentPoint.y == grid_min && dirCurrent == 3) // if cant go back up move foward regardless
            {
               nextPoint.x += 1;
               nextPoint.y = currentPoint.y;
               x++;
               dirNew = 1;
            }
            //       is at the top of grid         last came from below
            else if (currentPoint.y == grid_max && dirCurrent == 2) // if cant go back down move foward regardless
            {
               nextPoint.x += 1;
               nextPoint.y = currentPoint.y;
               x++;
               dirNew = 1;
            }
            else
            {
               nextPoint.x = currentPoint.x; // x stays where it is

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
         else // either directly below or above end goal
         {
            // if below, move up
            if (currentPoint.y < endPoint.y)
            {
               nextPoint.x = currentPoint.x;
               nextPoint.y += 1;
               dirNew = 2;
            }
            // if above, move down
            else
            {
               nextPoint.x = currentPoint.x;
               nextPoint.y -= 1;
               dirNew = 3;
            }
         }
         // TODO: add chance of straight being cross or junction
         //       add chance of corner being cross or junciton
         // Evaluate next point and choose appropriate pipe type
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
      for (int x = 1; x <= grid_max; x++)
      {
         for (int y = 1; y <= grid_max; y++)
         {
            puzzleGrid[x, y].rotate(random.Next(4));
         }
      }

      // Light up the first pipe if its connected
      checkPower();
   }
}
