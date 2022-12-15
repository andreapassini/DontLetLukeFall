using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionUIController : MonoBehaviour
{

    [SerializeField] private Sprite _transparentSprite; // This is a transparent sprite used when there are less actions then the number of actions to display
    [SerializeField] private Image[] _displayActionImages; // this array is setted with images via inspector
    // Each Image represent an action
    [SerializeField] private Image _imageBar; // this is the image with the bar witch has an animation to show the current action
    
    private List<Sprite> _actionsSprites; // In this list it will memorized the list of sprite of actions using method LoadActionSequence
    private const float TimeToCompleteAnimation = 0.5f;

    private Animator _imageBarAnimator;
    private bool _hasBeenLoaded;

    private void Awake()
    {
        foreach (var el in _displayActionImages) // starting fixings
        {
            el.sprite = _transparentSprite;
            el.GetComponent<Animator>().enabled = false;
        }
        _imageBarAnimator = _imageBar.GetComponent<Animator>();
        _imageBarAnimator.enabled = false; // At the beginning the bar is disabled and it is enabled when it's called LoadActionSequence
    }

    public int GetNumberOfDisplayedActions()
    // Return the number of displayed actions
    {
        return _displayActionImages.Length;
    }

    public bool HasBeenLoaded()
    {
        return _hasBeenLoaded;
    }

    private void UpdateUi() // update the actions ui
    {
        int j = 0;
        for (int i = _displayActionImages.Length - 1; i >= 0; i--)
        {
            try
            {
                _displayActionImages[i].sprite = _actionsSprites[j];
            }
            catch (ArgumentOutOfRangeException)
            {
                _displayActionImages[i].sprite = _transparentSprite;
            }
            j++;
        }
    }

    public void LoadActionSequence(List<Sprite> actionsSprites, float durationOfTheFirstAction) 
    // This method loads the list of sprite of actions
    {
        _imageBarAnimator.enabled = true;
        _imageBarAnimator.speed = TimeToCompleteAnimation / durationOfTheFirstAction;
        _imageBarAnimator.SetTrigger("RestartAnimationOfTheBarTrigger");
        _actionsSprites = actionsSprites;
        _hasBeenLoaded = true;
        UpdateUi();
    }

    public void NextAction(float durationOfThisAction)
    // This method pops the current action and lets the others shift right
    {
        _imageBarAnimator.speed = TimeToCompleteAnimation / durationOfThisAction;
        _imageBarAnimator.SetTrigger("RestartAnimationOfTheBarTrigger");
        _actionsSprites.RemoveAt(0);
        UpdateUi();
    }

    public void StopSequence()
    {
        gameObject.SetActive(false);
    }

}
