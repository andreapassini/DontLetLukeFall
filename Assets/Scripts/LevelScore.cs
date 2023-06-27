using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DLLF
{
    public class LevelScore
    {
        private static LevelScore instance;

        private float _levelDuration;
        private int _usedPlatforms;
        public float PlatfromWeight = 1.0f;
        private float _startingTime;
        private String levelName;

        private Dictionary<String, float> _scores;
        
        private LevelScore()
        {
            // Load from PlayerPref
            EventManager.StartListening(LevelManager.OnLevelCompletedEventName, CalculateScore);
        }

        public static LevelScore GetInstance()
        {
            if (instance == null)
            {
                instance = new LevelScore();
            }

            return instance;
        }

        public void ResetScore()
        {
            _levelDuration = 0.0f;
            _usedPlatforms = 0;
        }

        public void StartLevel()
        {
            _startingTime = Time.time;
        }

        public void EndLevel()
        {
            _levelDuration = Time.time - _startingTime;
        }

        public void IncrementUsedPlatforms()
        {
            _usedPlatforms++;
        }

        // Called by endLevel event
        private void CalculateScore()
        {
            float previousScore;
            float currentScore = _levelDuration + PlatfromWeight * (_usedPlatforms);
            if (_scores.TryGetValue(levelName, out previousScore))
            {
                if (currentScore < previousScore)
                {
                    _scores[levelName] = currentScore;
                    
                    // Store to PlayerPref
                    
                }
            }
            else
            {
                _scores.Add(levelName, currentScore);
            }
        }

        public float GetScore(String levelName)
        {
            float previousScore;
            if (_scores.TryGetValue(levelName, out previousScore))
            {
                return previousScore;
            }

            return float.MaxValue;
        }
    }
}