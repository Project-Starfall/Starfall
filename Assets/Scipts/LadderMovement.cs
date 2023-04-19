using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    public  float LadderMoveSpeed = 6; // speed of accending or descending ladders
    private float vertical;            // determins if player is going up or down

    // Audio members
    public AudioSource ladderSound;

    // When player is on a ladder use w/s or upArrow/downArrow to climb up and down the ladder
    private void OnTriggerStay2D(Collider2D other)
    {
        // have a chance to play ladder sound when climbing a ladder
        vertical = Input.GetAxisRaw("Vertical");
        //ladderSound = GetComponent<AudioSource>();
        if (other.tag == "Player" && Random.Range(0, 12) == 0 && vertical != 0 && !other.IsTouchingLayers(LayerMask.GetMask("Ground/Platform")) && !ladderSound.isPlaying)
            ladderSound.Play();

        if (vertical == 0)
            ladderSound.Stop();

        // ladder climbing capabilities when player is touching a ladder
        if (other.tag == "Player" && vertical > 0)
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, LadderMoveSpeed);
        else if (other.tag == "Player" && vertical < 0)
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -LadderMoveSpeed);
        else
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.55f);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ladderSound.Stop();
    }
}
