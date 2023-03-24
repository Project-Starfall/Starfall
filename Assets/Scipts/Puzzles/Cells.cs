using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cells : MonoBehaviour
{
    public List<Cell> cellsList { get; set; } = new List<Cell>();
    [SerializeField]
    public int cellNumber { get; set; }

}
