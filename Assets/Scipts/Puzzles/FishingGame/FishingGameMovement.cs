using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingGameMovement : MonoBehaviour
{
    /*
     * Notes:
     *  Starting depth of hook is 1.56f
     *  Max depth of hook is -3.3f
     */

    // Movement members
    private bool isCasting; // Is the fishing rod casting?
    [SerializeField] private float hookSpeed; // Speed at which the hook sinks
    private float horizontal; // Character movement direction
    private bool movementEnabled; // Can the character move or not?
    [SerializeField] private float playerSpeed; // Speed of the character
    private bool isReeling; // Is the fishing rod reeling?

    // Rod/Line members
    private int fishLayer; // The value of the "Fish" layer mask
    [SerializeField] private LayerMask fishLayerMask; // The "Fish" layer mask
    [SerializeField] private GameObject hook; // Hook Gameobject
    private CircleCollider2D hookCollider; // The hook's collider
    [SerializeField] private GameObject hookConnection; // Where the line connects to the hook
    [SerializeField] private SpriteRenderer hookedFishSprite; // Fish sprite from on the fishing hook
    private Color lineColor = Color.white; // Color of the fishing line
    [SerializeField] private SpriteRenderer movingFishSprite; // Fish sprite from bottom of water
    [SerializeField] private GameObject rodConnection; // Where the line connects to the rod
    private int trashLayer; // Int value of the "Trash" layer mask
    [SerializeField] private LayerMask trashLayerMask; // The "Trash" layer mask

    // Start is called before the first frame update
    void Start()
    {
        // NOTICE: WILL BE HANDLED BY HANDLER IN FULL ECOSYSTEM

        // Set initial game values
        horizontal = 0f;
        DisableMovement(false);

        LineRenderer line = gameObject.AddComponent<LineRenderer>();
        line.widthMultiplier = 0.05f;
        line.positionCount = 2;
        line.sortingOrder = 4;
        Material lineMaterial = GetComponent<LineRenderer>().material;
        lineMaterial.color = lineColor;
        line.sortingLayerName = "UI";

        hookCollider = hook.GetComponent<CircleCollider2D>();
        trashLayer = trashLayerMask.value;
        fishLayer = fishLayerMask.value;
    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer line = GetComponent<LineRenderer>();

        if (movementEnabled == true)
        {
            // Move the character
            horizontal = Input.GetAxisRaw("Horizontal");
            transform.Translate(new Vector3((horizontal * playerSpeed) * Time.deltaTime, 0, 0));
            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -2.9f, 3.2f), 0, 0);

            // Cast the fishing rode
            if (Input.GetButtonDown("Action"))
            {
                isCasting = true;
                DisableMovement(true);
            }
        }

        // Stop reeling in hook when it reaches its resting point
        if (hook.transform.localPosition.y >= 1.56f)
            isReeling = false;
        // Reel in hook if it hits a piece of trash
        if (hookCollider.IsTouchingLayers(trashLayer))
        {
            ReelRod();
        }
        if (hookCollider.IsTouchingLayers(fishLayer))
        {
            movingFishSprite.enabled = false;
            hookedFishSprite.enabled = true;
            ReelRod();
        }
        // Reel in hook when it hits the bottom of the water
        if (hook.transform.localPosition.y <= -2.90f)
        {
            ReelRod();
        }
        

        // Reenable movement when resting
        if (!isReeling && !isCasting)
            DisableMovement(false);

        // Handles casting and reeling the rod
        if (isCasting)
            hook.transform.Translate(new Vector3(0, (hookSpeed * -1) * Time.deltaTime, 0));
        if (isReeling)
            hook.transform.Translate(new Vector3(0, (hookSpeed * 1) * Time.deltaTime, 0));

        // Set fishing line position
        line.SetPosition(0, rodConnection.transform.position);
        line.SetPosition(1, hookConnection.transform.position);
    }

    // Disable character movement
    void DisableMovement(bool p)
    {
        if (p == true)
        {
            movementEnabled = false;
        }
        else if (p == false)
        {
            movementEnabled = true;
        }
    }

    void ReelRod()
    {
        isCasting = false;
        isReeling = true;
    }
}
