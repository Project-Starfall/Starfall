using UnityEngine;

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
   private PIPE_TYPE pipeType;                  // The type of the pipe
   private int[] definition = { 0, 0, 0, 0 };   // Defines which sides of the pipe is the connection
                                                // {Top, Right, Bottom, Left}
   private int orientation;                     // The orientation of the pipe
   private bool isPowered;                      // Indicate that the currently has power
   [SerializeField]
   private int posX;
   [SerializeField]
   private int posY;

   private Quaternion startingRotation;

   // Start is called before the first frame update
   void Start() {
      startingRotation = transform.rotation;
      orientation = 0;
      isPowered = false;
   }

   // Update is called once per frame
   void Update() {

   }
   private void OnMouseDown() {
      for(int i = 0; i < 90 ; i++)
         transform.Rotate(new Vector3(0, 0, 1));
   }

      //initialize the Pipe object setting the definitions for the given Pipetype
      public void selectPipeType(PIPE_TYPE pipeType) {
      this.pipeType = pipeType;
      //There is probably a painfully more easy way to do this
      switch (pipeType) {
         case PIPE_TYPE.Straight:
            definition[1] = 1;
            definition[3] = 1;
            break;
         case PIPE_TYPE.Corner:
            definition[2] = 1;
            definition[3] = 1;
            break;
         case PIPE_TYPE.Cross:
            definition[0] = 1;
            definition[1] = 1;
            definition[2] = 1;
            definition[3] = 1;
            break;
         case PIPE_TYPE.Junction:
            definition[1] = 1;
            definition[2] = 1;
            definition[3] = 1;
            break;
         case PIPE_TYPE.Start:
            definition[1] = 1;
            isPowered = true;
            break;
         case PIPE_TYPE.End:
            definition[3] = 1;
            break;
         default:
            Debug.Log("Could not create pipe object.\n Unknown pipe type given");
            break;
      }
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
   public void setType(PIPE_TYPE value) {
      pipeType = value;
   }
   public void setOrientation(int value) {
      orientation = value;
   }
   public void setIsPowered(bool value) {
      isPowered = value;
   }
   //**********************************************/
}

