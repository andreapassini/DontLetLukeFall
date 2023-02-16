using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenAnimation : MonoBehaviour
{
    public GameManager gameManager;
    
    public void Intro()
    { 
        gameManager.HandleIntro();
    }
}
