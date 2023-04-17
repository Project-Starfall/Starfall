using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering.Universal;
using static SaveSystem;
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

   // Timelines
   [SerializeField] PlayableDirector startTimeline;

    private void Awake()
    {
        Application.targetFrameRate = 60;
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
        startTimeline.Play();
        //playerMovement.DisableMovement();
        //startTimeline.Play();

        playerSeed = player.seed;
        System.Random random = new System.Random(playerSeed);

        pipe5Seed = random.Next(0, 10000);
        component1Seed = random.Next(0, 1000);
        component2Seed = random.Next(0, 1000);
        component3Seed = random.Next(0, 1000);

        componentPuzzle[0].setEnabled(false);
        componentPuzzle[1].setEnabled(false);
        componentPuzzle[2].setEnabled(false);
    }

    public void completePuzzlePipe()
    {
        currentPuzzle += 1;
        puzzleComplete[0] = 1;
        wirePuzzle.setEnabled(false);
        componentPuzzle[0].setEnabled(true);
        componentPuzzle[1].setEnabled(true);
        componentPuzzle[2].setEnabled(true);
    }

    public void completePuzzleComponent(int puzzleNumber)
    {
        puzzleComplete[puzzleNumber] = 1;
        currentPuzzle += 1;
    }
}
