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

    private void Start()
    {
        // Start with the background music
        AudioManager.instance.Play("luke_intro");
    }

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
    }
    
    public void GoToLevelSelectionMenu() // Go to level selection menu
    {
        SceneManager.LoadScene("LevelSelectionMenu");
    }

    public void GoToSettings() // Go to settings menu
    {
        SceneManager.LoadScene("SettingMenu");
    }
    
    public void GoToSendFeedback() // Go to send feedback menu
    {
        SceneManager.LoadScene("SendFeedbackMenu");
    }

    public void QuitGame() // Exit from the game
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
    
}
