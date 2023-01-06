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
    [SerializeField] private Text _secondaryText; // A text with the written something as "You've completed the level aaa!"
    [SerializeField] private GameObject _buttonNextLevel; // This button could not be shown in case you lose
    [SerializeField] private Animator _animatorImageEmojy; // The animator that controls the image emojy to show an image of Luke that you won or lose
    [SerializeField] private GameObject _imageArrowNextLevel1; // 1 of the 2 images to decorate the button next level
    [SerializeField] private GameObject _imageArrowNextLevel2; 
    // 1 of the 2 images to decorate the button next level

    [SerializeField] private GameObject _loseEnemyBackground;
    [SerializeField] private GameObject _winEnemyBackground;
    [SerializeField] private Camera _camera;
    
    [SerializeField] private GameObject _loaderCanvas; // The loading screen
    [SerializeField] private Image _progressBar; // The progress bar in the loading screen

    private Color _winBackground = new Color(0.64f, 0.64f, 0.64f);
    private Color _loseBackground = new Color(0.32f, 0.32f, 0.32f);

    private void Start()
    {
        PauseMenu.gameIsPaused = false;
        _buttonNextLevel.SetActive(true);
        if (GameManager.Instance.state == GameState.Win)
        {
            _winEnemyBackground.SetActive(true);
            _camera.backgroundColor = _winBackground;
            int numberLevelToShow = GameManager.Instance.GetLevelToPlay() - 1;
            string numberLevelToShowString = "";
            if (numberLevelToShow == 0)
            {
                numberLevelToShowString = "tutorial";
            }
            else
            {
                numberLevelToShowString = "Level " + numberLevelToShow;
            }
            _secondaryText.text = numberLevelToShowString + " completed!";
        }
        if (GameManager.Instance.LevelToPlayIsTheLastOne())
        {
            _buttonNextLevel.SetActive(false);
            _imageArrowNextLevel1.SetActive(false);
            _imageArrowNextLevel2.SetActive(false);
        }
        if (GameManager.Instance.state == GameState.Lose)
        {
            _camera.backgroundColor = _loseBackground;
            _loseEnemyBackground.SetActive(true);
            _animatorImageEmojy.SetTrigger("showInsteadAnimationYouLose");
            _imageArrowNextLevel1.SetActive(false);
            _imageArrowNextLevel2.SetActive(false);
            int numberLevelToShow = GameManager.Instance.GetLevelToPlay() - 1;
            string numberLevelToShowString = "";
            if (numberLevelToShow == 0)
            {
                numberLevelToShowString = "tutorial";
            }
            else
            {
                numberLevelToShowString = "Level " + numberLevelToShow;
            }
            _secondaryText.text = numberLevelToShowString + " failed!";
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
        GameManager.Instance.SetElementsForLoadingScreen(_loaderCanvas, _progressBar);
        GameManager.Instance.UpdateGameState(GameState.Playing);
    }
    
    public void GoToMenu() // Return to main menu
    {
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }
    
}
