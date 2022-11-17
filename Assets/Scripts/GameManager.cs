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
    
    public int levelToPlay; // The level you are playing / you are going to play

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

    public void SetLevelToPlay(int newLevelToPlay)
    {
        levelToPlay = newLevelToPlay;
    }

    public bool LevelToPlayIsTheLastOne()
    {
        // TODO
        // TO FIX return true if var levelToPlay is the last level available, else false
        if (_levelsInfo.levelInfos.Length <= levelToPlay)
        {
            return true;
        }
        return false;
    }
    
    public void NextLevelToPlay() // update var levelToPlay with the next level to play
    {
        levelToPlay++;
    }

    private void HandlePlaying() // show the scene with the level to play
    {
        SceneManager.LoadScene("Level" + levelToPlay);
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
