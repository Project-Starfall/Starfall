using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using static Constants.Scenes;

public class LaunchButton : MonoBehaviour
{
   [SerializeField] PlayableDirector endtimeline;

   public void OnMouseDown()
   {
      endtimeline.Play();
      FindObjectOfType<audioManager>().musicFadeOut("levelFiveMusic");
      StartCoroutine(waitForASec());
   }

   private IEnumerator waitForASec()
   {
      yield return new WaitForSeconds(1);

      SceneManager.LoadScene(FinalCutscene, LoadSceneMode.Single);
      
      yield return null;
   }
}
