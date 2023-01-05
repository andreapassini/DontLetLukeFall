using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPath : MonoBehaviour
{
    [SerializeField]
    private List<Vector2> _path;
    private float _time = 0;
    private int _actualTarget=0;
    private int _nextTarget=1;
    private Vector2 _offset;
    [SerializeField]
    private float _speed = 0.25f;
    [SerializeField]
    private float _waitingTime = 0;
    private float _timer = 0;
    private void Awake()
    {
        _timer = _waitingTime;
        _offset = transform.position - (Vector3)_path[0];
    }
    // Update is called once per frame
    void Update()
    {

        _time += Time.deltaTime * _speed * _speed / Vector2.Distance(_path[_actualTarget],_path[_nextTarget]);
        if (_time < 1)
        {
            transform.position = Vector3.Lerp(_path[_actualTarget] + _offset, _path[_nextTarget] + _offset, _time);
        }
        else
        {
            if (_waitingTime <= 0)
            {
                _waitingTime = _timer;
                _time = 0;
                _actualTarget = _actualTarget != _path.Count - 1 ? _actualTarget + 1 : 0;
                _nextTarget = _nextTarget != _path.Count - 1 ? _nextTarget + 1 : 0;
            }
            else
            {
                _waitingTime -= Time.deltaTime;
                Debug.Log(_waitingTime);
            }
        }
        
    }
}
