using Cinemachine;
using System.Collections;

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using static Constants.Scenes;

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
    }

   private void OnTriggerEnter2D(Collider2D collision)
   {
      // Lock Player Movement
      endTransition.Play();
      StartCoroutine(EndSequence());
   }

   public void pickupMap()
   {
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
      yield return new WaitForSeconds(1);
      SceneManager.LoadScene(LevelOne, LoadSceneMode.Single);
   }
}
