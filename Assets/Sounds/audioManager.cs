using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class audioManager : MonoBehaviour
{
    public sound[] sounds;

    public static audioManager instance;

    // Control how fast the tracks cross-fade
    [SerializeField]
    [Range(1.0f, 10.0f)]
    private float fadeTime;

    // Control volume of all music
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float musicVolume;

    /*sound menuMusic,
          levelTutorialMusic,
          levelOneMusic,
          levelTwoMusic,
          levelThreeMusic,
          levelFourMusic,
          levelFiveMusic,
          endingMusic;*/

    // 
    void Awake()
    {
        // if an audio manager already exists dont create a new one
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // keep audio manger alive between scenes
        DontDestroyOnLoad(gameObject);
        
        // create an audio source for every audio clip
        foreach(sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip   = s.clip;

            s.source.volume = s.volume;
            s.source.pitch  = s.pitch;
            s.source.loop   = s.loop;
        }
        //populateMusicSources();
    }
    
    // Upon game launch play menu music
    void Start()
    {
        play("menuMusic");
        play("levelTutorialMusic");
        play("levelOneMusic");
        play("levelTwoMusic");
        play("levelThreeMusic");
        play("levelFourMusic");
        play("levelFiveMusic");
        play("endingMusic");
    }

    private void populateMusicSources()
    {
        //menuMusic = Array.Find(sounds, sound => sound.name == "menuMusic");
        //play("levelTutorialMusic");
        //levelTutorialMusic = Array.Find(sounds, sound => sound.name == "levelTutorialMusic");
        //play("levelOneMusic");
        //levelOneMusic = Array.Find(sounds, sound => sound.name == "levelOneMusic");
        //play("levelTwoMusic");
        //levelTwoMusic = Array.Find(sounds, sound => sound.name == "levelTwoMusic");
        //play("levelThreeMusic");
        //levelThreeMusic = Array.Find(sounds, sound => sound.name == "levelThreeMusic");
        //play("levelFourMusic");
        //levelFourMusic = Array.Find(sounds, sound => sound.name == "levelFourMusic");
        //play("levelFiveMusic");
        //levelFiveMusic = Array.Find(sounds, sound => sound.name == "levelFiveMusic");
        //play("endingMusic");
        //endingMusic = Array.Find(sounds, sound => sound.name == "endingMusic");
    }

    // finds and plays audio clip of specified name
    public void play(string name)
    {
        // find audio clip of said name within sounds file
        sound s = Array.Find(sounds, sound => sound.name == name);

        // if audio file name is not recognized dont play
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }

        // play audio clip of said name
        s.source.Play();
    }

    // fade into tutorialMusic
    public void musicFadeIn(string musicname)
    {
        sound musicClip = Array.Find(sounds, sound => sound.name == musicname);
        if (musicClip == null)
        {
            Debug.LogWarning("music name not recognized");
            return;
        }
        while (musicClip.source.volume < musicVolume)
        {
            musicClip.source.volume += 0.01f / fadeTime;
        }

        /*while (levelTutorialMusic.source.volume < musicVolume)
        {
            levelTutorialMusic.source.volume += 0.01f / fadeTime;
        }*/
    }

    // fade out tutorialMusic
    public void musicFadeOut(string musicname)
    {
        sound musicClip = Array.Find(sounds, sound => sound.name == musicname);
        if (musicClip == null)
        {
            Debug.LogWarning("music name not recognized");
            return;
        }
        while (musicClip.source.volume > 0.0f)
        {
            musicClip.source.volume -= 0.01f / fadeTime;
        }
        musicClip.source.Stop();

        /*while (levelTutorialMusic.source.volume > 0.0f)
        {
            levelTutorialMusic.source.volume -= 0.01f / fadeTime;
        }
        levelTutorialMusic.source.Stop();*/
    }
    
    // FindObjectOfType<audioManager>().play("dashSound"); example of calling in other scripts
}