using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelZeroMap : MonoBehaviour, Interactable
{
   private readonly TYPE itemType = TYPE.Item; // The interactable tpye of the item
   [SerializeField] private ParticleSystem particles;
   [SerializeField] private Color color;

   public void onEnter() {

   }

   public void onLeave() {

   }

   public bool run(Player player) {
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
