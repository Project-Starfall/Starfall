using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This is an interface between interactable objects and the Player 
 */
public interface Grappleable
{
    public Transform[] controlPoints { get; set; }
    void onEnter();          // Is ran when the object is first detected
    void onLeave();          // is ran when the onbject is no longer detected
    void grapple(); // called when the player presses the interact key
}
