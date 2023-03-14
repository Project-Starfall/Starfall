using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficePopup : MonoBehaviour
{
   [SerializeField] private SpriteRenderer popupRenderer;
   private bool fadePopup;
   private bool inPopup;
   private Color objectColor;
   private float fadeAmount = 0f;
   [SerializeField]
   private float fadeSpeed = 1f;

   // Start is called before the first frame update
   public void popup(bool value)
    {
      if(value)
      {
         fadePopup= true;
      }
    }

   // Update is called once per frame
   void Update()
   {
      if (fadePopup)
      {
         objectColor = popupRenderer.color;

            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
            if (fadeAmount >= 64f)
            {
               fadeAmount = 64f;
               fadePopup = false;
            }
         objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
         popupRenderer.color = objectColor;
      }
   }
}
