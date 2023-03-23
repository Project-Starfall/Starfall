using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LevelTwoHandler : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector startTimeline;
    [SerializeField]
    private PlayableDirector endTimeline;

    [SerializeField]
    private PlayerMovement playerMovement;


    public void Awake()
    {
        Application.targetFrameRate = 60;

        // transition background music into level two music
        FindObjectOfType<audioManager>().musicFadeOut("menuMusic");
        FindObjectOfType<audioManager>().musicFadeOut("levelOneMusic");
        FindObjectOfType<audioManager>().musicFadeIn("levelTwoMusic");
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMovement.disableMovement = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement.disableMovement = true;
        endTimeline.Play();
        StartCoroutine(delayClose());

    }

    public void allowMove()
    {
        playerMovement.disableMovement = false;
    }

    private IEnumerator delayClose()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        yield return null;
    }
}
