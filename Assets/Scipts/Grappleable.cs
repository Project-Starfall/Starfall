using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This is an interface between interactable objects and the Player 
 */
public interface Grappleable
{
    void onEnter();          // Is ran when the object is first detected
    void onLeave();          // is ran when the onbject is no longer detected
    public (Transform t1, Transform t2, Transform t3, Transform t4, Transform t5) returnGrapple();
                             // called when the player presses the interact key
}
