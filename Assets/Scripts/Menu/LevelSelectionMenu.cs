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

    [SerializeField] private LevelsInfo _levelsInfo; // The information of the levels
    [SerializeField] private GameObject _buttonToFirstSelect; // Button to first select when starting to navigate with keyboard
    [SerializeField] private EventSystem _eventSystem; // The event system to witch attach this script
    [SerializeField] private GameObject _doubleArrowLeftButton; // The button to scroll left between levels
    [SerializeField] private GameObject _doubleArrowRightButton; // The button to scroll right between levels
    [SerializeField] private GameObject[] _imageLevelButtons; // The three selectable images
    [SerializeField] private Image[] _imagesLevels; // The three images of the three levels
    [SerializeField] private Text[] _levelTitleTexts; // The text files for the titles of the levels
    [SerializeField] private Text[] _levelMiniTitleTexts; // The text files for the mini titles of the levels
    [SerializeField] private Text _pageNumberText; // A text to show the page number
    
    [SerializeField] private GameObject _loaderCanvas; // The loading screen
    [SerializeField] private Image _progressBar; // The progress bar in the loading screen
    
    [SerializeField] private ScriptForTransitionsBetweenMenuScenes _scriptForTransitionsBetweenMenuScenes; // A script to load the next scene with a transition

    private int _page = 0; // The page you are visualizing

    private void LoadTheThreeLevelsToShow() // Function to load the three levels to show
    {
        int totalNumberOfLevels = _levelsInfo.levelInfos.Length; // Total number of levels
        int totalNumberOfPages = totalNumberOfLevels / 3;
        if (totalNumberOfLevels % 3 > 0)
        {
            totalNumberOfPages++; // Total number of pages
        }
        if (_page < 0) // Checks on var _page
        {
            _page = 0;
        }
        if (_page >= totalNumberOfPages)
        {
            _page = totalNumberOfPages - 1;
        }
        // Set active / not active double arrow buttons
        for (int i = 0; i < 3; i++)
        {
            _imageLevelButtons[i].SetActive(true);
        }
        bool needToSelectLastImageLevelAvailable = false;
        _doubleArrowLeftButton.SetActive(true);
        if (_page == 0)
        {
            if (_eventSystem.currentSelectedGameObject == _doubleArrowLeftButton)
            {
                _eventSystem.SetSelectedGameObject(_imageLevelButtons[0]);
            }
            _doubleArrowLeftButton.SetActive(false);
        }
        _doubleArrowRightButton.SetActive(true);
        bool activateDoubleArrowRightButton = false;
        if (_page < totalNumberOfPages-1)
        {
            activateDoubleArrowRightButton = true;
        }
        if (activateDoubleArrowRightButton == false)
        {
            if (_eventSystem.currentSelectedGameObject == _doubleArrowRightButton)
            {
                _eventSystem.SetSelectedGameObject(_imageLevelButtons[0]);
                needToSelectLastImageLevelAvailable = true;
            }
            _doubleArrowRightButton.SetActive(false);
        }
        // Show the number of the page
        _pageNumberText.text = (_page+1).ToString() + "/" + totalNumberOfPages.ToString();
        // Load images of levels
        for (int i = 0; i < 3; i++)
        {
            _imagesLevels[i].enabled = true;
            _levelTitleTexts[i].enabled = true;
            _levelMiniTitleTexts[i].enabled = true;
            if ((_page * 3) + i >= totalNumberOfLevels)
            {
                _imagesLevels[i].enabled = false;
                _levelTitleTexts[i].enabled = false;
                _levelMiniTitleTexts[i].enabled = false;
                _imageLevelButtons[i].SetActive(false);
            }
            else
            {
                _imagesLevels[i].sprite = _levelsInfo.levelInfos[(_page * 3) + i].image;
                _levelTitleTexts[i].text = _levelsInfo.levelInfos[(_page * 3) + i].levelTitle;
                string miniTitle = _levelsInfo.levelInfos[(_page * 3) + i].levelTitle;
                if (miniTitle.Length > 5)
                {
                    miniTitle = miniTitle.Substring(0, 1);
                }
                _levelMiniTitleTexts[i].text = miniTitle;
            }
        }
        if (needToSelectLastImageLevelAvailable)
        {
            if (_imageLevelButtons[2].activeSelf)
            {
                _eventSystem.SetSelectedGameObject(_imageLevelButtons[2]);
            } else if (_imageLevelButtons[1].activeSelf)
            {
                _eventSystem.SetSelectedGameObject(_imageLevelButtons[1]);
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
                AudioManager.instance?.PlayClickMenuSFX();
                ClickedDoubleArrowLeftButton();
            }
        }
        if (Event.current.Equals(Event.KeyboardEvent("right")))
        {
            if (_eventSystem.currentSelectedGameObject == _doubleArrowRightButton)
            {
                AudioManager.instance?.PlayClickMenuSFX();
                ClickedDoubleArrowRightButton();
            }
        }
    }

    public void ClickedDoubleArrowLeftButton() // Click left arrows to show previous levels
    {
        _page = _page - 1;
        LoadTheThreeLevelsToShow();
    }
    
    public void ClickedDoubleArrowRightButton() // Click right arrows to show next levels
    {
        _page = _page + 1;
        LoadTheThreeLevelsToShow();
    }

    private void ClickedShowedLevel(int num) // Click on a showed level
    {
        int levelYouAreGoingToPlay = _levelsInfo.levelInfos[(_page * 3) + num].levelNumber;
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
        _scriptForTransitionsBetweenMenuScenes.AnimationEndTransitionBetweenMenuScenes("MainMenu");
    }
    
    public void GoToSettings() // Go to settings menu
    {
        _scriptForTransitionsBetweenMenuScenes.AnimationEndTransitionBetweenMenuScenes("SettingMenu");
    }
    
}
