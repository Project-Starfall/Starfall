using UnityEngine;
using UnityEngine.Playables;

public class Lvl3RescueAnimScript : MonoBehaviour
{
	// [SerializeField] Timeline GameObject
    [SerializeField] PlayableDirector RescueAnimation;

    // Star
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Texture2D proneAlphaMask;
    [SerializeField] Texture2D rescueAlphaMask;
    [SerializeField] GameObject star;
    Material glowMaterial;

    // Player
    [SerializeField]
    PlayerMovement playerMovement;

    void Start()
    {
        glowMaterial = spriteRenderer.material;
        glowMaterial.SetTexture("_AlphaMask", proneAlphaMask);
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


    public void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement.DisableMovement();
	   Debug.Log("Entered anim collider");
 	   RescueAnimation.Play();
 	   return;
    }
}
