using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    private bool moving;
    private float startPosX;
    private float startPosY;
    private Transform componentTransform;
    private Vector3 resetPosition;

    // Start is called before the first frame update
    void Start()
    {
        componentTransform = transform;
        transform.localPosition = resetPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            componentTransform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, componentTransform.localPosition.z);
        }
    }

    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - transform.localPosition.x;
            startPosY = mousePos.y - transform.localPosition.y;

            moving = true;
        }
    }

    private void OnMouseUp()
    {
/*        moving = false;
        if() // if its in a slot
        {

        }
        else // Not in a slot or appropriate resting position
        {
            componentTransform.position = resetPosition;
        }*/
    }
}
