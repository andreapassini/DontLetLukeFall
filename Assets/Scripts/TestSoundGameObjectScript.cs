using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSoundGameObjectScript : MonoBehaviour
// This is a gameObject I used only to test the audio manager: when this object is disabled (OnDisable) it plays a sound
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        FindObjectOfType<AudioManager>().Play("BackgroungMusic");
        // To play a sound you have just to call this function passing the name of the sound to play
    }

    private bool firstTime = true;
    
    private void OnEnable()
    {
        if (firstTime)
        {
            firstTime = false;
            return;
        }
        Debug.Log("The volume is: " + (FindObjectOfType<AudioManager>().GetVolumeScriptableObject("BackgroungMusic")));
        FindObjectOfType<AudioManager>().Stop("BackgroungMusic");
        // To stop a looped sound (such as the BackgroungMusic) you have just to call this function passing the name of the sound to stop
    }
    
}
