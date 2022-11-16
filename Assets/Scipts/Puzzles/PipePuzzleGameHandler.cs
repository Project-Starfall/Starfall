using System;
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

    [SerializeField] Sprite[] sprites;
    
    // Start is called before the first frame update
    void Start()
    {
      Pipe[] pipesInGrid = pipeContainer.GetComponentsInChildren<Pipe>();
      foreach(Pipe pipe in pipesInGrid) {
         puzzleGrid[pipe.posX, pipe.posY] = pipe;
      }

      generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public bool generate() {
      System.Random random = new System.Random()/* Get the seed from player */;
      int start, // the y coordinate of the starting block
          end;   // the y coordinate of the ending   block

      // fill the grid with random pipes to start
      for (int x = 1; x <= 8; x++)
      {
         for (int y = 1; y <= 8; y++)
         {
            switch(random.Next(5))
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
      start = random.Next(grid_min, grid_max + 1);
      end = random.Next(grid_min, grid_max + 1);
      puzzleGrid[0, start].setType(PIPE_TYPE.Start);
      puzzleGrid[0, start].GetComponentInParent<SpriteRenderer>().sprite = sprites[0];
      puzzleGrid[9, end].setType(PIPE_TYPE.End);
      puzzleGrid[9, end].GetComponentInParent<SpriteRenderer>().sprite = sprites[1];

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
