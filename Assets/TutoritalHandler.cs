using System.Collections;
using System.Collections.Generic;
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

   public void resetPlayerLock()
   {
      playerTransform.localRotation = Quaternion.identity;
      playerRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
      Debug.Log("RestLock");
   }

   public IEnumerator EndSequence()
   {
      yield return new WaitForSeconds(3);
      SceneManager.LoadScene(LevelOne);
   }
}
