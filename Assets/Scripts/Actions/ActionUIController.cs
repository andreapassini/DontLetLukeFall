using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionUIController : MonoBehaviour
{
    
    [SerializeField] private Image[] _displayActionImage;
    //this array is initialized with 5 elements (Image) via inspector
    //Each Image represent an action

    public void AddActionSprite(Sprite imageActionSprite)
    //Add an action Sprite (image) and shift right the others
    {
        for (int i = _displayActionImage.Length; i > 1; i--)
        {
            _displayActionImage[i - 1].sprite = _displayActionImage[i - 2].sprite;
        }
        _displayActionImage[0].sprite = imageActionSprite;
    }
}
