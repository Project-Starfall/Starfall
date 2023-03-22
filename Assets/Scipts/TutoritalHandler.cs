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
   [SerializeField] Rigidbody2D playerRigidBody;
   [SerializeField] PlayableDirector endTransition;

   [SerializeField] BoxCollider2D blocker;

   [SerializeField] PolygonCollider2D cameraConfinerNew;
   [SerializeField] CinemachineConfiner2D cameraConfiner;

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;

        // transition background music into tutorial music
        FindObjectOfType<audioManager>().musicFadeOut("menuMusic");
        FindObjectOfType<audioManager>().musicFadeIn("levelTutorialMusic");
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
      yield return new WaitForSeconds(1);
      SceneManager.LoadScene(LevelOne, LoadSceneMode.Single);
   }
}
