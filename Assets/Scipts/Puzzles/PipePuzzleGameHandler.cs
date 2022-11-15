using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePuzzleGameHandler : MonoBehaviour
{
    private int grid_max = 8; // The offset from the border grid
    private int grid_min = 1; //
    private Pipe[,] puzzleGrid = new Pipe[10, 10]; // The pipe puzzle grid
    [SerializeField] Player player;
    [SerializeField] GameObject pipeContainer;
    
    // Start is called before the first frame update
    void Start()
    {
      Pipe[] pipesInGrid = pipeContainer.GetComponentsInChildren<Pipe>();
      foreach(Pipe pipe in pipesInGrid) {
         puzzleGrid[pipe.posX, pipe.posY] = pipe;
      }
      string test = "|";
      for (int y = 1; y < 9; y++) {
         for(int x = 0; x < 10; x++) {
            test += $"{puzzleGrid[x, y].getType()}|";
         }
         test += "\n|";
      }

      Debug.Log(test);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public bool generate(Player player) {
      System.Random random = new System.Random(12345)/* Get the seed from player */;
      int start, // the y coordinate of the starting block
          end;   // the y coordinate of the ending   block

      // Determine the positions of start and end 
      start = random.Next(grid_min, grid_max + 1);
      end = random.Next(grid_min, grid_max + 1);

      //loop until first path is generated
      for (int i = grid_min; i <= grid_max; i++) {
         //find the equation of the line
         double slope = (end - start) / (grid_max - grid_min);
         double nextSpot = (slope * (i - grid_max)) + (double) end;
      }

      return true;
   }

   private int clampPipes(int value) {
      if (value > grid_max) return grid_max;
      else if (value < grid_min) return grid_min;
      else return value;
   }
}
