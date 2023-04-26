using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering.Universal;
using static SaveSystem;
using static UnityEngine.ParticleSystem;

public class Level5Handler : MonoBehaviour
{
    // Player
    [SerializeField] Player player;
    [SerializeField] PlayerMovement playerMovement;
    int playerSeed;

    // Puzzle info
    [SerializeField] WirePuzzleScript wirePuzzle;
    [SerializeField] ComponentPuzzleInteractable[] componentPuzzle;

    // Puzzle Data
    public int pipe5Seed { get; set; }
    public int component1Seed { get; set; }
    public int component2Seed { get; set; }
    public int component3Seed { get; set; }
    public int[] puzzleComplete { get; set; } = { 0, 0, 0, 0 };
    public int currentPuzzle = 0;

   // Level objects
   [SerializeField]
   public Light2D[] rocketLights;
   [SerializeField]
   public ParticleSystem[] thrusterParticles;
   [SerializeField] 
   public ParticleSystem[] steamParticles;
    [SerializeField]
    public GroundLights groundLights;
   [SerializeField] HatchInteractable launchButton;

   // Timelines
   [SerializeField] PlayableDirector startTimeline;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        // transition background music into level five music
        //FindObjectOfType<audioManager>().musicFadeOut("menuMusic"); // only if save game feature is added
        FindObjectOfType<audioManager>().musicFadeOut("levelFourMusic");
        FindObjectOfType<audioManager>().stop("levelFourMusic");
        FindObjectOfType<audioManager>().play("levelFiveMusic");
        FindObjectOfType<audioManager>().musicFadeIn("levelFiveMusic");
    }

    void Start()
    {
        // save the player data
        SaveGame(player);
        Debug.Log("Saving the player...");
        // load player save
        LoadGame();
        Debug.Log("Loading the game...");

        playerMovement.manualFlip(false);
      playerMovement.DisableMovement();
        startTimeline.Play();
        //playerMovement.DisableMovement();
        //startTimeline.Play();

        playerSeed = player.seed;
        System.Random random = new System.Random((int) Time.time);

        pipe5Seed = random.Next(0, 10000);
        component1Seed = random.Next(0, 1000);
        component2Seed = random.Next(0, 1000);
        component3Seed = random.Next(0, 1000);

        componentPuzzle[0].setEnabled(false);
        componentPuzzle[1].setEnabled(false);
        componentPuzzle[2].setEnabled(false);
        launchButton.setEnabled(false);
    }

    public void completePuzzlePipe()
    {
        currentPuzzle += 1;
        puzzleComplete[0] = 1;
        wirePuzzle.setEnabled(false);
        componentPuzzle[0].setEnabled(true);
        componentPuzzle[1].setEnabled(true);
        componentPuzzle[2].setEnabled(true);
        rocketStageOne();
        for (int i = 0; i < 4; i++)
        {
            if (puzzleComplete[i] == 1) rocketLights[i].enabled = true;
        }
        if (currentPuzzle >= 4)
        {
           allPuzzleComplete();
        }
   }

    public void completePuzzleComponent(int puzzleNumber)
    {
        puzzleComplete[puzzleNumber] = 1;
        currentPuzzle += 1;
        for(int i = 0; i < 4; i++)
        {
            if (puzzleComplete[i] == 1) rocketLights[i].enabled = true;
        }
        if(currentPuzzle >= 4)
        {
            allPuzzleComplete();
        }
        rocketSubsequentStages();
    }

    public void allPuzzleComplete()
    {
        rocketLights[4].color = new Color(0f, 1f, 0f);
        groundLights.lightColor = new Color(0f, 1f, 0f);
        launchButton.setEnabled(true);
    }

    // Called after the pipe puzzle is first completed
    public void rocketStageOne()
    {
        foreach (ParticleSystem particles in thrusterParticles)
        {
            EmissionModule emission= particles.emission;
            emission.rateOverTime = 5;
            particles.Play();         
        }
    }

    // Every stage after the first one
    public void rocketSubsequentStages()
    {
        foreach(ParticleSystem particles in thrusterParticles)
        {
            EmissionModule emission = particles.emission;
            emission.rateOverTime = 10 * currentPuzzle;
        }
    }

     public bool isComplete()
     {
         return currentPuzzle >= 4;
     }

   public void unlockPlayer()
   {
      playerMovement.EnableMovement();
   }
}
