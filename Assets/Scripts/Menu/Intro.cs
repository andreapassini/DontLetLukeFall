using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    private void Start()
    {
        _gameManager ??= FindObjectOfType<GameManager>();
    }

    public void EndIntro()
    {
        _gameManager?.UpdateGameState(GameState.MainMenu);
    }
}
