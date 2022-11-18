using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsEditorScriptVisualizer : MonoBehaviour
{

    [SerializeField] private GameObject _luke;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Camera _camera;
    [SerializeField] private Image _image;

    private const float _referenceCameraSize = 8.457275f;
    private const float _referenceImageMeasure = 321f;
    private const float _adapterCameraCanvas = 0.0005f;
    
    private void Awake()
    {
        // fixing image dimensions
        float measure = _referenceCameraSize * _referenceImageMeasure / _camera.orthographicSize;
        ((RectTransform)_image.transform).sizeDelta = new Vector2 (measure, measure);
        // Now I have fixed the image dimensions
        // fixing image position to Luke's position
        Vector3 relativePositionOfLukeToCamera = _camera.transform.InverseTransformDirection(_luke.transform.position - _camera.transform.position);
        float multiplier = 952.0f / 15.0f;
        multiplier = multiplier * _referenceCameraSize / _camera.orthographicSize;
        multiplier = multiplier * _canvas.pixelRect.width / 1920f;
        _image.transform.position = new Vector3(multiplier * relativePositionOfLukeToCamera.x + _canvas.pixelRect.width / 2,multiplier * relativePositionOfLukeToCamera.y + _canvas.pixelRect.height / 2,_image.transform.position.z);
    }

}
