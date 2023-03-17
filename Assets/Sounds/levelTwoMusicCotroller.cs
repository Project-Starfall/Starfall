using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelTwoMusicCotroller : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        // fade in level two background track
        FindObjectOfType<audioManager>().musicFadeOut("menuMusic");
        FindObjectOfType<audioManager>().musicFadeIn("levelTwoMusic");
    }

}
