using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class tunnelTwo : MonoBehaviour
{
   [SerializeField] PlayableDirector tunnelHandler2;
   [SerializeField] CinemachineConfiner2D cameraConfiner;
   [SerializeField] PolygonCollider2D tunnelConfiner;

   private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButtonDown("Action"))
        {
            tunnelHandler2.Play();
            StartCoroutine(switchCamera());
            
        }
    }

   private IEnumerator switchCamera()
   {
      yield return new WaitForSeconds(0.35f);
      cameraConfiner.m_BoundingShape2D = tunnelConfiner;
      yield return null;
   }
}
