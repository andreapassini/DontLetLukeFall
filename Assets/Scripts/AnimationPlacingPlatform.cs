using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlacingPlatform : MonoBehaviour
{
    
    private float _timer = 0.0f;
    private bool _needToExecuteAnimation = false;
    private float _startScaleValue = 2.0f;
    private float _speed = 1.25f;

    void Start()
    {
        ExecuteAnimation();//TEST
    }

    void Update()
    {
        if (_needToExecuteAnimation)
        {
            _timer += (Time.deltaTime * _speed);
            float val = _startScaleValue - _timer;
            if (val < 1.0f)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                _needToExecuteAnimation = false;
            }
            else
            {
                gameObject.transform.localScale = new Vector3(val, val, val);
            }
        }
    }

    public void ExecuteAnimation()
    {
        gameObject.transform.localScale = new Vector3(_startScaleValue, _startScaleValue, _startScaleValue);
        _timer = 0.0f;
        _needToExecuteAnimation = true;
    }
    
}
