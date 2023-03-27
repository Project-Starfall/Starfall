using UnityEngine;

public class FishMovement : MonoBehaviour
{
   private int direction;
   private float speed;
   private SpriteRenderer FishSprite;

   // Start is called before the first frame update
   void Start()
   {
      // Set random direction, speed, and start position
      transform.position = new Vector3(Random.Range(-2.9f, 3f), transform.position.y, transform.position.z);
      direction = Random.Range(-100, 100) <= 0 ? -1 : 1;
      speed = 3f;

      // Get the fish's sprite
      FishSprite = GetComponentInChildren<SpriteRenderer>();
      FishSprite.flipX = direction == -1 ? false : true;
   }



   // Update is called once per frame
   void Update()
   {
      // Move fish back and forth, flipping its sprite to match its movements
      transform.Translate(new Vector3((direction * speed) * Time.deltaTime, 0, 0));
      if (transform.position.x <= -2.9)
      {
         direction = 1;
         FishSprite.flipX = true;
      }
      if (transform.position.x >= 3)
      {
         direction = -1;
         FishSprite.flipX = false;
      }
   }
}
