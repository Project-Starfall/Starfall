using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class sound
{
    public string name;

    public AudioClip clip;
    
    // audio clips individual customizations
    [Range(0.0f, 1.0f)]
    public float volume;
    [Range(0.1f, 3.0f)]
    public float pitch;
    public bool  loop;

    [HideInInspector]
    public AudioSource source;
}
