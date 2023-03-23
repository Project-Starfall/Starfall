using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Lv2TrappedAnimScript : MonoBehaviour
{
	// [SerializeField] GameObject pipecanvas
    [SerializeField] PlayableDirector TrappedAnimation;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
    public void OnTriggerEnter2D(Collider2D collision)
    {
 	   TrappedAnimation.Play();
 	   return;
    }
}
