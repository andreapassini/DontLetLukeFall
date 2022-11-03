using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSliderChangingVolumeScript : MonoBehaviour
// This is a gameObject I used only to test the changing volume with sliders
{

    [SerializeField] private VolumeSetting _volumeSetting; // The volume setting script
    // The two sliders to set background music volume and sound effects volume
    [SerializeField] private Slider _backgroundMusicSlider;
    [SerializeField] private Slider _soundEffectsSlider;
    
    private void Awake() // At the beginning we add listeners on the values of the sliders to change the volume
    {
        _backgroundMusicSlider.onValueChanged.AddListener(_volumeSetting.SetBackgroundMusicVolume);
        _soundEffectsSlider.onValueChanged.AddListener(_volumeSetting.SetSoundEffectsVolume);
    }
    
    private void Start() // Setting the initials slider's values
    {
        _backgroundMusicSlider.value = _volumeSetting.GetBackgroundMusicVolume();
        _soundEffectsSlider.value = _volumeSetting.GetSoundEffectsVolume();
    }
    
    private void OnDisable() // On disable save volume settings
    {
        _volumeSetting.OnDisableSaveLastSettings(_backgroundMusicSlider.value, _soundEffectsSlider.value);
    }
    
}
