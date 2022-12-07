using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelZeroMap : MonoBehaviour, Interactable
{
   private readonly TYPE itemType = TYPE.Item; // The interactable type of the item
   [SerializeField] private ParticleSystem particles;
   [SerializeField] private Color color;

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
      return this.itemType;
   }

    // Update is called once per frame
    void Update()
    {
        
    }
}