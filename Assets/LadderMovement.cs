using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    public float LadderMoveSpeed = 6;


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.W))
            other.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, LadderMoveSpeed);
        else if (other.tag == "Player" && Input.GetKey(KeyCode.UpArrow))
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, LadderMoveSpeed);
        else if(other.tag == "Player" && Input.GetKey(KeyCode.S))
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -LadderMoveSpeed);
        else if (other.tag == "Player" && Input.GetKey(KeyCode.DownArrow))
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -LadderMoveSpeed);
        else
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.55f);
    }
}
