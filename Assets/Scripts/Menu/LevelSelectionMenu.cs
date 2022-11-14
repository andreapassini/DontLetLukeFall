using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelSelectionMenu : MonoBehaviour
{
    // This script is to manage the level selection menu
    
    [SerializeField] private GameObject _buttonToFirstSelect; // Button to first select when starting to navigate with keyboard
    [SerializeField] private EventSystem _eventSystem; // The event system to witch attach this script
    
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

    public void ClickedDoubleArrowLeftButton()
    {
        
    }
    
    public void ClickedDoubleArrowRightButton()
    {
        
    }
    
    public void ClickedShowedLevel1Button()
    {
        
    }
    
    public void ClickedShowedLevel2Button()
    {
        
    }
    
    public void ClickedShowedLevel3Button()
    {
        
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
