using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class StarRescueFlying : MonoBehaviour
{
   [SerializeField] Transform startTransform;
   [SerializeField] Transform endTransform;
   [SerializeField] GameObject flyingStar;
   [SerializeField] AnimationCurve curve;
   [SerializeField] Image starImage;
   [SerializeField] Sprite emptyToFilled;
   [SerializeField] ParticleSystem particles;
   [SerializeField] SpriteRenderer spriteRenderer;

   public float hudPosFromPlayerX = 0;
   public float hudPosFromPlayerY = 0;

   private float startX = 0;
   private float startY = 0;
   private float endX    = 0;
   private float endY = 0;
   private float posY   = 0;
   private float posX   = 0;
   public float speed  = 1f;
   public float fadeSpeed = 1f;
   public float padding = 0.25f;

   public bool test;


   public void Update()
   {
      if(test)
      {
         test = false;
         StartAnimation();
      }
   }

   public void StartAnimation()
   {
      particles.Play();
      endX = endTransform.position.x + hudPosFromPlayerX;
      endY = endTransform.position.y + hudPosFromPlayerY;
      startX = startTransform.position.x;
      startY = startTransform.position.y;
      posX = 0f;
      posY = 0f;
      StartCoroutine(Play());
   }

   public IEnumerator Play()
   {
      Color tempColor = spriteRenderer.color;
      for(float interpolate = 0f; interpolate <= 1f; interpolate += speed * Time.deltaTime)
      {
         posX = Mathf.Lerp(startX, endX, curve.Evaluate(interpolate));
         posY = Mathf.Lerp(startY, endY, interpolate);

         if(Math.Abs(posX - endX) < padding && Math.Abs(posY - endY) < padding)
         {
            starImage.sprite = emptyToFilled;
            startTransform.position = new Vector2(endX, endY);
            break;
         }
         startTransform.position = new Vector2(posX, posY);
         
         yield return new WaitForEndOfFrame();
      }
      startTransform.position = new Vector2(endX, endY);
      for (float fade = 1f; fade > 0f; fade -= fadeSpeed * Time.deltaTime)
      {
         spriteRenderer.color = new Color(tempColor.r, tempColor.g, tempColor.b, fade);
         yield return new WaitForEndOfFrame();
      }
      particles.Stop();
      spriteRenderer.color = new Color(tempColor.r, tempColor.g, tempColor.b, 0f);
      yield return null;
      
   }
}
