using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionUIController : MonoBehaviour
{

    [SerializeField] private int _numActionsToDisplay; // This is to set the number of images
    [SerializeField] private GameObject _canvas; // This is the canvas where to visualize images
    [SerializeField] private Image _image; // This is the image to be cloned; this image should be located at the center of the canvas
    [SerializeField] private Sprite _transparentSprite; // This is a transparent sprite used when there are less actions then the number of actions to display
    [SerializeField] private bool _avoidDoubleOffset; // Avoid double offset when showing images of actions in action ui
    
    private Image[] _displayActionImages; // this array is initialized with 5 elements (Image) via inspector
    // Each Image represent an action
    private Image _imageBar; // this is the image with the bar witch has an animation to show the current action
    private List<Sprite> _actionsSprites; // In this list it will memorized the list of sprite of actions using method LoadActionSequence
    private const float TimeToCompleteAnimation = 0.5f;

    private Animator _imageBarAnimator;
    private bool _hasBeenLoaded;

    private void Awake()
    // At the beginning images are created; these images go to be located where there was the image to be cloned (at the center of the canvas)
    {
        float imageLenght = _image.rectTransform.rect.width;
        float movingPosFirstImage = (imageLenght * (_numActionsToDisplay - 1));
        _image.rectTransform.position = new Vector3(_image.rectTransform.position.x - movingPosFirstImage,
            _image.rectTransform.position.y, _image.rectTransform.position.z);
        if (_avoidDoubleOffset == false)
        {
            _image.rectTransform.position = new Vector3(_image.rectTransform.position.x - movingPosFirstImage,
                _image.rectTransform.position.y, _image.rectTransform.position.z);
        }
        // The image to be cloned is moved to the fist position
        _displayActionImages = new Image[_numActionsToDisplay];
        for (int i = 0; i < _numActionsToDisplay; i++)
        {
            // For each iteration the image to be cloned is cloned and then moved to the next position
            Image newImage = Instantiate(_image, _image.transform.position, _image.transform.rotation, gameObject.transform);
            _displayActionImages[i] = newImage;
            newImage.sprite = _transparentSprite;
            newImage.GetComponent<Animator>().enabled = false;
            _image.rectTransform.position = new Vector3(_image.rectTransform.position.x + imageLenght,
                _image.rectTransform.position.y, _image.rectTransform.position.z);
            if (_avoidDoubleOffset == false)
            {
                _image.rectTransform.position = new Vector3(_image.rectTransform.position.x + imageLenght,
                    _image.rectTransform.position.y, _image.rectTransform.position.z);
            }
        }
        _imageBar = Instantiate(_image, _image.transform.position, _image.transform.rotation, gameObject.transform); // This is the image to show the bar of the action
        _image.enabled = false; // The original image to be cloned is disabled
        _imageBarAnimator = _imageBar.GetComponent<Animator>();
        _imageBarAnimator.enabled = false; // At the beginning the bar is disabled and it is enabled when it's called LoadActionSequence
        _imageBar.transform.position = _displayActionImages[_displayActionImages.Length-1].transform.position; // The bar will be in the same position of the last image
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
