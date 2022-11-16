using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // The game manager is a singleton

    public GameState state;
    
    public string levelToPlay; // TO FIX witch tipe of var to represent a level? (string?)

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.SelectionLevel); // Setting the initial state
    }

    public void UpdateGameState(GameState newState) // A public method to change the state
    {
        state = newState;
        
        switch (newState)
        {
            case GameState.SelectionLevel:
                break;
            case GameState.Playing:
                HandlePlaying();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            case GameState.Win:
                HandleWin();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void SetLevelToPlay(string newLevelToPlay)
    {
        levelToPlay = newLevelToPlay;
    }
    
    public void NextLevelToPlay()
    {
        // TO IMPLEMENT update var levelToPlay with the next level to play
    }

    private void HandlePlaying()
    {
        // TO IMPLEMENT show the scene with the level to play
        // (To know witch level to play check levelToPlay var)
    }

    private void HandleLose() // Show the screen you lose
    {
        SceneManager.LoadScene("YouLoseWon");
    }
    
    private void HandleWin() // Show the screen you won
    {
        SceneManager.LoadScene("YouLoseWon");
    }

}

public enum GameState // The possible states of the game
{
    SelectionLevel,
    Playing,
    Lose,
    Win
}

/*
 * To subscribe on an event:
 * void Awake(){
 *  GameManager.OnGameStateChanged += newMethod
 * }
 * To unsubscribe on an event:
 * void OnDestroy(){
 *  GameManager.OnGameStateChanged -= newMethod
 * }
 * New method:
 * private void newMethod(GameState state){
 *  //content of the method
 * }
 * To update the state:
 * GameManager.instance.UpdateGameState(.....);
 */
