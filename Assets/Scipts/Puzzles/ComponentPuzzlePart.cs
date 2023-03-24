using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPuzzlePart : MonoBehaviour
{
    int value;
    int operation;

    public ComponentPuzzlePart(int value, int operation = 0)
    {
        this.value = value;
        this.operation = operation;
    }

    public void startPuzzle()
    {

    }
}
