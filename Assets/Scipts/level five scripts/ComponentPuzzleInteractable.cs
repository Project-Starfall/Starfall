using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPuzzleInteractable : MonoBehaviour, Interactable
{
    // Interactable Fields
    private bool interactEnabled;

    // Reference to game objects
    [SerializeField] GameObject componentGame;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem particles;
    [SerializeField] PauseMenu menu;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Level5Handler level5Handler;
    [SerializeField] ComponentHandler componentHandler;
    [SerializeField] int puzzleNumber;

    // Data
    private bool active = false; // if UI is active

    // InteractGlow data
    private Material glowMaterial;
    private bool isFade = false;
    private bool fadeIn = false;
    private float fade = 0f;

    // Setup the glow material at very first start tick
    public void Start()
    {
        glowMaterial = spriteRenderer.material;
        glowMaterial.SetFloat("_Fade", 0f);

        interactEnabled = true;
    }

    // Used to enable the game canvas
    private void openGame()
    {
        componentGame.SetActive(true); // Shows the component game
        menu.isUIOpen = true; // Tells the pause menu to ignore esc;
        setEnabled(false);    // prevents opening twice
        StartCoroutine(delayedActive());  // prevents opening before important systems
        playerMovement.DisableMovement(); // Stop the player :3
    }

    // Used to disable the game canvas and disable the interact spot if completed
    private void closeGame(bool completed)
    {
        if (completed)
        {
            setEnabled(false);
            StartCoroutine(completedDelay());
            playerMovement.EnableMovement();
            particles.Stop();
            isFade = true;
            fadeIn = false;
        }
        else
        {
            componentHandler.closeGame();
            componentGame.SetActive(false); // closes the game on the canvas
            StartCoroutine(delayedDeactive());
            menu.isUIOpen = false;
            playerMovement.EnableMovement();
        }
    }

    public void completed()
    {
        closeGame(true);
        level5Handler.completePuzzleComponent(puzzleNumber);
    }

    public void Update()
    {
        // Handles the fade of the glow
        if (isFade)
        {
            if (!fadeIn)
            {
                fade -= Time.deltaTime;
                if (fade <= 0f)
                {
                    fade = 0f;
                    isFade = false;
                }
            }
            else
            {
                fade += Time.deltaTime;
                if (fade >= 1f)
                {
                    fade = 1f;
                    isFade = false;
                }
            }
            glowMaterial.SetFloat("_Fade", fade);
        }

        // Dont even bother checking the inputs, the game isnt active
        if (!active) return;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Action"))
        {
            closeGame(false);
        }
    }

    #region Interface Methods
    public bool run(Player player)
    {
      switch(puzzleNumber)
      {
         case 1:
            componentHandler.startGame(level5Handler.component1Seed, this);
            break;
         case 2:
            componentHandler.startGame(level5Handler.component2Seed, this);
            break;
         case 3:
            componentHandler.startGame(level5Handler.component3Seed, this);
            break;
      }
        
        openGame();
        return true;
    }

    // To be removed from interface
    public TYPE getType()
    {
        throw new System.NotImplementedException();
    }

    public bool isEnabled()
    {
        return interactEnabled;
    }

    public void onEnter()
    {
        isFade = true;
        fadeIn = true;
    }

    public void onLeave()
    {
        isFade = true;
        fadeIn = false;
    }

    public void setEnabled(bool enabled)
    {
        interactEnabled = enabled;
    }
    #endregion

    public IEnumerator delayedActive()
    {
        yield return new WaitForSeconds(0.1f);
        active = true;
        yield return null;
    }

    public IEnumerator delayedDeactive()
    {
        yield return new WaitForSeconds(0.1f);
        setEnabled(true);
        active = false;
        yield return null;
    }

   public IEnumerator completedDelay()
   {
      yield return new WaitForSeconds(1f);
      menu.isUIOpen = false;
      active = false;
      componentHandler.closeGame();
      componentGame.SetActive(false);
      yield return null;
   }

   public void OnTriggerEnter2D(Collider2D collision)
    {
        if(interactEnabled) onEnter();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        onLeave();
    }
}
