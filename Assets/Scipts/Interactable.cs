/*
 * The type of an interactable object
 */
public enum TYPE {
   Powerup,
   Item
}

/*
 * This is an interface between interactable objects and the Player 
 */
public interface Interactable {
   void onEnter();          // Is ran when the object is first detected
   void onLeave();          // is ran when the onbject is no longer detected
   bool run(Player player); // called when the player presses the interact key
   TYPE getType();          // returns the type of the interactable to the player
}
