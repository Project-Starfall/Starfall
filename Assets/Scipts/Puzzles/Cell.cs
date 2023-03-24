using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Transform cellTransform { get; set; }
    public ComponentPuzzlePart component { get; set; }

    public void Start()
    {
        cellTransform = transform;
    }

}
