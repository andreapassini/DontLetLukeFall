using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DLLF;
using MoreMountains.Tools;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // The game manager is a singleton
    
    [SerializeField] private LevelsInfo _levelsInfo;
    
    private GameObject _loaderCanvas; // The panel in a scene to show the loading screen
    private Image _progressBar; // The image of the progress bar in a loading screen
    private float _targetForProgressBar; // The target to mouve the progress bar
    private bool _needToUpdateTheProgressBar = false; // If we need to update the value of the progress bar
    private float _maxSpeedProgressBar = 2.0f; // Max speed for the progress bar

    public GameState state;
    
    private int _levelToPlay; // The level you are playing / you are going to play

    public static event Action<GameState> OnGameStateChanged;

    private AudioManager _audioManager;

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
        _audioManager = AudioManager.instance;

        UpdateGameState(GameState.SplashScreen); // Setting the initial state
    }

    private void Update()
    {
        if (_needToUpdateTheProgressBar)
        {
            _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _targetForProgressBar, _maxSpeedProgressBar * Time.deltaTime);
        }
    }

    public void SetElementsForLoadingScreen(GameObject loaderCanvas, Image progressBar)
    // A method to set the loader canvas and the progress bar for the loading screen
    {
        _loaderCanvas = loaderCanvas;
        _progressBar = progressBar;
    }
    
    public async void LoadScene(string sceneName) // A function to load a scene showing a loading screen
    {
        if (_progressBar == null || _loaderCanvas == null)
        {
            SceneManager.LoadScene(sceneName);
            return;
        }
        
        _needToUpdateTheProgressBar = true;
        _targetForProgressBar = 0;
        _progressBar.fillAmount = 0;
        
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        
        _loaderCanvas.SetActive(true);

        do
        {
            await UniTask.Delay(100);
            _targetForProgressBar = scene.progress;
            _targetForProgressBar = 1.0f * (_targetForProgressBar / 0.9f);
            if (_targetForProgressBar > 1.0f)
            {
                _targetForProgressBar = 1.0f;
            }
        } while (scene.progress < 0.9f || _progressBar.fillAmount < 0.9f);
        _targetForProgressBar = 1.0f;
        
        scene.allowSceneActivation = true;
        _needToUpdateTheProgressBar = false;
    }

    public void UpdateGameState(GameState newState) // A public method to change the state
    {
        state = newState;
        
        switch (newState)
        {
            case GameState.SplashScreen:
                HandleSplashScreen();
                break;
            case GameState.Intro:
                HandleIntro();
                break;
            case GameState.MainMenu:
                HandleMainMenu();
                break;
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

    private void HandleSplashScreen()
    {
        SceneManager.LoadScene("SplashScreen");
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

    public void HandleIntro()
    {
        SceneManager.LoadScene("Intro");
    }

    private void HandleMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

        AudioManager.instance.StopAllAudioSources();
        AudioManager.instance.PlayIntro();
    }

    private void HandleSelectionLevel()
    {
        SceneManager.LoadScene("LevelSelectionMenu");

        AudioManager.instance.StopAllAudioSources();
        AudioManager.instance.PlayIntro();
    }

    private void HandlePlaying() // show the scene with the level to play
    {
        if (_levelToPlay == 1)
        {
            GameManager.Instance.LoadScene("Tutorial");
        }
        else
        {
            int levelToPlayNameScene = _levelToPlay - 1;
            GameManager.Instance.LoadScene("Level" + levelToPlayNameScene);
        }
        TextFileManager.AddWitchLevelYouStartPlaying();

        AudioManager.instance.StopAllAudioSources();
        AudioManager.instance.PlayBackgroundMusic();
    }

    private void LoadYouLoseWonScene()
    {
        SceneManager.LoadScene("YouLoseWon");
        AudioManager.instance.StopAllAudioSources();
        AudioManager.instance.PlayIntro();
    }

    private void HandleLose() // Show the screen you lose
    {
        LoadYouLoseWonScene();
        TextFileManager.AddThatYouLostALevel();
        AudioManager.instance.StopAllAudioSources();
        AudioManager.instance.PlayIntro();
    }

    private void HandleWin() // Show the screen you won
    {
        LoadYouLoseWonScene();
        TextFileManager.AddThatYouWonALevel();
        AudioManager.instance.StopAllAudioSources();
        AudioManager.instance.PlayIntro();
    }

}

public enum GameState // The possible states of the game
{
    SplashScreen,
    Intro,
    MainMenu,
    SelectionLevel,
    Playing,
    Lose,
    Win
}
