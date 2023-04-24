using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class tunnelTwo : MonoBehaviour, Interactable
{
   [SerializeField] PlayableDirector tunnelHandler2;
   [SerializeField] CinemachineConfiner2D cameraConfiner;
   [SerializeField] PolygonCollider2D tunnelConfiner;
   [SerializeField] SpriteRenderer glowRenderer;

   private bool interactEnabled = true;

   private Material glowMaterial;
   private bool isFade = false;
   private bool isFadeSpot = false;
   private bool fadeIn = false;
   private float fade = 0f;
   private float fadeSpot = 0f;
   [SerializeField] private float fadeSpeed = 1.5f;

   public void Start()
   {
      glowMaterial = glowRenderer.material;
      glowMaterial.SetFloat("_Fade", 0f);
   }

   public void Update()
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

      if (isFadeSpot)
      {
         if (!fadeIn)
         {
            fadeSpot -= (fadeSpeed * Time.deltaTime);
            if (fadeSpot <= 0f)
            {
               fadeSpot = 0f;
               isFadeSpot = false;
            }
         }
         else
         {
            if (interactEnabled)
            {
               fadeSpot += (fadeSpeed * Time.deltaTime);
               if (fadeSpot >= 1f)
               {
                  fadeSpot = 1f;
                  isFadeSpot = false;
               }
            }
         }
         glowRenderer.color = new Color(glowRenderer.color.r, glowRenderer.color.g, glowRenderer.color.b, fadeSpot);
      }
   }

   // to be removed
   public TYPE getType()
   {
      throw new System.NotImplementedException();
   }

   public bool isEnabled()
   {
      return interactEnabled;
   }

   public void onEnter()
   {
      if (!interactEnabled) return;
      isFade = true;
      fadeIn = true;
      isFadeSpot = true;
   }

   public void onLeave()
   {
      isFade = true;
      isFadeSpot = true;
      fadeIn = false;

   }

   public bool run(Player player)
   {
      tunnelHandler2.Play();
      StartCoroutine(switchCamera());
      return true;
   }

   public void setEnabled(bool enabled)
   {
      interactEnabled = enabled;
   }

   private IEnumerator switchCamera()
   {
      yield return new WaitForSeconds(0.35f);
      cameraConfiner.m_BoundingShape2D = tunnelConfiner;
      yield return null;
   }

   public void OnTriggerEnter2D(Collider2D collision)
   {
      onEnter();
   }

   public void OnTriggerExit2D(Collider2D collision)
   {
      onLeave();
   }
}
