using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class tunnelOne : MonoBehaviour
{
    [SerializeField] PlayableDirector tunnelHandler1;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetButtonDown("Action"))
        {
            tunnelHandler1.Play();
        }
    }
}
