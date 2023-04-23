
using UnityEngine;

public class RescueLevelOne : MonoBehaviour
{
   [SerializeField] SpriteRenderer spriteRenderer;
   [SerializeField] Texture2D proneAlphaMask;
   [SerializeField] Texture2D rescueAlphaMask;
   [SerializeField] GameObject star;
   [SerializeField] Level1Handler handler;

   Material glowMaterial;
    // Start is called before the first frame update
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
      handler.officeRescue();
      
      
   }

   public void movePlayer()
   {
      star.SetActive(false);
      handler.enablePlayerMove();
   }
}
