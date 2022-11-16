using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // The game manager is a singleton

    public GameState state;

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

    private void HandleLose()
    {
        // TO IMPLEMENT Show the screen you lose
        // (in this screen there is a button to repeat the level or to return back to the main menu)
    }
    
    private void HandleWin()
    {
        // TO IMPLEMENT Show the screen you won
        // (in this screen there is a button to repeat the level, to go to the next level or to return back to the main menu)
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
