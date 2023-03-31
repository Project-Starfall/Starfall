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
    [SerializeField] private Material lineSpriteMaterial; // Material of the fishing material
    [SerializeField] private SpriteRenderer movingFishSprite; // Fish sprite from bottom of water
    [SerializeField] private GameObject rodConnection; // Where the line connects to the rod
    private int trashLayer; // Int value of the "Trash" layer mask
    [SerializeField] private LayerMask trashLayerMask; // The "Trash" layer mask

    // Game members
    private bool gameCompleted;
    private int gameNum;

    // References to game objects
    [SerializeField] FishingSpotInteractable game1Interact;
    [SerializeField] FishingSpotInteractable game2Interact;
    [SerializeField] FishingSpotInteractable game3Interact;

    // Start is called before the first frame update
    void Start()
    {
        LineRenderer line = gameObject.AddComponent<LineRenderer>();
        line.positionCount = 2;
        line.sortingOrder = 4;
        Material lineMaterial = GetComponent<LineRenderer>().material;
        lineMaterial = lineSpriteMaterial;
        lineMaterial.color = lineColor;
        line.material = lineMaterial;
        line.widthMultiplier = 0.05f;
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

            // Cast the fishing rode
            if (Input.GetButtonDown("Cast"))
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
        // Reel in hook when it hits the fish
        if (hookCollider.IsTouchingLayers(fishLayer))
        {
            gameCompleted = true;
            FishOnHook(true);
            ReelRod();
        }
        // Reel in hook when it hits the bottom of the water
        if (hook.transform.localPosition.y <= -2.90f)
        {
            ReelRod();
        }

        // Complete game
        if (!isReeling && !isCasting && gameCompleted)
        {
            CompleteGame(gameCompleted, gameNum);
        }

        // Reenable movement when resting
        if (!isReeling && !isCasting && !gameCompleted)
            DisableMovement(false);

        // Set fishing line position
        line.SetPosition(0, rodConnection.transform.position);
        line.SetPosition(1, hookConnection.transform.position);
    }

    private void FixedUpdate()
    {
        // Moves the character
        transform.Translate(new Vector3((horizontal * playerSpeed) * Time.deltaTime, 0, 0));
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -2.9f, 3.2f), 0, 0);

        // Handles casting and reeling the rod
        if (isCasting)
            hook.transform.Translate(new Vector3(0, (hookSpeed * -1) * Time.deltaTime, 0));
        if (isReeling)
            hook.transform.Translate(new Vector3(0, (hookSpeed * 1) * Time.deltaTime, 0));
    }

    // Disable character movement
    public void DisableMovement(bool p)
    {
        if (p == true)
        {
            movementEnabled = false;
            horizontal = 0f;
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

    public void FishOnHook(bool v)
    {
        if (v == true)
        {
            movingFishSprite.enabled = false;
            hookedFishSprite.enabled = true;
        }
        else
        {
            movingFishSprite.enabled = true;
            hookedFishSprite.enabled = false;
        }
    }

    public void SetGameNum(int num)
    {
        gameNum = num;
    }

    public void SetHorizontal(float h)
    {
        horizontal = h;
    }

    private void CompleteGame(bool completed, int gameNum)
    {
        if (!completed) { return; }
        switch (gameNum)
        {
            case 1:
                game1Interact.SetComplete(true);
                Debug.Log("GAME 1 CLEAR");
                break;
            case 2:
                game2Interact.SetComplete(true);
                Debug.Log("GAME 2 CLEAR");
                break;
            case 3:
                game3Interact.SetComplete(true);
                Debug.Log("GAME 3 CLEAR");
                break;
        }
    }

    public void SetComplete(bool v)
    {
        gameCompleted = v;
    }
}
