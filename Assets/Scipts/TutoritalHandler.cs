using Cinemachine;
using System.Collections;

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using static Constants.Scenes;
using static SaveSystem;

public class TutoritalHandler : MonoBehaviour
{
   [SerializeField] Transform playerTransform;
   [SerializeField] Player player;
    [SerializeField] PlayerMovement playerMovement;
   [SerializeField] Rigidbody2D playerRigidBody;
   [SerializeField] PlayableDirector endTransition;

   [SerializeField] BoxCollider2D blocker;

   [SerializeField] PolygonCollider2D cameraConfinerNew;
   [SerializeField] CinemachineConfiner2D cameraConfiner;

    // check if the puzzel is already complete
    private bool puzzleCompleted = false;

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;

        // transition background music into tutorial music
        FindObjectOfType<audioManager>().musicFadeOut("menuMusic");
        FindObjectOfType<audioManager>().musicFadeIn("levelTutorialMusic");

        if(saveExist())
        {
            PlayerData data = LoadGame();
        }
    }

   private void OnTriggerEnter2D(Collider2D collision)
   {
      // Lock Player Movement
      endTransition.Play();
      StartCoroutine(EndSequence());
   }

   public void pickupMap()
   {
      SaveGame(player); // Saving the player data
      Debug.Log("Saving the Player...");
      LoadGame();
      Debug.Log("Loading the game...");
      cameraConfiner.m_BoundingShape2D = cameraConfinerNew;
      blocker.enabled = false;
   }

   public void resetPlayerLock()
   {
      playerTransform.localRotation = Quaternion.identity;
      playerRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
      Debug.Log("RestLock");
   }

   public IEnumerator EndSequence()
   {      
      SaveGame(player); // Saving the player data
      Debug.Log("Saving the Player...");
      LoadGame();
      Debug.Log("Loading the game...");
      playerMovement.DisableMovement();
      Debug.Log("Saving the Player...");
      yield return new WaitForSeconds(1);
      

        if(puzzleCompleted)
        {
            PlayerData data = LoadGame();
            playerTransform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        }
        else
        {
            SceneManager.LoadScene(Transistion, LoadSceneMode.Single);
        }
   }
}
