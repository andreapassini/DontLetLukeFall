using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLLF
{
    public class LevelManager : MonoBehaviour
    {
        public static string OnLevelFailedEventName = "onLevelFailedEvent";
        public static string OnLevelCompletedEventName = "onLevelCompletedEvent";
        
        [SerializeField] private ActionsSequence _levelActionsSequence;
        [SerializeField] private ActionsManager _levelActionsManager;

        [SerializeField] private PlatformSequence _levelPlatformSequence;
        [SerializeField] private PlatformBar _levelPlatformBar;

        void Awake()
        {
            //Register to onLevelFailedEvent and onLevelCompletedEvent
            EventManager.StartListening(OnLevelCompletedEventName, OnLevelCompleted);
            EventManager.StartListening(OnLevelFailedEventName, OnLevelFailed);

        }

        void Start()
        {
            _levelActionsManager.Begin(_levelActionsSequence);
            _levelPlatformBar.EnqueuePlatforms(_levelPlatformSequence);
        }

        public ActionsSequence GetLevelActionsSequence()
        {
            return _levelActionsSequence;
        }
        
        public PlatformSequence GetLevelPlatformSequence()
        {
            return _levelPlatformSequence;
        }

        private void OnDestroy()
        {
            EventManager.StopListening(OnLevelCompletedEventName, OnLevelCompleted);
            EventManager.StopListening(OnLevelFailedEventName, OnLevelFailed);
        }

        //TODO integrare con GameManager quando andrà in develop
        private void OnLevelCompleted()
        {
            Debug.Log("Level completed!");
            GameManager.Instance.UpdateGameState(GameState.Win);
        }

        private void OnLevelFailed()
        {
            Debug.Log("Level failed!");
            GameManager.Instance.UpdateGameState(GameState.Lose);
        }
    }

}