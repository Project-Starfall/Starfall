using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal; // Player's movement direction
    private bool  isFacingRight = true; // Player orientation
    [SerializeField] private float jumpStrength = 16f; // Player's jump force
    private int starPower = 0; // Star power currently equipped
    [SerializeField] private float speed = 8f; // Player's movement speed

    [SerializeField] private Rigidbody2D rb; // Player's physics body
    [SerializeField] private Transform groundCheck; // Checks for ground beneath player
    [SerializeField] private LayerMask groundLayer; // Ground layer the player will walk on
    [SerializeField] private Transform interactCheck; // Checks for nearby interactable
    [SerializeField] private LayerMask interactLayer; // Layer containing interactable objects

    void Update()
    {
        // Sets the player direction
        horizontal = Input.GetAxisRaw("Horizontal");

        // Jump when jump button is pressed (w or up)
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }
        
        // Makes player's jump shorter if jump button is released
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetButtonDown("Action"))
        {
            Action();
        }

        Flip();
    }

    private void FixedUpdate()
    {
        // Move player along set vector (Vector direction set in Update)
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    // Flips the player's sprite horizontally when moving a different direction
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // Determines if the player is on the ground
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // Determines if the player is near an interactable
    private bool NearInteractable()
    {
        return Physics2D.OverlapCircle(interactCheck.position, 1.0f, interactLayer);
    }

    // Performs player action
    private void Action()
    {
        // Interact if interactable is nearby
        if (NearInteractable())
        {
            Debug.Log("Interact");
        }
        // Otherwise, use currently equipped star power
        else
        {
            switch (starPower)
            {
                case 0:
                    Dash();
                    break;
                case 1:
                    Debug.Log("NOT DASH!!!");
                    break;
                default:
                    Debug.Log("PANIC!!!");
                    break;
            }
        }
    }

    // Dashes the player forward
    private void Dash()
    {
        Debug.Log("DASHED!!!");
    }
}
