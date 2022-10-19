using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal; // Player's movement direction
    private bool  isFacingRight = true; // Player orientation
    [SerializeField] private float jumpStrength = 16f; // Player's jump force
    [SerializeField] private float speed = 8f; // Player's movement speed

    [SerializeField] private Rigidbody2D rb; // Player's physics body
    [SerializeField] private Transform groundCheck; // Position of the Player's feet
    [SerializeField] private LayerMask groundLayer; // Ground layer the player will walk on

    void Update()
    {
        // Sets the Player direction
        horizontal = Input.GetAxisRaw("Horizontal");

        // Jump when jump button is pressed (w or up)
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }
        
        // Makes Player's jump shorter if jump button is released
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        // Move Player along set vector (Vector direction set in Update)
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    // Flips the Player's sprite horizontally when moving a different direction
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

    // Determines if the Player is on the ground
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
