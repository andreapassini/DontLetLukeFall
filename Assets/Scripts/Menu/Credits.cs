using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    // This script is to manage credits
    
    void Update()
    {
        // User can click any key to return back to main menu
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    
}
