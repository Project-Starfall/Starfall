using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelZeroMap : MonoBehaviour, Interactable
{
   [SerializeField] private ParticleSystem particles;
   [SerializeField] private SpriteRenderer spriteRenderer;
   [SerializeField] private SpriteRenderer shadowRenderer;
   [SerializeField] private Color color;
   [SerializeField] private TutoritalHandler handler;
   [SerializeField] private SpriteRenderer popupRenderer;

   private Material glowMaterial;
   private bool isFade = false;
   private bool fadeIn = false;
   private float fade = 0f;

   private bool fadePopup;
   private bool inPopup;
   private Color objectColor;
   private float fadeAmount = 0f;
   [SerializeField]
   private float fadeSpeed = 1f;

   private bool toBeDestroyed = false;
   // Interface methods
   private bool interactEnabled = true;
   private readonly TYPE interactableType = TYPE.Item;

   public void OnTriggerEnter2D(Collider2D collision)
   {
      onEnter();
   }

   public void OnTriggerExit2D(Collider2D collision)
   {
      onLeave();
   }



   public void onEnter() {
      particles.Play();
      isFade = true;
      fadeIn = true;
      fadePopup= true;
      inPopup= true;
   }

   public void onLeave() {
      particles.Stop();
      isFade = true;
      fadeIn = false;
      fadePopup = true;
      inPopup = false;
   }

   public bool run(Player player) {
      Debug.Log("Runs map");
      interactEnabled = false;
      handler.pickupMap();
      particles.Stop();
      spriteRenderer.enabled= false;
      shadowRenderer.enabled= false;
      toBeDestroyed= true;
      fadePopup= true;
      inPopup = false;
      return true;
   }

   public TYPE getType() {
      return interactableType;
   }

   void Start()
   {
      particles.Stop();
      glowMaterial = spriteRenderer.material;
      glowMaterial.SetFloat("_Fade", 0f);
   }

   // Update is called once per frame
   void Update()
   {
      if (isFade)
      {
         if (!fadeIn)
         {
            fade -= Time.deltaTime;
            if (fade <= 0f)
            {
               fade = 0f;
               isFade = false;
            }
         }
         else
         {
            fade += Time.deltaTime;
            if (fade >= 1f)
            {
               fade = 1f;
               isFade = false;
            }
         }
         glowMaterial.SetFloat("_Fade", fade);
      }

      if (fadePopup)
      {
         objectColor = popupRenderer.color;

         if (inPopup) {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
            if(fadeAmount >= 64f)
            {
               fadeAmount = 64f;
               fadePopup= false;
            }
         }
         else
         {
            fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
            if (fadeAmount <= 0f)
            {
               
               fadeAmount = 0f;
               fadePopup = false;
            }
         }
         if(toBeDestroyed && fadeAmount == 0f) Destroy(gameObject);
         objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
         popupRenderer.color = objectColor;
      }
   }

   public bool isEnabled()
   {
      return interactEnabled;
   }

   public void setEnabled(bool enabled)
   {
      interactEnabled = enabled;
   }
}
