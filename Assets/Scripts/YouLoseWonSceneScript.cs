using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YouLoseWonSceneScript : MonoBehaviour
{
    
    [SerializeField] private GameObject _buttonToFirstSelect; // Button to first select when starting to navigate with keyboard
    [SerializeField] private EventSystem _eventSystem; // The event system to witch attach this script
    [SerializeField] private Text _youLoseWonText; // A text with the written "You won!" or "You lose!"
    [SerializeField] private Text _secondaryText; // A text with the written something as "You've completed the level aaa!"
    [SerializeField] private GameObject _buttonNextLevel; // This button could not be shown in case you lose
    
    private void Start()
    {
        _buttonNextLevel.SetActive(true);
        if (GameManager.Instance.state == GameState.Win)
        {
            _youLoseWonText.text = "You won!";
            _secondaryText.text = "You've completed the level " + GameManager.Instance.GetLevelToPlay() + "!";
        }
        if (GameManager.Instance.LevelToPlayIsTheLastOne())
        {
            _buttonNextLevel.SetActive(false);
        }
        if (GameManager.Instance.state == GameState.Lose)
        {
            _youLoseWonText.text = "You lose!";
            _secondaryText.text = "You've not completed the level " + GameManager.Instance.GetLevelToPlay() + "!";
            _buttonNextLevel.SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
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

    public void RepeatLevel() // Repeat the level
    {
        GameManager.Instance.UpdateGameState(GameState.Playing);
    }
    
    public void NextLevel() // Go to the next level
    {
        GameManager.Instance.NextLevelToPlay();
        GameManager.Instance.UpdateGameState(GameState.Playing);
    }
    
    public void GoToMenu() // Return to main menu
    {
        GameManager.Instance.UpdateGameState(GameState.SelectionLevel);
    }
    
}
