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
        void Awake()
        {
            //Register to onLevelFailedEvent and onLevelCompletedEvent
            EventManager.StartListening(OnLevelCompletedEventName, OnLevelCompleted);
            EventManager.StartListening(OnLevelFailedEventName, OnLevelFailed);

        }

        void Start()
        {
            _levelActionsManager.Begin(_levelActionsSequence);
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
        }

        private void OnLevelFailed()
        {
            Debug.Log("Level failed!");
        }
    }

}