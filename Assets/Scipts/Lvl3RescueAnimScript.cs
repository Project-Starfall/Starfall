using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Lvl3RescueAnimScript : MonoBehaviour
{
	// [SerializeField] Timeline GameObject
    [SerializeField] PlayableDirector RescueAnimation;
	
    // Player
    [SerializeField]
    PlayerMovement playerMovement;
	
    public void OnTriggerEnter2D(Collider2D collision)
    {
	   Debug.Log("Entered anim collider");
 	   RescueAnimation.Play();
 	   return;
    }
}
