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

        [SerializeField] private StartingEndingLevelTransition _startingEndingLevelTransition;

        private TimedSlowMotion _slowMotion;
        
        void Awake()
        {
            //Register to onLevelFailedEvent and onLevelCompletedEvent
            EventManager.StartListening(OnLevelCompletedEventName, OnLevelCompleted);
            EventManager.StartListening(OnLevelFailedEventName, OnLevelFailed);
            _slowMotion = GetComponent<TimedSlowMotion>();
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

        private void OnLevelCompleted()
        {
            Debug.Log("Level completed!");
            if (_startingEndingLevelTransition != null)
            {
                _startingEndingLevelTransition.AnimationTransitionEndLevel(GameState.Win); // Show the ending transition at the end of the level
            }
            else
            {
                Debug.Log("You've missed to connect the panel for the ending level transition");
                GameManager.Instance.UpdateGameState(GameState.Win);
            }
        }

        private void OnLevelFailed()
        {
            Debug.Log("Level failed!");
            if (_startingEndingLevelTransition != null)
            {
                _startingEndingLevelTransition.AnimationTransitionEndLevel(GameState.Lose); // Show the ending transition at the end of the level
            }
            else
            {
                Debug.Log("You've missed to connect the panel for the ending level transition");
                GameManager.Instance.UpdateGameState(GameState.Lose);
            }
        }

        public ISlowMotion GetSlowMo()
        {
            return _slowMotion;
        }
    }
    

}