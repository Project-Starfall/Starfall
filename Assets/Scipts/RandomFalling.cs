using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFalling : MonoBehaviour
{

   [SerializeField]
   private Rigidbody2D rb;
   [SerializeField]
   private SpriteRenderer spriteRenderer;
   private Color objectColor;
   private float fadeAmount;
   [SerializeField]
   private float fadeSpeed;

   bool fade = false;

    // Start is called before the first frame update
    void Start()
    {
      ;
      System.Random random = new System.Random();
      rb.velocity = new Vector2((float) random.NextDouble() * random.Next(-2, 3), random.Next(1,5));
      StartCoroutine(waitToFade());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      if (fade)
      {
         objectColor = spriteRenderer.material.color;
         fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

         objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
         spriteRenderer.material.color = objectColor;

         if (objectColor.a <= 0) Destroy(this.gameObject);
      }
    }

   IEnumerator waitToFade()
   {
      yield return new WaitForSeconds(0.5f);
      fade = true;
      yield return null;
   }
}
