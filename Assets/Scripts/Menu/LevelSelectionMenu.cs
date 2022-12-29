using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DLLF;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionMenu : MonoBehaviour
{
    // This script is to manage the level selection menu

    [SerializeField] private LevelsInfo _levelsInfo;
    [SerializeField] private GameObject _buttonToFirstSelect; // Button to first select when starting to navigate with keyboard
    [SerializeField] private EventSystem _eventSystem; // The event system to witch attach this script
    [SerializeField] private GameObject _doubleArrowLeftButton; // The button to scroll left between levels
    [SerializeField] private GameObject _doubleArrowRightButton; // The button to scroll right between levels
    [SerializeField] private GameObject _firstImageLevelButton; // The first selectable image of the three
    [SerializeField] private GameObject _lastImageLevelButton; // The last selectable image of the three
    [SerializeField] private Image[] _imagesLevels; // The three images of the three levels
    
    [SerializeField] private GameObject _loaderCanvas; // The loading screen
    [SerializeField] private Image _progressBar; // The progress bar in the loading screen

    private int _firstOfTheThreeLevelsToShow = 0; // The index of the first of the three levels to show

    private void LoadTheThreeLevelsToShow() // Function to load the three levels to show
    {
        if (_firstOfTheThreeLevelsToShow < 0) // check on the var _firstOfTheThreeLevelsToShow
        {
            _firstOfTheThreeLevelsToShow = 0;
        }
        int numberOfLevelsToShow = _imagesLevels.Length;
        int firstOfTheThreeLevelsToNotShow = _firstOfTheThreeLevelsToShow + numberOfLevelsToShow;
        int totalNumberOfLevels = _levelsInfo.levelInfos.Length;
        if (firstOfTheThreeLevelsToNotShow > totalNumberOfLevels && _firstOfTheThreeLevelsToShow > 0)
        // Check on the upper bound of var _firstOfTheThreeLevelsToShow
        {
            _firstOfTheThreeLevelsToShow = _firstOfTheThreeLevelsToShow - 1;
            LoadTheThreeLevelsToShow();
            return;
        }
        // Set active / not active double arrow buttons
        _doubleArrowLeftButton.SetActive(true);
        if (_firstOfTheThreeLevelsToShow == 0)
        {
            if (_eventSystem.currentSelectedGameObject == _doubleArrowLeftButton)
            {
                _eventSystem.SetSelectedGameObject(_firstImageLevelButton);
            }
            _doubleArrowLeftButton.SetActive(false);
        }
        _doubleArrowRightButton.SetActive(true);
        bool activateDoubleArrowRightButton = false;
        if (firstOfTheThreeLevelsToNotShow < totalNumberOfLevels)
        {
            activateDoubleArrowRightButton = true;
        }
        if (activateDoubleArrowRightButton == false)
        {
            if (_eventSystem.currentSelectedGameObject == _doubleArrowRightButton)
            {
                _eventSystem.SetSelectedGameObject(_lastImageLevelButton);
            }
            _doubleArrowRightButton.SetActive(false);
        }
        // Load images of levels
        for (int i = 0; i < _imagesLevels.Length; i++)
        {
            _imagesLevels[i].enabled = true;
            if (_firstOfTheThreeLevelsToShow + i >= totalNumberOfLevels)
            {
                _imagesLevels[i].enabled = false;
            }
            else
            {
                _imagesLevels[i].sprite = _levelsInfo.levelInfos[_firstOfTheThreeLevelsToShow + i].image;
            }
        }
    }
    
    private void Start()
    {
        GameManager.Instance.SetElementsForLoadingScreen(_loaderCanvas, _progressBar);
        LoadTheThreeLevelsToShow();
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
        // In case a selected object disappears, a default object is selected
        if (_eventSystem.currentSelectedGameObject && !_eventSystem.currentSelectedGameObject.activeInHierarchy)
        {
            _eventSystem.SetSelectedGameObject(_buttonToFirstSelect);
        }
        // Click on Esc key to go back to main menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMainMenu();
        }
    }
    
    void OnGUI()
    {
        // To manage double arrows button click while navigating with keyboard
        if (Event.current.Equals(Event.KeyboardEvent("left")))
        {
            if (_eventSystem.currentSelectedGameObject == _doubleArrowLeftButton)
            {
                ClickedDoubleArrowLeftButton();
            }
        }
        if (Event.current.Equals(Event.KeyboardEvent("right")))
        {
            if (_eventSystem.currentSelectedGameObject == _doubleArrowRightButton)
            {
                ClickedDoubleArrowRightButton();
            }
        }
    }

    public void ClickedDoubleArrowLeftButton() // Click left arrows to show previous levels
    {
        _firstOfTheThreeLevelsToShow = _firstOfTheThreeLevelsToShow - 1;
        LoadTheThreeLevelsToShow();
    }
    
    public void ClickedDoubleArrowRightButton() // Click right arrows to show next levels
    {
        _firstOfTheThreeLevelsToShow = _firstOfTheThreeLevelsToShow + 1;
        LoadTheThreeLevelsToShow();
    }

    private void ClickedShowedLevel(int num) // Click on a showed level
    {
        int levelYouAreGoingToPlay = _levelsInfo.levelInfos[_firstOfTheThreeLevelsToShow + num].levelNumber;
        GameManager.Instance.PlayLevel(levelYouAreGoingToPlay);
    }
    
    public void ClickedShowedLevel1Button() // Click on the first showed level
    {
        ClickedShowedLevel(0);
    }
    
    public void ClickedShowedLevel2Button() // Click on the second showed level
    {
        ClickedShowedLevel(1);
    }
    
    public void ClickedShowedLevel3Button() // Click on the third showed level
    {
        ClickedShowedLevel(2);
    }

    public void GoToMainMenu() // Go to main menu
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void GoToSettings() // Go to settings menu
    {
        SceneManager.LoadScene("SettingMenu");
    }
    
}
