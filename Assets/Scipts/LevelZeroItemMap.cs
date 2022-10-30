using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour, Interactable
{
   private readonly TYPE itemType = TYPE.Item; // The interactable tpye of the item

   public void onDetect() {

   }

   public void offDetect() {

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
