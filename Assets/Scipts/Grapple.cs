using Cinemachine;
using UnityEngine;

public class Grapple : MonoBehaviour, Grappleable
{
    [SerializeField] private Transform[] controlPoints;

    private Vector2 gizmosPosition;
   [SerializeField] float startSpeed;
   [SerializeField] float endSpeed;

   // Anim control
   [SerializeField]
   private CinemachineVirtualCamera vcam;
   [SerializeField]
   private CinemachineFramingTransposer vcam_offset;
   [SerializeField]
   private bool cameraMoves;
   [SerializeField]
   private Transform playerPos;
   [SerializeField]
   private Material glowMaterial;
   private bool isFade = false;
   private bool fadeIn = false;
   private float fade = 1.0f;

   private bool isCameraMove = false;
   private bool moveToPoint = false;
   private float xPos = 0.0f;
   private float xTarget;
   public float camSpeed = 5f;

   public void Start()
   {
      vcam_offset = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
      if(vcam_offset == null)
      {
         Debug.Log("Could not get composer");
      }
   }

   public void Update()
   {
      if (isFade)
      {
         if (!fadeIn)
         {
            fade -= Time.deltaTime;
            if (fade <= 0f)
            {
               fade = 0f;
               isFade = false;
            }
         }
         else
         {
            fade += Time.deltaTime;
            if (fade >= 1f)
            {
               fade = 1f;
               isFade = false;
            }
         }
         glowMaterial.SetFloat("_Fade", fade);
      }

      if(isCameraMove)
      {
         if(moveToPoint) {
            xPos += Time.deltaTime * camSpeed;
            Debug.Log($"initial:{xPos}");
            if (xPos >= xTarget)
            {
               xPos = xTarget;
               isCameraMove = false;
            }
         }
         else
         {
            xPos -= Time.deltaTime * camSpeed;
            if(xPos <= 0f)
            {
               xPos = 0f;
               isCameraMove = false;
            }
         }
         Debug.Log($"to:{xTarget}");
         Debug.Log($"latter:{xPos}");
         vcam_offset.m_TrackedObjectOffset = new Vector3(xPos, 0.0f, 0.0f);
      }
   }

   public void OnTriggerEnter2D(Collider2D collision)
   {
      Debug.Log("Entered Collider");
      onEnter();
   }

   public void OnTriggerExit2D(Collider2D collision)
   {
      Debug.Log("Exited Collider");
      onLeave();
   }

   // Is ran when the grapple is first detected
   public void onEnter()
    {
      isFade= true;
      fadeIn= true;

      if(cameraMoves)
      {
         xTarget = (controlPoints[4].position.x - playerPos.position.x) * 0.5f;
         moveToPoint = true;
         isCameraMove = true;
      }
    }
   
    // Is ran when the grapple is no longer detected
    public void onLeave()
    {
      isFade = true;
      fadeIn = false;

      if (cameraMoves)
      {
         moveToPoint = false;
         xTarget = 0.0f;
         isCameraMove = true;
      }
   }

    // Called when the player presses the interact key
    public (Transform t1, Transform t2, Transform t3, Transform t4, Transform t5) returnGrapple()
    {
        Transform t1 = controlPoints[0];
        Transform t2 = controlPoints[1];
        Transform t3 = controlPoints[2];
        Transform t4 = controlPoints[3];
        Transform t5 = controlPoints[4];
        return (t1, t2, t3, t4, t5);
    }

   public (float startSpeed, float endSpeed) returnSpeeds()
   {
      return (startSpeed, endSpeed);
   }

  /*private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.25f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
                Mathf.Pow(t, 3) * controlPoints[3].position;

            Gizmos.DrawSphere(gizmosPosition, 0.25f);
        }

        Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y),
            new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));

        Gizmos.DrawLine(new Vector2(controlPoints[2].position.x, controlPoints[2].position.y),
            new Vector2(controlPoints[3].position.x, controlPoints[3].position.y));
    }*/

}
