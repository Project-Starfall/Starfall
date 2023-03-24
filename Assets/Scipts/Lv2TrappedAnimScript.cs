using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Lv2TrappedAnimScript : MonoBehaviour
{
	// [SerializeField] Timeline GameObject
    [SerializeField] PlayableDirector TrappedAnimation;
	
    // Player
    [SerializeField]
    PlayerMovement playerMovement;
	
    public void OnTriggerEnter2D(Collider2D collision)
    {
	   Debug.Log("Entered anim collider");
 	   TrappedAnimation.Play();
 	   return;
    }

}
