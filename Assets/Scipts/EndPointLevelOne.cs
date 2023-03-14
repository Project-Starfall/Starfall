using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using static Constants.Scenes;

public class EndPointLevelOne : MonoBehaviour
{
   [SerializeField] PlayableDirector timeline;
   public void OnTriggerEnter2D(Collider2D collision)
   {
      timeline.Play();
      StartCoroutine(delaySceneClose());
   }

   public IEnumerator delaySceneClose()
   {
      yield return new WaitForSeconds(1);
      SceneManager.LoadScene(LevelTwo, LoadSceneMode.Single);
      yield return null;
   }
}
