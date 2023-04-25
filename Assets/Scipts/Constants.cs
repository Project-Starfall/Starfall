/**********************************************************************
 * This is the Constants namespace to define constants that are used
 * across multple classes.
 *********************************************************************/
namespace Constants
{
   public static class Pipes
   {
      public const int LEFT = 0;
      public const int BOTTOM = 1;
      public const int RIGHT = 2;
      public const int TOP = 3;
   }

   public static class Scenes
   {
      public const int Menu = 0;
      public const int Tutorial = 1;
      public const int LevelOne = 2;
      public const int LevelTwo = 3;
      public const int LevelThree = 4;
      public const int LevelFour = 5;
      public const int LevelFive = 6;
      public const int FinalCutscene = 7;
      public const int Transistion = 8;
   }

   public static class ComponentsPuzzle
   {
      public const int MaxComponentValue = 9;
      public const int MinComponentValue = 0;
      public const int Operation = 0;
      public const int Value = 1;
      public const int Subtract = 0;
      public const int Addition = 1;
   }

   public static class Transitions
   {
      public const int LV0_LV1 = 0;
      public const int LV1_LV2 = 1;
      public const int LV2_LV3 = 2;
      public const int LV3_LV4 = 3;
      public const int LV4_LV5 = 4;
   }
}
