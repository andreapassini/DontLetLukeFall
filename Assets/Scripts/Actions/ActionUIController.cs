using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;

public class ActionUIController : MonoBehaviour
{

    [SerializeField] private Sprite _transparentSprite; // This is a transparent sprite used when there are less actions then the number of actions to display
    [SerializeField] private Image[] _displayActionImages; // this array is setted with images via inspector
    // Each Image represent an action
    [SerializeField] private Image _imageBar; // this is the image with the bar witch has an animation to show the current action
    [SerializeField] private Image _imageExplosionChangeAction; // this is the image to show an explosion animation when changing action
    
    private List<Sprite> _actionsSprites; // In this list it will memorized the list of sprite of actions using method LoadActionSequence
    private const float TimeToCompleteAnimation = 1f;

    private bool _hasBeenLoaded;

    private bool _animationBarActive = false;
    private float _animationBarVelocity = 0f;
    
    private Animator _explosionChangeActionAnimator;

    private void Awake()
    {
        foreach (var el in _displayActionImages) // starting fixings
        {
            el.sprite = _transparentSprite;
            el.GetComponent<Animator>().enabled = false;
        }
        _animationBarActive = false;
        UpdateBarAmount(0f);
        _explosionChangeActionAnimator = _imageExplosionChangeAction.GetComponent<Animator>();
        _explosionChangeActionAnimator.enabled = false;
    }

    private void Update()
    {
        if (_animationBarActive)
        {
            UpdateBarAmount(Mathf.MoveTowards(_imageBar.fillAmount, 0f, _animationBarVelocity * Time.deltaTime));
        }
    }

    private void UpdateBarAmount(float percentage)
    {
        _imageBar.fillAmount = percentage;
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

    private void ShowExplosionChangeActionAnimation()
    {
        //_explosionChangeActionAnimator.enabled = true;
        //_explosionChangeActionAnimator.SetTrigger("animationExplosionChangeAction");
    }

    public void LoadActionSequence(List<Sprite> actionsSprites, float durationOfTheFirstAction) 
    // This method loads the list of sprite of actions
    {
        ShowExplosionChangeActionAnimation();
        _animationBarActive = true;
        _animationBarVelocity = TimeToCompleteAnimation / durationOfTheFirstAction;
        UpdateBarAmount(1f);
        _actionsSprites = actionsSprites;
        _hasBeenLoaded = true;
        UpdateUi();
    }

    public void NextAction(float durationOfThisAction)
    // This method pops the current action and lets the others shift right
    {
        ShowExplosionChangeActionAnimation();
        _animationBarVelocity = TimeToCompleteAnimation / durationOfThisAction;
        UpdateBarAmount(1f);
        _actionsSprites.RemoveAt(0);
        UpdateUi();
    }

    public void StopSequence()
    {
        gameObject.SetActive(false);
    }

}
