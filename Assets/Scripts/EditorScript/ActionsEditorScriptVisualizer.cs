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
    
    private void Awake()
    {
        float measure = _referenceCameraSize * _referenceImageMeasure / _camera.orthographicSize;
        ((RectTransform)_image.transform).sizeDelta = new Vector2 (measure, measure);
        // Now I have fixed the image dimensions
        // Now I fix the image position to Luke's position
        _image.transform.position = new Vector3(0,0,0);
        Debug.Log("rrr" + _canvas.pixelRect.width);
        float canvasWidth = _canvas.pixelRect.width;
        float canvasHeight = _canvas.pixelRect.height;
        float cameraWidth = _camera.sensorSize.x;
        float cameraHeight = _camera.sensorSize.y;
        Debug.Log("rrrrrr" + cameraWidth);
        Vector3 relativePositionOfLukeToCamera = _camera.transform.InverseTransformDirection(_luke.transform.position - _camera.transform.position);
    }
    
}
