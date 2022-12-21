using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour // This script is to manage the volume settings
{

    [SerializeField] private AudioMixer mixer; // The audio mixer

    // Names of exposed var of audio mixer groups
    public const string MIXER_BACKGROUNDMUSIC = "BackgroundMusicVolume";
    public const string MIXER_SOUNDEFFECTS = "SoundEffectsVolume";

    public float GetBackgroundMusicVolume() // Method to get the background music volume from user pref
    {
		float defaultParameter;
        AudioManager.instance.GetComponent<AudioMixer>().GetFloat("BackgroundMusic", out defaultParameter);

        return PlayerPrefs.GetFloat(AudioManager.BACKGROUNDMUSIC_KEY, defaultParameter);
    }
    
    public float GetSoundEffectsVolume() // Method to get the sound effects volume from user pref
    {
        float defaultParameter;
        AudioManager.instance.GetComponent<AudioMixer>().GetFloat("SoundEffects", out defaultParameter);

        return PlayerPrefs.GetFloat(AudioManager.SOUNDEFFECTS_KEY, defaultParameter);
    }

    public void OnDisableSaveLastSettings(float backgroundMusicValue, float soundEffectsValue) // Method to save settings of the volumes of background music and sound effects
    {
        PlayerPrefs.SetFloat(AudioManager.BACKGROUNDMUSIC_KEY, backgroundMusicValue);
        PlayerPrefs.SetFloat(AudioManager.SOUNDEFFECTS_KEY, soundEffectsValue);
    }

    public void SetBackgroundMusicVolume(float value) // Method to change background music volume
    {
        mixer.SetFloat(MIXER_BACKGROUNDMUSIC, Mathf.Log10(value) * 20.0f);
    }
    
    public void SetSoundEffectsVolume(float value) // Method to change sound effects volume
    {
        mixer.SetFloat(MIXER_SOUNDEFFECTS, Mathf.Log10(value) * 20.0f);
    }
    
}
