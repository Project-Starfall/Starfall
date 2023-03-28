using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Lvl3AligatorOpen : MonoBehaviour
{
	// [SerializeField] Timeline GameObject
    [SerializeField] PlayableDirector AligatorOpen;
	
    // Animation members
    private Animator anim; // Reference to animator component
	
    // Player
    [SerializeField]
    Player player;
    [SerializeField]
    PlayerMovement playerMovement;
	
	void OnTriggerEnter2D(Collider2D collision)
	 {
	     if(collision.gameObject.tag == "Player")
	     {
	         anim.SetBool("InCollider", true);
	     }
	  }
 
	 void OnTriggerExit2D(Collider2D collision)
	 {
	     if(collision.gameObject.tag == "Player")
	     {
	         anim.SetBool("InCollider", false);
	     }
	 }
}
