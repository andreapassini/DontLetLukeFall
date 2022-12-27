using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class AnimationPlacingPlatform : MonoBehaviour
{

    [SerializeField] private MMFeedbacks _feedbackAnimationPlacingPlatform;

    public void ExecuteAnimation()
    {
        _feedbackAnimationPlacingPlatform?.PlayFeedbacks();
    }
    
}
