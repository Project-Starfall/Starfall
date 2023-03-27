using UnityEngine;

public class Cell : MonoBehaviour
{
    public Transform cellTransform { get; set; } // Where the cell is, used for the dragndrop to place the component on this cell
    public DragNDrop component { get; set; } // The component currently in the cell

    public void Start()
    {
        cellTransform = transform; // transform calls GetComponentTransform which is a taxing call and shouldnt be called over and over
    }
}
