using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using static Constants.Transitions;
using static Constants.Scenes;
using static TransitionManager;

public class TransitionHandler : MonoBehaviour
{
   [SerializeField] PlayableDirector fadeIn;
   [SerializeField] PlayableDirector fadeOut;
   [SerializeField] PlayableDirector postLvl0;
   [SerializeField] PlayableDirector postLvl1;
   [SerializeField] PlayableDirector postLvl2;
   [SerializeField] PlayableDirector postLvl3;
   [SerializeField] PlayableDirector postLvl4;
   [SerializeField] GameObject star1;
   [SerializeField] GameObject star2;
   [SerializeField] GameObject star3;
   [SerializeField] GameObject star4;
   [SerializeField] GameObject mainStar;


   // Start is called before the first frame update
   void Start()
    {
      setUpScene();
      fadeIn.Play();
    }

   public void setUpScene()
   {
      switch (transition)
      {
         case LV1_LV2:
            mainStar.transform.localPosition = new Vector3(3.16f, -2.67f, 0);
            break;
         case LV2_LV3:
            mainStar.transform.localPosition = new Vector3(5.57f, 1.88f, 0);
            star1.SetActive(false);
            break;
         case LV3_LV4:
            mainStar.transform.localPosition = new Vector3(7.55f, 1.7f, 0);
            star1.SetActive(false);
            star2.SetActive(false);
            break;
         case LV4_LV5:
            mainStar.transform.localPosition = new Vector3(11.27f, -0.55f, 0);
            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);
            break;
      }
   }

   public void runMap()
   {
      switch(transition)
      {
         case LV0_LV1:
            postLvl0.Play();
            break;
         case LV1_LV2:
            postLvl1.Play();
            break;
         case LV2_LV3:
            postLvl2.Play();
            break;
         case LV3_LV4:
            postLvl3.Play();
            break;
         case LV4_LV5:
            postLvl4.Play();
            break;
      }
   }

   public void faded()
   {
      fadeOut.Play();
   }

   public void changeScene()
   {
      switch (transition)
      {
         case LV0_LV1:
            setTransition(LV1_LV2);
            SceneManager.LoadScene(LevelOne, LoadSceneMode.Single);
            break;
         case LV1_LV2:
            setTransition(LV2_LV3);
            star1.SetActive(false);
            SceneManager.LoadScene(LevelTwo, LoadSceneMode.Single);
            break;
         case LV2_LV3:
            setTransition(LV3_LV4);
            star2.SetActive(false);
            SceneManager.LoadScene(LevelThree, LoadSceneMode.Single);
            break;
         case LV3_LV4:
            setTransition(LV4_LV5);
            star3.SetActive(false);
            SceneManager.LoadScene(LevelFour, LoadSceneMode.Single);
            break;
         case LV4_LV5:
            star4.SetActive(false);
            SceneManager.LoadScene(LevelFive, LoadSceneMode.Single);
            setTransition(LV0_LV1);
            break;
      }
   }

}
