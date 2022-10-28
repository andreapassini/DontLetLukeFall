using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarf : MonoBehaviour
// This script is to manage the scarf of Luke
// To use this script you need: Luke witch is parent of the scarf witch is a LineRender and is itself parent of TargetDir witch is an empty gameObject with the same position of Luke
{

    [Tooltip("The number of point of the LineRender to render the scarf")]
    [SerializeField] private int _lenght;
    [Tooltip("The LineRenderer of the scarf")]
    [SerializeField] private LineRenderer _lineRend;
    [Tooltip("The Transform of the scarf")]
    [SerializeField] private Transform _lineRendTransform;

    [Tooltip("An empty gameObject in the same position of Luke")]
    [SerializeField] private Transform _targetDir;
    [Tooltip("The distance between different points of the LineRender of the scarf")]
    [SerializeField] private float _targetDist;
    [Tooltip("The more is low the most the scarp is short")]
    [SerializeField] private float _smoothSpeed;
    
    [Tooltip("Speed to wiggle the scarf")]
    [SerializeField] private float _wiggleSpeed;
    [Tooltip("Wiggle magnitude of the scarf")]
    [SerializeField] private float _wiggleMagnitude;
    [Tooltip("The more is big the most you have waves when scarf is wiggling")]
    [SerializeField] private float _phaseMultiplier;

    [Tooltip("To adjust the Y pos to attach the scarf")]
    [SerializeField] private float _scarfYAttachment;
    [Tooltip("To adjust the X pos to attach the scarf on both symmetrical sides")]
    [SerializeField] private float _scarfDistXFromCenter;
    [Tooltip("To adjust even so the Y pos to attach the scarf when Luke is crouched")]
    [SerializeField] private float _scarfYCrouched;
    
    private Vector3[] _segmentPoses; //Positions of the points composing the LineRender of the scarf
    private Vector3[] _realSegmentPoses; //Positions of the points composing the LineRender of the scarf adjusted with transactions and with the wiggle
    private Vector3[] _segmentV; //Used to adjust the wiggle of the scarf
    private float _adjustment = 1f; //Adjustment of the magnitude of the scarf based on Luke velocity
    private Vector3 _prevPos; //The previous position of luke on the previous fixedUpdate
    private bool _crouched = false; //To set if Luke is crouched or not
    
    // Start is called before the first frame update
    void Start()
    {
        _lineRend.positionCount = _lenght;
        _segmentPoses = new Vector3[_lenght];
        _realSegmentPoses = new Vector3[_lenght];
        _segmentV = new Vector3[_lenght];
        _lineRendTransform.transform.rotation = Quaternion.Euler(0,0,-90);
        _prevPos = _targetDir.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _segmentPoses[0] = _targetDir.position;
        for (int i = 1; i < _segmentPoses.Length; i++)
        {
            _segmentPoses[i] = Vector3.SmoothDamp(_segmentPoses[i], _segmentPoses[i - 1] + _targetDir.right * _targetDist, ref _segmentV[i], _smoothSpeed);
        }
        for (int i = _segmentPoses.Length - 1; i >= 0; i--)
        {
            float value = (float)(i+1) / (float)(_segmentPoses.Length);
            _realSegmentPoses[i] = _segmentPoses[i];
            float phase = _phaseMultiplier * i;
            _realSegmentPoses[i].y = _segmentPoses[i].y + value * Mathf.Sin(Time.time * _wiggleSpeed + phase) * _wiggleMagnitude * _adjustment;
            _realSegmentPoses[i].y += _scarfYAttachment;
            if (_segmentPoses[1].x < _segmentPoses[0].x)
            {
                _realSegmentPoses[i].x -= _scarfDistXFromCenter;
            }
            else
            {
                _realSegmentPoses[i].x += _scarfDistXFromCenter;
            }
            if (_crouched)
            {
                _realSegmentPoses[i].y -= _scarfYCrouched;
            }
        }
        _lineRend.SetPositions(_realSegmentPoses);
    }

    void FixedUpdate()
    {
        Vector3 newPos = _targetDir.transform.position;
        float distanceX = Mathf.Abs(newPos.x - _prevPos.x);
        _adjustment = (float)(distanceX) * (float)(Time.fixedDeltaTime) * 500.0f;
        _prevPos = newPos;
    }

    public void SetCrouched(bool crouched)
    {
        this._crouched = crouched;
    }
    
}
