using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMovement : MonoBehaviour
{
    private int direction;
    [SerializeField] private bool rotate;
    private float rotationSpeed;
    private float movementSpeed;
    private SpriteRenderer trashSprite;

    // Start is called before the first frame update
    void Start()
    {
        // Set random direction, speed, and start position
        transform.position = new Vector3(Random.Range(-2.9f, 3f), transform.position.y, transform.position.z);
        direction = Random.Range(-100, 100) <= 0 ? -1 : 1;
        movementSpeed = 3f + Random.Range(0f, 2f);
        rotationSpeed = 15;

        // Get the trash's sprite
        trashSprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move trash back and forth
        transform.Translate(new Vector3((direction * movementSpeed) * Time.deltaTime, 0, 0));
        if (transform.position.x <= -2.9)
            direction = 1;
        if (transform.position.x >= 3)
            direction = -1;

        // Rotate trash along its path
        if (rotate)
            trashSprite.transform.Rotate(new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
    }
}
