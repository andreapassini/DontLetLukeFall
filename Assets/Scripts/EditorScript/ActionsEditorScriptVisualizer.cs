using System;
using System.Collections;
using System.Collections.Generic;
using DLLF;
using UnityEngine;
using UnityEngine.UI;

public class ActionsEditorScriptVisualizer : MonoBehaviour
// This script is to visualize actions in the editor (editor script) while creating levels
{

    [SerializeField] private GameObject _luke; // Luke
    [SerializeField] private Canvas _canvas; // The canvas
    [SerializeField] private Camera _camera; // The main camera
    [SerializeField] private ActionsSprites _actionsSprites; // The action sprites
    [SerializeField] private Image _image; // The image to be cloned to visualize the other actions
    
    [SerializeField] private List<Image> _instantiatedImages; // This list should be left empty at the beginning
    // This list will contain images of actions to visualize
    
    private const float _referenceImageMeasure = 5f; // to fix image dimensions based on dimension of a platform
    private const float _referenceCanvasWidth = 1920f; // to fix image dimensions based on camera size

    public void RemoveActions()
    {
        // (re)enable the image to initial params
        _image.enabled = true;
        _image.transform.position = new Vector3(_canvas.pixelRect.width / 2, _canvas.pixelRect.height / 2, 0);
        ((RectTransform)_image.transform).sizeDelta = new Vector2 (100, 100);
        // remove images of actions you used before
        foreach (var el in _instantiatedImages)
        {
            SafeDestroyGameObject(el);
        }
        _instantiatedImages.Clear();
    }
    
    public void VisualizeActions()
    {
        // (re)enable the image to initial params and reset images of actions you used before
        RemoveActions();
        // fixing image dimensions
        float measure = _referenceImageMeasure * _referenceCanvasWidth / _canvas.pixelRect.width;
        ((RectTransform)_image.transform).sizeDelta = new Vector2 (measure, measure);
        // fixing image position to Luke's position
        _image.transform.position = new Vector3(0, 0,
            _image.transform.position.z);
        Vector3 relativePositionOfLukeToCamera = _camera.transform.InverseTransformDirection(_luke.transform.position - _camera.transform.position);
        _image.transform.position = new Vector3( relativePositionOfLukeToCamera.x + _image.transform.position.x, relativePositionOfLukeToCamera.y + _image.transform.position.y, _image.transform.position.z);
        // fixing image position so that is located in the place of the actions
        float imageLenght = _image.rectTransform.rect.width;
        _image.transform.position = new Vector3(_image.transform.position.x + imageLenght / 2, _image.transform.position.y, _image.transform.position.z);
        // cloning the image
        ActionsManager actionsManager = _luke.GetComponent<ActionsManager>();
        foreach (var el in actionsManager.GetActionSequence())
        {
            Image newImage = Instantiate(_image, _image.transform.position, _image.transform.rotation, gameObject.transform);
            _image.rectTransform.position = new Vector3(_image.rectTransform.position.x + imageLenght,
                _image.rectTransform.position.y, _image.rectTransform.position.z);
            newImage.sprite = _actionsSprites.GetSprite(el);
            _instantiatedImages.Add(newImage);
        }
        _image.enabled = false;
    }
    
    private static T SafeDestroy<T>(T obj) where T : UnityEngine.Object
    {
        if (Application.isEditor)
            UnityEngine.Object.DestroyImmediate(obj);
        else
            UnityEngine.Object.Destroy(obj);
        return null;
    }
    private static T SafeDestroyGameObject<T>(T component) where T : Component // Function to destroy a gameObject in editor scripts
    {
        if (component != null)
            SafeDestroy(component.gameObject);
        return null;
    }

}
