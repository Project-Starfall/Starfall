using UnityEngine.Audio;
using System;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public sound[] sounds;

    public static audioManager instance;

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
    }

    //
    void Start()
    {
        play("themeMusic");
    }

    //
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
    //FindObjectOfType<audioManager>().play("dashSound"); bracheys ways
    // audioManager.instance.play("dashSound");  syntax for movement script
}
