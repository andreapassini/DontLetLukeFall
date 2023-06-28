using System;
using System.Collections;
using System.Collections.Generic;
using DLLF;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText;
    
    void Start()
    {
        String newBestScoreTex;
        
        if (LevelScore.GetInstance().GetLastBestScore())
        {
            scoreText.color = Color.yellow;
            newBestScoreTex = "New Best Score: ";
        }
        else
        {
            scoreText.color = Color.white;
            newBestScoreTex = "";
        }
        
        scoreText.text = newBestScoreTex + LevelScore.GetInstance().GetLastScore().ToString();
    }
}
