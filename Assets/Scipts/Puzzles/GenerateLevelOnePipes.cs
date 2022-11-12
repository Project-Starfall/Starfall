public class GenerateLevelOnePipes {
   private int grid_max = 8;
   private int grid_min = 1;
   private Pipe[,] puzzleGrid = new Pipe[10, 10];

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

   // clamps the game to the playable grid
   private int clampPipes(int value) {
      if (value > grid_max) return grid_max;
      else if (value < grid_min) return grid_min;
      else return value;
   }
}
