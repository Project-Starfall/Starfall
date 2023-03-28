using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FishingHandler : MonoBehaviour
{
    [SerializeField] GameObject fishingGameParent;
    [SerializeField] FishingSpotInteractable game1Interact;
    [SerializeField] FishingSpotInteractable game2Interact;
    [SerializeField] FishingSpotInteractable game3Interact;

    public int fishCount;
 
    // Start is called before the first frame update
    void Start()
    {    
        fishCount = 0;
        fishingGameParent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // WHEN ALL FISHING GAMES ARE COMPLETE, fishCount WILL BE EQUAL TO 3
    }
}
