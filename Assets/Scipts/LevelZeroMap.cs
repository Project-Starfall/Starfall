using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelZeroMap : MonoBehaviour, Interactable
{
   [SerializeField] private ParticleSystem particles;
   [SerializeField] private Color color;

   // Interface methods
   private bool interactEnabled = true;
   private readonly TYPE interactableType = TYPE.Item;

   public void onEnter() {
      Debug.Log("Entered Range");
      var spriteRenderer = GetComponentInParent<SpriteRenderer>();
   }

   public void onLeave() {
      Debug.Log("Exited Range");
   }

   public bool run(Player player) {
      Debug.Log("Runs map");
      Destroy(gameObject);
      return true;
   }

   public TYPE getType() {
      return interactableType;
   }

    // Update is called once per frame
    void Update()
    {
        
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
