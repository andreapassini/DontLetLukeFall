using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.SelectionLevel);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        
        switch (newState)
        {
            case GameState.SelectionLevel:
                break;
            case GameState.Playing:
                break;
            case GameState.Lose:
                break;
            case GameState.Win:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

}

public enum GameState
{
    SelectionLevel,
    Playing,
    Lose,
    Win
}
