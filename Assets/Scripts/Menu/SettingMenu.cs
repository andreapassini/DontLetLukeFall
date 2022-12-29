using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    // This script is to manage the setting menu

    [SerializeField] private GameObject _buttonToFirstSelect;
    [SerializeField] private EventSystem _eventSystem;

    [SerializeField] private Image _fillBackgroundMusicSlider;
    [SerializeField] private Image _fillSoundEffectsSlider;
    private Color _oldColor;
    private Color _selectionSliderColor = Color.green;
    private const float _maxSliderValue = 0.971f;

    [SerializeField] private VolumeSetting _volumeSetting; // The volume setting script
    // The two sliders to set background music volume and sound effects volume
    [SerializeField] private Slider _backgroundMusicSlider;
    [SerializeField] private Slider _soundEffectsSlider;
    
    [SerializeField] private ScriptForTransitionsBetweenMenuScenes _scriptForTransitionsBetweenMenuScenes; // A script to load the next scene with a transition

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
    
    void Update()
    {
        // Fix maximum slider values
        if (_backgroundMusicSlider.value >= _maxSliderValue)
        {
            _backgroundMusicSlider.value = _maxSliderValue;
        }
        if (_soundEffectsSlider.value >= _maxSliderValue)
        {
            _soundEffectsSlider.value = _maxSliderValue;
        }
        // This part of the script is to change the color of the sliders when navigating on them
        if (_eventSystem.currentSelectedGameObject != null)
        {
            if (_eventSystem.currentSelectedGameObject.name == _backgroundMusicSlider.name && _fillBackgroundMusicSlider.color != _selectionSliderColor)
            {
                _oldColor = _fillBackgroundMusicSlider.color;
                _fillBackgroundMusicSlider.color = _selectionSliderColor;
            } else if (_eventSystem.currentSelectedGameObject.name != _backgroundMusicSlider.name && _fillBackgroundMusicSlider.color == _selectionSliderColor)
            {
                _fillBackgroundMusicSlider.color = _oldColor;
            }
            if (_eventSystem.currentSelectedGameObject.name == _soundEffectsSlider.name && _fillSoundEffectsSlider.color != _selectionSliderColor)
            {
                _oldColor = _fillSoundEffectsSlider.color;
                _fillSoundEffectsSlider.color = _selectionSliderColor;
            } else if (_eventSystem.currentSelectedGameObject.name != _soundEffectsSlider.name && _fillSoundEffectsSlider.color == _selectionSliderColor)
            {
                _fillSoundEffectsSlider.color = _oldColor;
            }
        }
        // Select an element when starting to navigate with keyboard
        if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
        {
            if (!_eventSystem.currentSelectedGameObject)
            {
                _eventSystem.SetSelectedGameObject(_buttonToFirstSelect);
            }
        }
        // Click on Esc key to go back to main menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMainMenu();
        }
    }
    
    private void OnDisable() // On disable save volume settings
    {
        _volumeSetting.OnDisableSaveLastSettings(_backgroundMusicSlider.value, _soundEffectsSlider.value);
    }

    public void GoToMainMenu() // Go to main menu
    {
        _scriptForTransitionsBetweenMenuScenes.AnimationEndTransitionBetweenMenuScenes("MainMenu");
    }
    
    public void GoToCredits() // Go to credits
    {
        _scriptForTransitionsBetweenMenuScenes.AnimationEndTransitionBetweenMenuScenes("Credits");
    }

}
