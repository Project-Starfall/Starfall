using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Lvl3Aligator : MonoBehaviour
{
	// [SerializeField] Timeline GameObject
    [SerializeField] PlayableDirector AlligatorFlop;
	
    // Animation members
    [SerializeField] Animator anim; // Reference to animator component
	
    // Player
    [SerializeField]
    Player player;
    [SerializeField]
    PlayerMovement playerMovement;

	// Renderers
	[SerializeField]
	FishingHandler FishingHandler;
	[SerializeField] 
	SpriteRenderer popupRenderer;

	// Colliders
	[SerializeField]
	CinemachineConfiner2D cameraConfiner;
	[SerializeField]
	PolygonCollider2D confiner;
	[SerializeField]
	BoxCollider2D blocker;
	[SerializeField]
	BoxCollider2D interactSpot;

	// Fade controls
	private bool isFade = false;
	private bool fadeIn = false;
	public float fadeSpeed = 0.5f;
	private float fadeAmount = 0f;
	Color objectColor;

    public void Start()
    {
		objectColor = popupRenderer.material.color;
		popupRenderer.material.color = new Color(objectColor.r, objectColor.g, objectColor.b, 0f);
	}

    void OnTriggerEnter2D(Collider2D collision)
	 {
		if(FishingHandler.fishCount != 3)
		//if (FishingHandler.fishCount == 3)
		{
			playerMovement.DisableMovement();
			interactSpot.enabled = false;
			AlligatorFlop.Play();
		}
		else 
		{ 
			anim.SetBool("InCollider", true);
			isFade = true;
			fadeIn = true;
		}
	  }
 
	 void OnTriggerExit2D(Collider2D collision)
	 {
	     anim.SetBool("InCollider", false);
		isFade = true;
		fadeIn = false;
	 }

    public void Update()
    {
		objectColor = popupRenderer.material.color;
		if (isFade)
		{
			if(fadeIn)
            {
				fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
				if(fadeAmount >= 1f)
                {
					fadeAmount = 1f;
					isFade = false;
                }
			}
			else
            {
				fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
				if(fadeAmount <= 0f)
                {
					fadeAmount = 0f;
					isFade = false;
                }
            }
			objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
			popupRenderer.material.color = objectColor;
		}
	}

	public void gatorHasFlopped()
    {
		interactSpot.enabled = false;
		playerMovement.EnableMovement();
		blocker.enabled = false;
		cameraConfiner.m_BoundingShape2D = confiner;
	}
}
