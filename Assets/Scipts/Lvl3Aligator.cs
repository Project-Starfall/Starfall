using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Lvl3Aligator : MonoBehaviour
{
	// [SerializeField] Timeline GameObject
    [SerializeField] PlayableDirector Alligator;
	
    // Animation members
    [SerializeField] Animator anim; // Reference to animator component
	
    // Player
    [SerializeField]
    Player player;
    [SerializeField]
    PlayerMovement playerMovement;
	[SerializeField]
	FishingHandler FishingHandler;
	
	void OnTriggerEnter2D(Collider2D collision)
	 {
	     anim.SetBool("InCollider", true);
	  }
 
	 void OnTriggerExit2D(Collider2D collision)
	 {
	     anim.SetBool("InCollider", false);
	 }
     void Update()
     {
	    if(FishingHandler.fishCount == 3)
	    {
	        playerMovement.DisableMovement();
	        Alligator.Play();
		    playerMovement.EnableMovement();
	    }
	 }
}
