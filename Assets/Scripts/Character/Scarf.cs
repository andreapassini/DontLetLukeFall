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
    [Tooltip("The more is low the most the scarf is short")]
    [SerializeField] private float _smoothSpeed;
    
    [Tooltip("Speed to wiggle the scarf")]
    [SerializeField] private float _wiggleSpeed;
    [Tooltip("Wiggle magnitude of the scarf")]
    [SerializeField] private float _wiggleMagnitude;
    [Tooltip("Wiggle magnitude PICK of the scarf")]
    [SerializeField] private float _wiggleMagnitudePick;
    [Tooltip("Wiggle PICK Duration of the scarf")]
    [SerializeField] private float _wiggleMagnitudePickDuration;
    [Tooltip("Wiggle PICK Smooth Step")]
    [SerializeField] private float _wigglePickSmoothStep;
    [Tooltip("The more is big the most you have waves when scarf is wiggling")]
    [SerializeField] private float _phaseMultiplier;
    [Tooltip("Wiggle Phase Pick")]
    [SerializeField] private float _phaseMultiplierPick;

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
    
    void Start()
    {
        _lineRend.positionCount = _lenght; //Setting the lenght of the LineRender
        _segmentPoses = new Vector3[_lenght];
        _realSegmentPoses = new Vector3[_lenght];
        _segmentV = new Vector3[_lenght];
        _lineRendTransform.transform.rotation = Quaternion.Euler(0,0,-90); //Normally the scarf will be in bottom (when Luke is stopped)
        _prevPos = _targetDir.transform.position; //starting position of Luke

        StartCoroutine(MagnitudeVariator());
    }

    void Update()
    {
        _segmentPoses[0] = _targetDir.position; // setting the first position of the lineRender of the scarf
        for (int i = 1; i < _segmentPoses.Length; i++) // setting the other positions of the lineRender of the scarf
        {
            _segmentPoses[i] = Vector3.SmoothDamp(_segmentPoses[i], _segmentPoses[i - 1] + _targetDir.right * _targetDist, ref _segmentV[i], _smoothSpeed);
        }
        for (int i = _segmentPoses.Length - 1; i >= 0; i--)
        {
            float value = (float)(i+1) / (float)(_segmentPoses.Length); //value to adjust the position of the single points of the scarf based on the wind
            _realSegmentPoses[i] = _segmentPoses[i];
            float phase = _phaseMultiplier * i; //value witch determines the number of waves on the scarf due to the wind
            _realSegmentPoses[i].y = _segmentPoses[i].y + value * Mathf.Sin(Time.time * _wiggleSpeed + phase) * _wiggleMagnitude * _adjustment;
            _realSegmentPoses[i].y += _scarfYAttachment; //scarf adjustment Y position (based on the scarf attachment position)
            if (_segmentPoses[1].x < _segmentPoses[0].x) //scarf adjustment X position (based on witch direction Luke is going)
            {
                _realSegmentPoses[i].x -= _scarfDistXFromCenter;
            }
            else
            {
                _realSegmentPoses[i].x += _scarfDistXFromCenter;
            }
            if (_crouched)
            {
                _realSegmentPoses[i].y -= _scarfYCrouched; //scarf adjustment Y position in case Luke is crouched
            }
        }
        _lineRend.SetPositions(_realSegmentPoses);
    }

    void FixedUpdate()
    {
        Vector3 newPos = _targetDir.transform.position;
        float distanceX = Mathf.Abs(newPos.x - _prevPos.x);
        _adjustment = (float)(distanceX) * (float)(Time.fixedDeltaTime) * 500.0f; //adjustment of waves's magnitude due to Luke's velocity
        _prevPos = newPos;
    }

    public void SetCrouched(bool crouched) //Method to set if Luke is crouched or not and adjust the Y position of the scarf
    {
        this._crouched = crouched;
    }
    
    private IEnumerator MagnitudeVariator()
	{
        float previousMagnitude = _wiggleMagnitude;
        float previousPhaseMultiplier = _phaseMultiplier;
        System.Random random = new System.Random();

        while (true) {

            // magnitude pick
            _wiggleMagnitude += _wiggleMagnitudePick;
            _phaseMultiplier += _phaseMultiplierPick;

            // Reset pick
            StartCoroutine(RestPick(_wiggleMagnitudePickDuration, previousMagnitude, previousPhaseMultiplier));

            yield return new WaitForSeconds(random.Next(2, 4));
        }
	}

    private IEnumerator RestPick(float timer, float prevMag, float prevPhase)
	{
        yield return new WaitForSeconds(timer);

        Debug.Log("Resetting Pick");

        for(int i = 0; i < _wigglePickSmoothStep; i++) {
            _wiggleMagnitude = Mathf.Lerp(_wiggleMagnitude, prevMag, i / _wigglePickSmoothStep);
            _phaseMultiplier = Mathf.Lerp(_phaseMultiplier, prevPhase, i / _wigglePickSmoothStep);

            yield return new WaitForSeconds(timer/_wigglePickSmoothStep);
        }

        //_wiggleMagnitude = prev;
    }
}
