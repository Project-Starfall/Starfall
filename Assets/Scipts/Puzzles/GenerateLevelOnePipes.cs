using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PIPE_TYPE
{
   Straight,
   Corner,
   Junction,
   Cross
}

public class Pipe {
   private PIPE_TYPE pipeType;                  // The type of the pipe
   private int[] definition = { 0, 0, 0, 0 };   // Defines which sides of the pipe is the connection
                                                // {Top, Bottom, Left, Right}
   private int orientation;                     // The orientation of the pipe

   public Pipe(PIPE_TYPE pipeType)
   {
      this.pipeType = pipeType;
      this.orientation = 0;
      switch (pipeType) {
         case PIPE_TYPE.Straight:
            this.right = 1;
            this.left = 1;
            break;
         case PIPE_TYPE.Corner:
            this.top = 1;
            this.right = 1;
            break;
         case PIPE_TYPE.Cross:
            this.top = 1;
            this.bottom = 1;
            this.left = 1;
            this.right = 1;
            break;
         case PIPE_TYPE.Junction:
            this.right = 1;
            this.left = 1;
            this.top = 1;
            break;
         default:
            Debug.Log("Could not create pipe object.\n Unknown pipe type given");
            throw Exception;
      }
   }
}

public class GenerateLevelOnePipes
{
   private Pipe[,] puzzleGrid;
}
