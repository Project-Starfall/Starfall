using static Constants.Transitions;

public static class TransitionManager
{
   public static int transition = LV0_LV1;

   public static void setTransition(int value)
   {
      transition = value;
   }
}
