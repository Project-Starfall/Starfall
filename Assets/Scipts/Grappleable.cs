/*
 * This is an interface between interactable objects and the Player 
 */
public interface Grappleable
{
    void onEnter();          // Is ran when the object is first detected
    void onLeave();          // is ran when the onbject is no longer detected
    void grapple(Player player); // called when the player presses the interact key
}
