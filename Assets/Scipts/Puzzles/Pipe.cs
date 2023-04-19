using UnityEngine;
using static Constants.Pipes;

// Names the types of pipes
public enum PIPE_TYPE {
   Straight,
   Corner,
   Junction,
   Cross,
   Start,
   End,
   Empty
}

public class Pipe : MonoBehaviour {
   [SerializeField] private PipePuzzleGameHandler handler; // The game handler
   [SerializeField] private PIPE_TYPE pipeType; // The type of the pipe
   private int[] definition = { 0, 0, 0, 0 };   // Defines which sides of the pipe is the connection
                                                // {Left, Bottom, Right, Top}
   private int orientation = 0;                 // The orientation of the pipe
   [SerializeField ]private bool isPowered = false; // Indicate that the currently has power
   [SerializeField] public int posX;
   [SerializeField] public int posY;

   [SerializeField] Material materialGlow;
   [SerializeField] Material materialGlownot;
   private SpriteRenderer pipeRenderer;

   // Start is called before the first frame update
   public void loadRenderer() {
      pipeRenderer = GetComponentInParent<SpriteRenderer>();
   }

   private void OnMouseDown() {
      if (!handler.canPlay) return;
      FindObjectOfType<audioManager>().play("pipeRotate");
      orientation = (orientation + 1) % 4;
      transform.Rotate(new Vector3(0, 0, -90));
      handler.checkPower();
   }

   public void setPoweredMaterial(bool powered)
   {
      if (powered) pipeRenderer.material = materialGlow;
      else pipeRenderer.material = materialGlownot;
   }

   //initialize the Pipe object setting the definitions for the given Pipetype
   public void setType(PIPE_TYPE pipeType) {
      this.pipeType = pipeType;
      definition = new int[4] { 0, 0, 0, 0 };
      //There is probably a painfully more easy way to do this
      switch (pipeType) {
         case PIPE_TYPE.Straight:
            definition[LEFT] = 1;
            definition[RIGHT] = 1;
            break;
         case PIPE_TYPE.Corner:
            definition[LEFT] = 1;
            definition[BOTTOM] = 1;
            break;
         case PIPE_TYPE.Cross:
            definition[LEFT] = 1;
            definition[BOTTOM] = 1;
            definition[RIGHT] = 1;
            definition[TOP] = 1;
            break;
         case PIPE_TYPE.Junction:
            definition[LEFT] = 1;
            definition[BOTTOM] = 1;
            definition[RIGHT] = 1;
            break;
         case PIPE_TYPE.Start:
            definition[RIGHT] = 1;
            isPowered = true;
            break;
         case PIPE_TYPE.End:
            definition[LEFT] = 1;
            break;
         case PIPE_TYPE.Empty:
            definition[LEFT] = 0;
            definition[BOTTOM] = 0;
            definition[RIGHT] = 0;
            definition[TOP] = 0;
            break;
         default:
            UnityEngine.Debug.Log("Could not create pipe object.\n Unknown pipe type given");
            break;
      }
   }

   //Rotate the pipe
   public void rotate(int rotation)
   {
      orientation = (orientation + rotation) % 4;
      transform.Rotate(new Vector3(0, 0, rotation * -90));
   }

   // Getters and setters for the object fields
   //**********************************************/
   public PIPE_TYPE getType() {
      return pipeType;
   }
   public int getOrientation() {
      return orientation;
   }
   public int[] getDefinition() {
      return definition;
   }
   public bool getIsPowered() {
      return isPowered;
   }
   public void setOrientation(int value) {
      orientation = value;
   }
   public void setIsPowered(bool value) {
      isPowered = value;
   }
   //**********************************************/
}

