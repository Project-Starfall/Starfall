using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInteractable : MonoBehaviour, Interactable
{

   private bool interactEnabled = true;
   private bool active = false;
   private readonly TYPE interactableType = TYPE.Item;
   [SerializeField] GameObject note;

   public TYPE getType()
   {
      return interactableType;
   }

   public bool isEnabled()
   {
      return interactEnabled;
   }

   public void onEnter()
   {
      throw new System.NotImplementedException();
   }

   public void onLeave()
   {
      throw new System.NotImplementedException();
   }

   public bool run(Player player)
   {
      note.transform.localScale = new Vector2(28, 28);
      active = true;
      return true;
   }

   private void Update()
   {
      if (!active) return;

      if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.RightShift))
      {
         note.transform.localScale = new Vector2(0, 0);
         active = false;
      }

   }

   public void setEnabled(bool enabled)
   {
      interactEnabled = enabled;
   }
}
