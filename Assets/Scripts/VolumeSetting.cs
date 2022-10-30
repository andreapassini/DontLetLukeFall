using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider _backgroundMusicSlider;
    [SerializeField] private Slider _soundEffectsSlider;

    public const string MIXER_BACKGROUNDMUSIC = "BackgroundMusicVolume";
    public const string MIXER_SOUNDEFFECTS = "SoundEffectsVolume";
    
    private void Awake()
    {
        _backgroundMusicSlider.onValueChanged.AddListener(setBackgroundMusicVolume);
        _soundEffectsSlider.onValueChanged.AddListener(setSoundEffectsVolume);
    }

    private void Start()
    {
        _backgroundMusicSlider.value = PlayerPrefs.GetFloat(AudioManager.BACKGROUNDMUSIC_KEY, 1f);
        _soundEffectsSlider.value = PlayerPrefs.GetFloat(AudioManager.SOUNDEFFECTS_KEY, 1f);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.BACKGROUNDMUSIC_KEY, _backgroundMusicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SOUNDEFFECTS_KEY, _soundEffectsSlider.value);
    }

    private void setBackgroundMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_BACKGROUNDMUSIC, Mathf.Log10(value) * 20.0f);
    }
    
    private void setSoundEffectsVolume(float value)
    {
        mixer.SetFloat(MIXER_SOUNDEFFECTS, Mathf.Log10(value) * 20.0f);
    }
    
}
