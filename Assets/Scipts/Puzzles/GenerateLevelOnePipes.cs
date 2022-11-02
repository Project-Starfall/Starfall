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

public class Pipe
{
   private PIPE_TYPE pipeType;
   private int top, bottom, left, right;
   private int orientation;

   public Pipe(PIPE_TYPE pipeType) {
      this.pipeType = pipeType;
      this.top = 0;
      this.bottom = 0;
      this.left = 0;
      this.right = 0;
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
