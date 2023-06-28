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
        
        public int lastScore;
        private bool _newBestScore = false;
        
        private float _startingTime;
        private String levelName;

        private Dictionary<String, int> _scores;
        
        private LevelScore()
        {
            // Load from PlayerPref
            GameManager.onLevelComplete += CalculateScore;
            GameManager.onLevelStart += StartLevel;

            _scores = new Dictionary<string, int>();
        }

        public static LevelScore GetInstance()
        {
            PlayerPrefs.DeleteAll();
            
            if (instance == null)
            {
                instance = new LevelScore();
            }

            return instance;
        }

        private void ResetScore()
        {
            _newBestScore = false;
            _levelDuration = 0.0f;
            _usedPlatforms = 0;
        }

        private void StartLevel(String lvlName)
        {
            ResetScore();
            _startingTime = Time.time;
            this.levelName = lvlName;
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
            EndLevel();
            float currentScore = _levelDuration + PlatfromWeight * (_usedPlatforms);
            lastScore = (int)currentScore;
            
            int previousScore;
            if (_scores.TryGetValue(levelName, out previousScore))
            {
                if (currentScore < previousScore)
                {
                    _scores[levelName] = (int)currentScore;
                    _newBestScore = true;
                    
                    // Store to PlayerPref
                    PlayerPrefs.SetInt(levelName, (int)currentScore);
                    PlayerPrefs.Save();
                }
            }
            else
            {
                _scores.Add(levelName, (int)currentScore);
                _newBestScore = true;

                // Store to PlayerPref
                PlayerPrefs.SetInt(levelName, (int)currentScore);
                PlayerPrefs.Save();
            }
        }

        public float GetScore(String levelName)
        {
            int previousScore;
            if (_scores.TryGetValue(levelName, out previousScore))
            {
                return previousScore;
            }
            
            if(PlayerPrefs.HasKey(levelName))
            {
                int savedScore = PlayerPrefs.GetInt(levelName);
                _scores.Add(levelName, savedScore);
                return savedScore;
            }

            return float.MaxValue;
        }

        public float GetLastScore()
        {
            return lastScore;
        }

        public bool GetLastBestScore()
        {
            return _newBestScore;
        }
    }
}