using System;
using System.Collections;
using System.Collections.Generic;
using DLLF;
using UnityEngine;
using UnityEngine.UI;

public class ActionsEditorScriptVisualizer : MonoBehaviour
{

    [SerializeField] private GameObject _luke;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Camera _camera;
    [SerializeField] private ActionsSprites _actionsSprites;
    [SerializeField] private Image _image;

    private const float _referenceCameraSize = 8.457275f;
    private const float _referenceImageMeasure = 321f;
    private const float _adapterCameraCanvas = 0.0005f;
    
    private void Awake()
    {
        // fixing image dimensions
        float measure = _referenceCameraSize * _referenceImageMeasure / _camera.orthographicSize;
        ((RectTransform)_image.transform).sizeDelta = new Vector2 (measure, measure);
        // fixing image position to Luke's position
        Vector3 relativePositionOfLukeToCamera = _camera.transform.InverseTransformDirection(_luke.transform.position - _camera.transform.position);
        float multiplier = 952.0f / 15.0f;
        multiplier = multiplier * _referenceCameraSize / _camera.orthographicSize;
        multiplier = multiplier * _canvas.pixelRect.width / 1920f;
        _image.transform.position = new Vector3(multiplier * relativePositionOfLukeToCamera.x + _canvas.pixelRect.width / 2,multiplier * relativePositionOfLukeToCamera.y + _canvas.pixelRect.height / 2,_image.transform.position.z);
        // fixing image position so that is located in the place of the actions
        float imageLenght = _image.rectTransform.rect.width;
        _image.transform.position = new Vector3(_image.transform.position.x + imageLenght / 2, _image.transform.position.y,
            _image.transform.position.z);
        // cloning the image
        ActionsManager actionsManager = _luke.GetComponent<ActionsManager>();
        int numberOfImages = actionsManager.GetActionSequence().Length;
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
