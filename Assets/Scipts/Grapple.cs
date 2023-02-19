using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour, Grappleable
{
    [SerializeField] public Transform[] controlPoints { get; set; }

    private Vector2 gizmosPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

   // Is ran when the grapple is first detected
   public void onEnter()
   {

   }
   
   // Is ran when the grapple is no longer detected
   public void onLeave()
   {

   }
      
   // Called when the player presses the interact key
   public void grapple()
   {

   }

    private void OnDrawGizmos()
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}