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
        glowMaterial.SetTexture("_AlphaMask", tiedUpAlphaMask);
    }

    public void rescueMask()
    {
        glowMaterial.SetTexture("_AlphaMask", rescueAlphaMask);
    }

    public void endRescue()
    {
        playerMovement.EnableMovement();
        star.SetActive(false);
    }
}
