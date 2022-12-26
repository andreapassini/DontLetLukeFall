using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class LukeFeedbacks : MonoBehaviour
{
    [SerializeField] private MMF_Player _jumpFeedback;
    [SerializeField] private MMF_Player _landingFeedback;
    [SerializeField] private MMF_Player _sprintFeedback;

    public void PlayJumpFeedback()
    {
        _jumpFeedback?.PlayFeedbacks();
    }

    public void PlayLandingFeedback()
    {
        _jumpFeedback?.StopFeedbacks();
        _landingFeedback?.PlayFeedbacks();
    }

    public void PlaySprintFeedback()
    {
        _sprintFeedback?.PlayFeedbacks();
    }

    public void StopSprintFeedback()
    {
        _sprintFeedback?.StopFeedbacks();
    }

    public void StopAllFeedbacks()
    {
        _jumpFeedback?.StopFeedbacks();
        //_landingFeedback?.StopFeedbacks();
        _sprintFeedback?.StopFeedbacks();
    }
}
