using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false; // A static variable containing if the game is paused or not
    
    [SerializeField] private GameObject _pauseMenuUI; // The part of the ui related to the pause menu
    [SerializeField] private GameObject _pauseButton; // A button to pause the game
    [SerializeField] private EventSystem _eventSystem; // The event system to witch attach this script
    [SerializeField] private GameObject _platformBar; // Avoid to show action bar while game is paused
    [SerializeField] private GameObject _buttonToFirstSelect; // Button to first select when starting to navigate with keyboard

    private float _oldTimeScale = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Use escape kay to pause or resume the game
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (gameIsPaused)
        {
            // Select an element when starting to navigate with keyboard
            if (!Input.GetKeyDown(KeyCode.Escape) && Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
            {
                if (!_eventSystem.currentSelectedGameObject)
                {
                    _eventSystem.SetSelectedGameObject(_buttonToFirstSelect);
                }
            }
        }
    }

    public void Resume() // Resume the game when clicking on escape button or clicking on the resume button
    {
        if (!gameIsPaused)
        {
            return;
        }
        _eventSystem.SetSelectedGameObject(null);
        _pauseMenuUI.SetActive(false); // close pause menu
        _pauseButton.SetActive(true);
        if (_platformBar != null)
        {
            _platformBar.SetActive(true);
        }
        //Time.timeScale = _oldTimeScale; // restart time
        if (AudioManager.instance != null)
        {
            //AudioManager.instance.UnPauseAllBackgroundMusics(); // Background music is resumed
        }
        gameIsPaused = false;
    }

    public void Pause() // Resume the game when clicking on escape button or clicking on the pause button
    {
        if (gameIsPaused)
        {
            return;
        }
        _pauseMenuUI.SetActive(true); // open pause menu
        _pauseButton.SetActive(false);
        if (_platformBar != null)
        {
            _platformBar.SetActive(false);
        }
        if (Time.timeScale != 0f)
        {
            _oldTimeScale = Time.timeScale;
        }
        //Time.timeScale = 0f; // Stop time
        if (AudioManager.instance != null)
        {
            //AudioManager.instance.PauseAllBackgroundMusics(); // Background music is paused
        }
        gameIsPaused = true;
    }

    public void RestartLevel() // restart the current level
    {
        Resume();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateGameState(GameState.Playing);
        }
    }
    
    public void QuitLevel() // Quit to main menu
    {
        Resume();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateGameState(GameState.SelectionLevel);
        }
    }
    
}
