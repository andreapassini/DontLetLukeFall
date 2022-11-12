using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{

    [SerializeField] private GameObject _buttonToFirstSelect;
    [SerializeField] private EventSystem _eventSystem;

    void Update()
    {
        if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
        {
            if (!_eventSystem.currentSelectedGameObject)
            {
                _eventSystem.SetSelectedGameObject(_buttonToFirstSelect);
            }
        }
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
