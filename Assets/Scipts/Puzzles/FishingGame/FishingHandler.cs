using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingHandler : MonoBehaviour
{
    [SerializeField] GameObject Fishing;

    // Start is called before the first frame update
    void Start()
    {
        Fishing.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
