using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    // the field for the volume settings
    [SerializeField] public Slider MasterVolumeSlider;
    [SerializeField] public Slider SFXVolumeSlider;
    [SerializeField] public Slider MusicVolumeSlider;

    // audio field
    [SerializeField] public audioManager audioManager;

    public AudioMixer audioMixer;

    public AudioSource musicVolume;

    public bool isMuted = false;

    public void OnMute()
    {
        isMuted = !isMuted;
        musicVolume.mute = isMuted;
    }

    public void Start()
    {
        // Load the current volume, sound volume, and music volume
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f);
        SFXVolumeSlider.value    = PlayerPrefs.GetFloat("SoundVolume", 1.0f);
        MusicVolumeSlider.value  = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        audioManager.musicVolume = MusicVolumeSlider.value;
    }

    public void setMasterVolume(float masterVolume)
    {
        // Set the volume and save it to the PlayerPrefs
        AudioListener.volume = masterVolume;
        audioMixer.SetFloat("Volume", masterVolume);
        PlayerPrefs.SetFloat("Volume", masterVolume);
    }

    public void setSFXVolumeSlider(float masterVolume)
    {
        AudioSource[] sfxSources = GameObject.FindObjectsOfType<AudioSource>();
        foreach(AudioSource sfxSource in sfxSources)
        {
            if(sfxSource.tag == "SFX")
            {
                sfxSource.volume = masterVolume;
            }
        }
        // Set the sound volume and save it to the playerprefs
        PlayerPrefs.SetFloat("SFXVolume", masterVolume);
    }

    public void setMusicVolumeSlider(float masterVolume)
    {
        AudioSource[] musicSources = GameObject.FindObjectsOfType<AudioSource>();
        foreach (AudioSource musicSource in musicSources)
        {
            if (musicSource.tag == "Music")
            {
                musicSource.volume = masterVolume;
            }
        }
        // Set the music volume and save it to the playerprefs
        PlayerPrefs.SetFloat("MusicVolume", masterVolume);
    }

}
