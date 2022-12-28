using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingImageAnimation : MonoBehaviour
{
    
    private Animator _LoadingImageAnimator;
    
    public void StartAnimation() // Start an animation of Luke during the loading screen
    {
        _LoadingImageAnimator = GetComponent<Animator>();
        _LoadingImageAnimator.SetTrigger("loadingImageAnimation");
    }

}
