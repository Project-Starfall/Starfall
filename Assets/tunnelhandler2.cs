using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class tunnelTwo : MonoBehaviour
{
    [SerializeField] PlayableDirector tunnelHandler2;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButtonDown("Action"))
        {
            tunnelHandler2.Play();
        }
    }
}
