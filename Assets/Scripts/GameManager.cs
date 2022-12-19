using System;
using System.Collections;
using System.Collections.Generic;
using DLLF;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // The game manager is a singleton
    
    [SerializeField] private LevelsInfo _levelsInfo;

    public GameState state;
    
    private int _levelToPlay; // The level you are playing / you are going to play

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
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
                HandleSelectionLevel();
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
    
    public int GetLevelToPlay()
    {
        return _levelToPlay;
    }

    public void PlayLevel(int levelToPlay)
    {
        _levelToPlay = levelToPlay;
        UpdateGameState(GameState.Playing);
    }

    public bool LevelToPlayIsTheLastOne() // return true if var levelToPlay is the last level available, else false
    {
        if (_levelsInfo.levelInfos.Length <= _levelToPlay)
        {
            return true;
        }
        return false;
    }
    
    public void NextLevelToPlay() // update var levelToPlay with the next level to play
    {
        _levelToPlay++;
    }

    private void HandleSelectionLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void HandlePlaying() // show the scene with the level to play
    {
        if (_levelToPlay == 1)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            int levelToPlayNameScene = _levelToPlay - 1;
            SceneManager.LoadScene("Level" + levelToPlayNameScene);
        }
        TextFileManager.AddWitchLevelYouStartPlaying();
    }

    private void HandleLose() // Show the screen you lose
    {
        SceneManager.LoadScene("YouLoseWon");
        TextFileManager.AddThatYouLostALevel();
    }
    
    private void HandleWin() // Show the screen you won
    {
        SceneManager.LoadScene("YouLoseWon");
        TextFileManager.AddThatYouWonALevel();
    }

}

public enum GameState // The possible states of the game
{
    SelectionLevel,
    Playing,
    Lose,
    Win
}
