using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // Movement members
    private float horizontal; // Player's movement direction
    private bool isFacingRight = true; // Player orientation
    [SerializeField] private float jumpStrength = 16f; // Player's jump force
    private int starPower = 0; // Star power currently equipped
    [SerializeField] private float speed = 8f; // Player's movement speed

    // Physics members
    [SerializeField] private Transform groundCheck; // Checks for ground beneath player
    [SerializeField] private LayerMask groundLayer; // Ground layer the player will walk on
    [SerializeField] private Transform interactCheck; // Checks for nearby interactable
    [SerializeField] private LayerMask interactLayer; // Layer containing interactable objects
    [SerializeField] private Rigidbody2D rb; // Player's physics body

    // Dash members
    bool canDash = true; // Can the player dash yet?
    private bool isDashing; // Is the player dashing or not?
    private float dashDirection = 1f; // Direction the player will dash in
                                      // (positive = right, negative = left)
    [SerializeField] float dashRate = 0.5f;
    [SerializeField] private float dashSpeed = 15f; // Velocity of the player's dash
    [SerializeField] private float dashTime = 0.4f; // Duration of the player's dash
    private float nextDash = 0f; // When the player can dash next

    // Animation members
    private Animator anim; // Reference to animator component
    public GameObject dashRender; // reference to the dash renderer component

    // Audio members
    public AudioSource walkingSound;

    private void Start()
    {
        anim = GetComponent<Animator>(); // Sets animator reference
        dashRender = GameObject.Find("/PlayerCharacter/Dash art");
        dashRender.GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        // Allows the player to dash again if they are on the ground
        if (IsGrounded())
            canDash = true;

        // Sets the player direction
        horizontal = Input.GetAxisRaw("Horizontal");

        // Jump when jump button is pressed (w or up) and play jump sound effect
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            FindObjectOfType<audioManager>().play("jumpSound");
            anim.SetBool("isJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }

        // Makes player's jump shorter if jump button is released
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f) {
            anim.SetBool("isJumping", false);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // Performs landing animation
        if (rb.velocity.y < 0f && anim.GetBool("isJumping")==true)
        {
            anim.SetBool("isJumping", false);
        }

        // Performs run animation
        if (horizontal == 0) {
            anim.SetBool("isRunning", false);
        } else {
            anim.SetBool("isRunning", true);
        }

        

        // Performs the player action
        if (Input.GetButtonDown("Action")) {
            Action();
        }

        Flip();
    }

    private void FixedUpdate() {
        // Move player along set vector (Vector direction set in Update)
        // (isDashing will likely be replaced by var "isActing" when more powers are implimented
        if (!isDashing)
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        
        if (horizontal == 0 || IsGrounded() == false)
            walkingSound.enabled = false;
        else
            walkingSound.enabled = true;
    }

    // Performs player action
    private void Action()
    {
        Collider2D collider;

        // Interact if interactable is nearby
        if (collider = NearInteractable())
        {
            Interactable interactable;

            Debug.Log("Interact");
            interactable = collider.GetComponentInParent<Interactable>();
            interactable.run(new Player());
        }
        // Otherwise, use currently equipped star power
        else {
            switch (starPower) {
                case 0:
                    if (nextDash < Time.time && canDash == true)
                    {
                        canDash = false;
                        nextDash = Time.time + dashRate;
                        StartCoroutine(Dash(dashDirection));
                    }
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

    // Dashes the player forward and play dash sound effect
    IEnumerator Dash (float direction)
    {
        float gravity;

        FindObjectOfType<audioManager>().play("dashSound");
        isDashing = true;
        anim.enabled = false;
        dashRender.GetComponent<Renderer>().enabled = true;
        rb.velocity = new Vector2(dashSpeed * direction, 0f);
        gravity = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = gravity;
        dashRender.GetComponent<Renderer>().enabled = false;
        anim.enabled = true;
        isDashing = false;
    }

    // Flips the player's sprite horizontally when moving a different direction
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            dashDirection *= -1f;
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
    private Collider2D NearInteractable()
    {
        return Physics2D.OverlapCircle(interactCheck.position, 1.0f, interactLayer);
    }
}
