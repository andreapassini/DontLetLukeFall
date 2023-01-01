using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingEndingLevelTransition : MonoBehaviour
// This script is to manage the transitions at the start or at the end of a level
{
    
    // The start level transition is activated automatically via animator
    
    public void AnimationTransitionEndLevel(GameState gameState) // To activate the end level transition
    {
        StartCoroutine(LoadYouWonYouLoseScene(gameState));
    }

    IEnumerator LoadYouWonYouLoseScene(GameState gameState)
    {
        gameObject.GetComponent<Animator>().SetTrigger("end");
        yield return new WaitForSeconds(2f); // Do the animation of the transition
        if (gameState == GameState.Win)
        {
            GameManager.Instance.UpdateGameState(GameState.Win); // Set the game state and go to the YouWonYouLose scene
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameState.Lose); // Set the game state and go to the YouWonYouLose scene
        }
    }
    
}
