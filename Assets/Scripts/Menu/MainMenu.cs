using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // This script is to manage the main menu

    [SerializeField] private GameObject _buttonToFirstSelect; // Button to first select when starting to navigate with keyboard
    [SerializeField] private EventSystem _eventSystem; // The event system to witch attach this script
    [SerializeField] private ScriptForTransitionsBetweenMenuScenes _scriptForTransitionsBetweenMenuScenes; // A script to load the next scene with a transition

    void Update()
    {
        // Select an element when starting to navigate with keyboard
        if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
        {
            if (!_eventSystem.currentSelectedGameObject)
            {
                _eventSystem.SetSelectedGameObject(_buttonToFirstSelect);
            }
        }
        // Click on Esc key to quit the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
    
    public void GoToLevelSelectionMenu() // Go to level selection menu
    {
        AudioManager.instance.PlayClickMenuSFX();
        _scriptForTransitionsBetweenMenuScenes.AnimationEndTransitionBetweenMenuScenes("LevelSelectionMenu");
    }

    public void GoToSettings() // Go to settings menu
    {
        AudioManager.instance.PlayClickMenuSFX();
        _scriptForTransitionsBetweenMenuScenes.AnimationEndTransitionBetweenMenuScenes("SettingMenu");
    }
    
    public void GoToSendFeedback() // Go to send feedback menu
    {
        AudioManager.instance.PlayClickMenuSFX();
        _scriptForTransitionsBetweenMenuScenes.AnimationEndTransitionBetweenMenuScenes("SendFeedbackMenu");
    }

    public void QuitGame() // Exit from the game
    {
        AudioManager.instance.PlayClickMenuSFX();
        Debug.Log("QUIT");
        Application.Quit();
    }
    
}