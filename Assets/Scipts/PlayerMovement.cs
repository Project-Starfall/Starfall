using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
   //TODO: add pinpad popup
   // Movement members
    [SerializeField]
    private Player player; // Player Script
    private float horizontal; // Player's movement direction
    private bool isFacingRight = true; // Player orientation
    private float jumpStrength; // Player's jump force
    [SerializeField] private float normalJumpStrength; // 16f
    [SerializeField] private float speed; // Player's movement speed - 8f
    private bool isActing; // Is the player performing an action?
    public bool disableMovement { get; set; } = false;
    private float gravity;

    // Physics members
    [SerializeField] private Transform groundCheck; // Checks for ground beneath player
    [SerializeField] private LayerMask groundLayer; // Ground layer the player will walk on
    [SerializeField] private Transform interactCheck; // Checks for nearby interactable
    [SerializeField] private LayerMask interactLayer; // Layer containing interactable objects
    [SerializeField] private LayerMask grappleableLayer; // Layer containing grappleable points
    [SerializeField] private Rigidbody2D rb; // Player's physics body

    // Dash members
    bool canDash = true; // Can the player dash yet?
    private float dashDirection = 1f; // Direction the player will dash in
                                      // (positive = right, negative = left)
    [SerializeField] float dashRate; // 0.5f
    [SerializeField] private float dashSpeed; // Velocity of the player's dash - 15f
    [SerializeField] private float dashTime; // Duration of the player's dash - 0.4f
    public bool disableDash { get; set; } = false;
    private float nextDash = 0f; // When the player can dash next

    // Animation members
    private Animator anim; // Reference to animator component
    public GameObject dashRender; // reference to the dash renderer component

    // Audio members
    public AudioSource walkingSound;

    // Grapple members
    bool isGrappling = false; // Is the player grappling?
    bool isToArc = false; // Is the player to the arc of the grapple?
    float step; // Time parameter for initial grapple movement
    float t; // Time parameter for the grapple curve
    [SerializeField] private float grappleStartSpeed; // 15f
    [SerializeField] private float grappleEndSpeed; // 4f
    [SerializeField] private GrappleRope grappleRope;
    private (Transform, Transform, Transform, Transform, Transform) grapplePoints;

    // Swimming members
    private bool isSwimming; // Is the player swimming or not?
    [SerializeField] private float swimmingGravityMult; // Multiplier for player gravity whiles swimming
    private float swimmingGravity; // Gravity of player while swimming
    [SerializeField] private float swimmingJumpMult; // Multiplier for the player jump while swimming
    private float swimmingJumpStrength; // Jump force of the player while swimming

    private void Start()
    {
        anim = GetComponent<Animator>(); // Sets animator reference
        dashRender = GameObject.Find("/PlayerCharacter/Dash art");
        dashRender.GetComponent<Renderer>().enabled = false;
        t = 0f;
        step = 0f;
        isGrappling = false;
        gravity = rb.gravityScale;
        swimmingGravity = gravity * swimmingGravityMult;
        jumpStrength = normalJumpStrength;
        swimmingJumpStrength = jumpStrength * swimmingJumpMult;
        grappleRope.enabled = false;
    }

    void Update()
    {
        if (disableMovement == false)
        {
            // Sets the player direction
            horizontal = Input.GetAxisRaw("Horizontal");

            // Jump when jump button is pressed (w or up) and play jump sound effect
            if (Input.GetButtonDown("Jump") && (IsGrounded() || isSwimming))
            {
                FindObjectOfType<audioManager>().play("jumpSound");
                anim.SetBool("isJumping", true);
                rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
            }

            // Makes player's jump shorter if jump button is released
            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                anim.SetBool("isJumping", false);
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }

            // Performs the player action
            if (Input.GetButtonDown("Action"))
            {
                Action();
            }

            // Performs the dash
            if (Input.GetButtonDown("Dash"))
            {
                if (nextDash < Time.time && canDash == true && disableDash == false)
                {
                    canDash = false;
                    nextDash = Time.time + dashRate;
                    StartCoroutine(Dash(dashDirection));
                }
            }
        }

        // Allows the player to dash again if they are on the ground
        if (IsGrounded())
            canDash = true;

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

        // Performs the grapple
        if (isGrappling == true)
        {
            grappleRope.grapplePoint = grapplePoints.Item5;
            grappleRope.grappleDistanceVector = (Vector2) (grappleRope.grapplePoint.position - grappleRope.firePoint.position);
            grappleRope.enabled = true;

         if (grappleRope.canGrapple)
         {
            if (!isToArc)
            {
               step = grappleStartSpeed * Time.deltaTime;
               transform.position = Vector2.MoveTowards(transform.position, grapplePoints.Item1.position, step);
               if (transform.position == (Vector3) grapplePoints.Item1.position)
               {
                  isToArc = true;
                  step = 0f;
               }
            }
            if (isToArc)
            {
               t += Time.deltaTime * grappleEndSpeed;
               transform.position = Mathf.Pow(1 - t, 3) * grapplePoints.Item1.position +
                   3 * Mathf.Pow(1 - t, 2) * t * grapplePoints.Item2.position +
                   3 * (1 - t) * Mathf.Pow(t, 2) * grapplePoints.Item3.position +
                   Mathf.Pow(t, 3) * grapplePoints.Item4.position;
               if (t >= 1f)
               {
                  isToArc = false;
                  isGrappling = false;
                  EnableMovement();
                  grappleRope.enabled = false;
                  EnableGravity(rb);
                  t = 0f;
               }
            }
         }
        }

        Flip();
    }

    private void FixedUpdate() {
        // Move player along set vector (Vector direction set in Update)
        // (isDashing will likely be replaced by var "isActing" when more powers are implimented
        if (!isActing)
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (horizontal == 0 || IsGrounded() == false)
            walkingSound.enabled = false;
        else
            walkingSound.enabled = true;
    }

    // Performs player action
    private void Action()
    {
        Collider2D interactCheck;
        Collider2D grappleCheck;

        // Interact if interactable is nearby
        if (interactCheck = NearInteractable())
        {
            Interactable interactable;

            Debug.Log("Interact");
            interactable = interactCheck.GetComponentInParent<Interactable>();
            if(interactable.isEnabled()) interactable.run(player);
        }
        else if (grappleCheck = NearGrappleable())
        {
            Grappleable grappleable;
            Debug.Log("Grappled");
            DisableMovement();
            DisableGravity(rb);
            grappleable = grappleCheck.GetComponentInParent<Grappleable>();
            grapplePoints = grappleable.returnGrapple();
            isGrappling = true;
        }
    }

    // Dashes the player forward and play dash sound effect
    IEnumerator Dash (float direction)
    {
        FindObjectOfType<audioManager>().play("dashSound");
        isActing = true;
        anim.enabled = false;
        dashRender.GetComponent<Renderer>().enabled = true;
        rb.velocity = new Vector2(dashSpeed * direction, 0f);
        DisableGravity(rb);
        yield return new WaitForSeconds(dashTime);
        EnableGravity(rb);
        dashRender.GetComponent<Renderer>().enabled = false;
        anim.enabled = true;
        isActing = false;
    }
	
    // Flips the player's sprite horizontally when moving a different direction
    public void Flip()
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

    private Collider2D NearGrappleable()
    {
        return Physics2D.OverlapCircle(interactCheck.position, 1.0f, grappleableLayer);
    }

    private void DisableGravity(Rigidbody2D rb)
    {
        gravity = rb.gravityScale;
        rb.gravityScale = 0;
    }

    private void EnableGravity(Rigidbody2D rb)
    {
        rb.gravityScale = gravity;
    }

    public void DisableMovement()
    {
        disableMovement = true;
        horizontal = 0f;
        rb.velocity = new Vector2(0, 0);
    }

    public void EnableMovement()
    {
        disableMovement = false;
    }

    public void DisableDash(bool v)
    {
        if (v)
            disableDash = true;
        else
            disableDash = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Swimmable")
        {
            isSwimming = true;
            rb.gravityScale = swimmingGravity;
            jumpStrength = swimmingJumpStrength;
            DisableDash(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Swimmable")
        {
            isSwimming = false;
            rb.gravityScale = gravity;
            jumpStrength = normalJumpStrength;
            DisableDash(false);
        }
    }
}
