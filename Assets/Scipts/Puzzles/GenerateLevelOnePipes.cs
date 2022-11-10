using System;
using UnityEngine;
// Names the types of pipes
public enum PIPE_TYPE
{
   Straight,
   Corner,
   Junction,
   Cross,
   Start,
   End,
   Empty
}

public class Pipe
{
   private PIPE_TYPE pipeType;                  // The type of the pipe
   private int[] definition = { 0, 0, 0, 0 };   // Defines which sides of the pipe is the connection
                                                // {Top, Right, Bottom, Left}
   private int orientation;                     // The orientation of the pipe
   private bool isPowered;                      // Indicate that the currently has power

   //initialize the Pipe object setting the definitions for the given Pipetype
   public Pipe(PIPE_TYPE pipeType)
   {
      this.pipeType = pipeType;
        orientation = 0;
          isPowered = false;

      //There is probably a painfully more easy way to do this
      switch (pipeType) {
         case PIPE_TYPE.Straight:
            definition[1] = 1;
            definition[3] = 1;
            break;
         case PIPE_TYPE.Corner:
            definition[2] = 1;
            definition[3] = 1;
            break;
         case PIPE_TYPE.Cross:
            definition[0] = 1;
            definition[1] = 1;
            definition[2] = 1; 
            definition[3] = 1;
            break;
         case PIPE_TYPE.Junction:
            definition[1] = 1;
            definition[2] = 1;
            definition[3] = 1;
            break;
         case PIPE_TYPE.Start:
            definition[1] = 1;
            isPowered = true;
            break;
         case PIPE_TYPE.End:
            definition[3] = 1;
            break;
         default:
            Debug.Log("Could not create pipe object.\n Unknown pipe type given");
            break;
      }
   }
   
   // Getters and setters for the object fields
   //**********************************************/
   public PIPE_TYPE getType()
   {
      return pipeType;
   }
   public int getOrientation()
   {
      return orientation;
   }
   public int[] getDefinition()
   {
      return definition;
   }
   public bool getIsPowered()
   {
      return isPowered;
   }
   public void setType(PIPE_TYPE value)
   {
      pipeType = value;
   }
   public void setOrientation(int value)
   {
      orientation = value;
   }
   public void setIsPowered(bool value)
   {
      isPowered = value;
   }
   //**********************************************/
}

public class GenerateLevelOnePipes
{
   private int grid_max = 8;
   private int grid_min = 1;
   private Pipe[,] puzzleGrid = new Pipe[10,10];
   
   public bool generate(Player player)
   {
      System.Random random = new System.Random(12345)/* Get the seed from player */;
      int start, // the y coordinate of the starting block
          end;   // the y coordinate of the ending   block

      // Determine the positions of start and end 
      start = random.Next(grid_min, grid_max + 1);
      end   = random.Next(grid_min, grid_max + 1);

      //loop until first path is generated
      for (int i = grid_min; i <= grid_max; i++) {
         //find the equation of the line
         double slope = (end - start) / (grid_max - grid_min);
         double nextSpot = (slope * (i - grid_max)) + (double) end;
      }

      return true;
   }

   // clamps the game to the playable grid
   private int clampPipes(int value)
   {
      if(value > grid_max)      return grid_max;
      else if(value < grid_min) return grid_min;
      else                      return value;
   }
}
