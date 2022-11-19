using System;
using System.Collections;
using System.Collections.Generic;
using DLLF;
using UnityEngine;
using UnityEngine.UI;

public class ActionsVisualizer : MonoBehaviour
// This script is to visualize actions while creating levels; this script should be disabled in the final game
{

    [SerializeField] private GameObject _luke; // Luke
    [SerializeField] private Canvas _canvas; // The canvas
    [SerializeField] private Camera _camera; // The main camera
    [SerializeField] private ActionsSprites _actionsSprites; // The action sprites
    [SerializeField] private Image _image; // The image to be cloned to visualize the other actions

    private const float _referenceCameraSize = 8.457275f; // to fix image dimensions based on camera size
    private const float _referenceImageMeasure = 321f; // to fix image dimensions based on dimension of a platform
    private const float _referenceCanvasWidth = 1920f; // to fix image position based on camera size
    private const float _referenceMultiplierForLukePosition = 952.0f / 15.0f; // to fix image position based on ratio between canvas size and unity unit measure
    
    private void Awake()
    {
        // fixing image dimensions
        float measure = _referenceCameraSize * _referenceImageMeasure * _canvas.pixelRect.width / (_camera.orthographicSize * _referenceCanvasWidth);
        ((RectTransform)_image.transform).sizeDelta = new Vector2 (measure, measure);
        // fixing image position to Luke's position
        Vector3 relativePositionOfLukeToCamera = _camera.transform.InverseTransformDirection(_luke.transform.position - _camera.transform.position);
        float multiplier = _referenceMultiplierForLukePosition;
        multiplier = multiplier * _referenceCameraSize / _camera.orthographicSize;
        multiplier = multiplier * _canvas.pixelRect.width / _referenceCanvasWidth;
        _image.transform.position = new Vector3(multiplier * relativePositionOfLukeToCamera.x + _canvas.pixelRect.width / 2,multiplier * relativePositionOfLukeToCamera.y + _canvas.pixelRect.height / 2, _image.transform.position.z);
        // fixing image position so that is located in the place of the actions
        float imageLenght = _image.rectTransform.rect.width;
        _image.transform.position = new Vector3(_image.transform.position.x + imageLenght / 2, _image.transform.position.y,
            _image.transform.position.z);
        // cloning the image
        ActionsManager actionsManager = _luke.GetComponent<ActionsManager>();
        foreach (var el in actionsManager.GetActionSequence())
        {
            Image newImage = Instantiate(_image, _image.transform.position, _image.transform.rotation, gameObject.transform);
            _image.rectTransform.position = new Vector3(_image.rectTransform.position.x + imageLenght,
                _image.rectTransform.position.y, _image.rectTransform.position.z);
            newImage.sprite = _actionsSprites.GetSprite(el);
        }
        _image.enabled = false;
    }

}
