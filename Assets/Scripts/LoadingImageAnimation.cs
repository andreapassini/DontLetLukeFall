using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingImageAnimation : MonoBehaviour
{
    
    private Animator _LoadingImageAnimator;
    
    public void StartAnimation()
    {
        _LoadingImageAnimator = GetComponent<Animator>();
        _LoadingImageAnimator.SetTrigger("loadingImageAnimtion");
    }

}
