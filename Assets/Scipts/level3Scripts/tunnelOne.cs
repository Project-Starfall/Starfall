using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class tunnelOne : MonoBehaviour
{
    [SerializeField] PlayableDirector tunnelHanddler1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(Input.GetButtonDown("Action"))
        {
            tunnelHanddler1.Play();
        }
    }
}
