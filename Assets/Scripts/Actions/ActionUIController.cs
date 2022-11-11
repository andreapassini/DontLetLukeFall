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
    private List<Sprite> _actionsSprites; // In this list it will memorized the list of sprite of actions using method LoadActionSequence

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
            _image.rectTransform.position = new Vector3(_image.rectTransform.position.x + imageLenght,
                _image.rectTransform.position.y, _image.rectTransform.position.z);
        }
        _image.enabled = false; // The original image to be cloned is disabled
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

    public void LoadActionSequence(List<Sprite> actionsSprites) // This method is to load the list of sprite of actions
    {
        _actionsSprites = actionsSprites;
        UpdateUi();
    }

    public void NextAction() // This method is to pop the current action and let the other shift right
    {
        Sprite removed = _actionsSprites[0];
        _actionsSprites.RemoveAt(0);
        //_actionsSprites.Add(removed); // add this line to show actions in loop
        UpdateUi();
    }

}
