using System;
using TMPro;
using UnityEngine;
using static Constants.ComponentsPuzzle;

public class ComponentHandler : MonoBehaviour
{
   [SerializeField] public Cells componentCells; // The cells in which parts are placed
   [SerializeField] DragNDrop[] componentPart;   // The parts with their drag and drop functionality
   [SerializeField] TMP_Text ssDisplay;          // The Seven Segment Display
   [SerializeField] TMP_Text solutionValue;      // The silk screened solution
   private int[,] equation = new int[5, 2];      // The generated equation
   private int total = 0;                        // The total value needed to be achieved 
   private int seed;                             // Random seed

   /**********************************************************************
    * Delete when putting into level 5 used to start the game in the 
    * puzzle test scene.
    *********************************************************************/
   private void Start()
   {
      startGame(new System.Random().Next());
   }

   /**********************************************************************
    * Starts the game and initializes all the components
    *********************************************************************/
   public void startGame(int seed)
   {
      this.seed = seed;
      generateEquation();
      initializeComponents();
   }

   /**********************************************************************
    * Is called when the puzzle is complete
    *********************************************************************/
   public void completedPuzzle()
   {
      throw new NotImplementedException("This is not implemented into a level 5 handler yet");
   }

   /**********************************************************************
    * Evaluate the cells updating the seven segment display and checking
    * for a win condition
    *********************************************************************/
   public void evaluateCells()
   {
      int totalCheck = 0; // Current evaluated total
      bool completed = true; // Is all the cells filled with a part

      // Loop through all the cells getting the value of components and
      // adding the values together to get a complete total
      foreach(Cell cell in componentCells.GetCells())
      {
         // Is this cell not empty?
         if (cell.component != null)
         {
            if (cell.component.operation == Subtract) // Subtraction
            {
               totalCheck -= cell.component.componentValue;
            }
            else // Addition
            {
               totalCheck += cell.component.componentValue;
            }
         }
         else
         {
            completed = false;
         }
      }
      
      // Display value on 7segDisplay
      ssDisplay.text = $"{totalCheck}";
      
      // Check if puzzle is complete
      if(totalCheck == total && completed) 
      {
         //Win condition
         Debug.Log("You Win!");
         completedPuzzle();
      }
      
   }

   /**********************************************************************
    * Initializes the componenets by setting their operation and value
    *********************************************************************/
   public void initializeComponents()
   {
      System.Random random = new System.Random(seed); // Random numbers go brrr
      
      // Shuffle the list so parts appear to be randomly placed on screen
      shuffleParts(componentPart, random);
      
      // Loop through each part until all parts are intitialized
      for(int x = 0; x < componentPart.Length; x++)
      {
         if (x < componentCells.getCellNumber()) componentPart[x].initPart(this, equation[x, Value], equation[x, Operation]); // Set the parts associated with the equation
         else componentPart[x].initPart(this, random.Next(MinComponentValue, MaxComponentValue + 1), random.Next(Subtract, Addition + 1)); // randomly set these part values
      }
   }

   /**********************************************************************
    * Generates the garunteed equation used to solve the component puzzle
    *********************************************************************/
   public void generateEquation()
   {
      System.Random random = new System.Random(seed); // random numbers go brrr
      
      // Generate the 5 length equation for the component puzzle
      for (int x = 0; x < 5; x++)
      {
         for (int y = 0; y < 2; y++)
         {
            if (y == 0) // Set the operation between subtract and addition
            {
               equation[x, y] = random.Next(Subtract, Addition + 1);
            }
            else // Pick the number
            {
               equation[x, y] = random.Next(MinComponentValue, MaxComponentValue + 1);
            }
         }
      }

      // Calculate and set the total amount needed to be achieved
      for (int x = 0; x < 5; x++)
      {
         if (equation[x, 0] == Subtract) // Subtraction :I
         {
            total -= equation[x, 1];
         }
         else // Addition
         {
            total += equation[x, 1];
         }
      }

      // Display the solution needed to be achieved
      solutionValue.text = total.ToString();
   }


   /**********************************************************************
    * This is used to shuffle an array of parts so that assignment of
    * operator and number are in a psuedo random sequence. This is done
    * using the Fisher-Yates algorithm AKA Knuth shuffle.
    *********************************************************************/
   public static void shuffleParts(DragNDrop[] array, System.Random rng) 
   {
      int n = array.Length;
      while(n > 1)
      {
         int k = rng.Next(n--);
         // tupple swap?? idk intellisense made me do it?
         (array[k], array[n]) = (array[n], array[k]);
      }
   }
}
