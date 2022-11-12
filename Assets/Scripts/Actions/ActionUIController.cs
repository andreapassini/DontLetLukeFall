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
    private Image[] _displayActionImages; // this array is initialized with 5 elements (Image) via inspector
    // Each Image represent an action
    private Image _imageBar; // this is the image with the bar witch has an animation to show the current action
    private List<Sprite> _actionsSprites; // In this list it will memorized the list of sprite of actions using method LoadActionSequence
    private const float TimeToCompleteAnimation = 0.5f;

    private void Awake()
    // At the beginning images are created; these images go to be located where there was the image to be cloned (at the center of the canvas)
    {
        float imageLenght = _image.rectTransform.rect.width;
        float movingPosFirstImage = ((imageLenght * _numActionsToDisplay) / 2) - (imageLenght / 2);
        _image.rectTransform.position = new Vector3(_image.rectTransform.position.x - movingPosFirstImage,
            _image.rectTransform.position.y, _image.rectTransform.position.z);
        // The image to be cloned is moved to the fist position
        _displayActionImages = new Image[_numActionsToDisplay];
        for (int i = 0; i < _numActionsToDisplay; i++)
        {
            // For each iteration the image to be cloned is cloned and then moved to the next position
            Image newImage = Instantiate(_image, _image.transform.position, _image.transform.rotation, _canvas.transform);
            _displayActionImages[i] = newImage;
            newImage.sprite = null;
            newImage.GetComponent<Animator>().enabled = false;
            _image.rectTransform.position = new Vector3(_image.rectTransform.position.x + imageLenght,
                _image.rectTransform.position.y, _image.rectTransform.position.z);
        }
        _imageBar = Instantiate(_image, _image.transform.position, _image.transform.rotation, _canvas.transform); // This is the image to show the bar of the action
        _image.enabled = false; // The original image to be cloned is disabled
        _imageBar.transform.position = _displayActionImages[_displayActionImages.Length-1].transform.position; // The bar will be in the same position of the last image
    }

    public int GetNumberOfDisplayedActions()
    // Return witch number of actions to display
    {
        return _displayActionImages.Length;
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
                _displayActionImages[i].sprite = null;
            }
            j++;
        }
    }

    public void LoadActionSequence(List<Sprite> actionsSprites, float durationOfTheFirstAction) 
    // This method is to load the list of sprite of actions
    {
        _imageBar.GetComponent<Animator>().speed = TimeToCompleteAnimation / durationOfTheFirstAction;
        _actionsSprites = actionsSprites;
        UpdateUi();
    }

    public void NextAction(float durationOfThisAction)
    // This method is to pop the current action and let the other shift right
    {
        _imageBar.GetComponent<Animator>().speed = TimeToCompleteAnimation / durationOfThisAction;
        _imageBar.GetComponent<Animator>().SetTrigger("RestartAnimationOfTheBarTrigger");
        _actionsSprites.RemoveAt(0);
        UpdateUi();
    }

}
