using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StarRescueFlying : MonoBehaviour
{
   [SerializeField] Transform startTransform;
   [SerializeField] Transform endTransform;
   [SerializeField] GameObject changingStar;
   [SerializeField] GameObject flyingStar;
   [SerializeField] AnimationCurve curve;

   private Vector2 endWorldSpace;
   private Vector2 startWorldSpace;
   private Vector2 updatedPosition;
   private float xPos = 0;
   private float speed = 0.5f;
   public bool test = false;


   public void Start()
   {
      
   }

   public void Update()
   {
      if(test)
      {
         endWorldSpace = Camera.main.ViewportToWorldPoint(changingStar.transform.position);
         startWorldSpace = startTransform.position;
         updatedPosition = startWorldSpace;
         xPos = (Math.Abs(startWorldSpace.x) - Math.Abs(endWorldSpace.x)) / Math.Abs(startWorldSpace.x);
         test = false;
         StartCoroutine(Play());
      }
   }

   public void Playanimation()
   {
      while(true)
      {
         Debug.Log($"{curve.Evaluate(startWorldSpace.x)}");
         xPos += speed * Time.deltaTime;
         startWorldSpace.x= xPos;
         if (xPos >= 1f) return;
      }
   }

   public IEnumerator Play()
   {
      Playanimation();
      yield return null;
   }
}
