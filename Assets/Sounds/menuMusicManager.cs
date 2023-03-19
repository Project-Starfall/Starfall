using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuMusicManager : MonoBehaviour
{
    void Awake()
    {
        // fade in menu background track
        FindObjectOfType<audioManager>().musicFadeOut("levelTutorialMusic");
        FindObjectOfType<audioManager>().musicFadeOut("levelOneMusic");
        FindObjectOfType<audioManager>().musicFadeOut("levelTwoMusic");
        FindObjectOfType<audioManager>().musicFadeOut("levelThreeMusic");
        FindObjectOfType<audioManager>().musicFadeOut("levelFourMusic");
        FindObjectOfType<audioManager>().musicFadeOut("levelFiveMusic");
        FindObjectOfType<audioManager>().musicFadeIn("menuMusic");
    }
}
