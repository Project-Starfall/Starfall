using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Lv2TrappedAnimScript : MonoBehaviour
{
	// [SerializeField] Timeline GameObject
    [SerializeField] PlayableDirector TrappedAnimation;

    // Refrences to other components
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Texture2D tiedUpAlphaMask;
    [SerializeField] Texture2D rescueAlphaMask;
    [SerializeField] Material rescueMaterial;

   [SerializeField] StarRescueFlying flyingStar;

    // Player
    [SerializeField]
    PlayerMovement playerMovement;
    [SerializeField]
    GameObject star; // Our star friend

    // Data
    Material glowMaterial;
	
    public void OnTriggerEnter2D(Collider2D collision)
    {
	   Debug.Log("Entered anim collider");
       playerMovement.DisableMovement();
 	   TrappedAnimation.Play();
 	   return;
    }

    void Start()
    {
        glowMaterial = spriteRenderer.material;
    }

    public void rescueMask()
    {
      glowMaterial = rescueMaterial;
      spriteRenderer.material = glowMaterial;
    }

   public void startFlying()
   {
      spriteRenderer.enabled = false;
      flyingStar.StartAnimation();
   }

    public void endRescue()
    {
        playerMovement.EnableMovement();
        star.SetActive(false);
    }
}
