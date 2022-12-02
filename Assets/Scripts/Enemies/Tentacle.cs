using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Tentacle : MonoBehaviour
{
    [SerializeField] private int _lenght;
    [SerializeField] private LineRenderer _lineRenderer;
    private Vector3[] _segmentPoses;

    [SerializeField] private Transform _targetDir;
    [SerializeField] private float _targetDist;

    private Vector3[] _segmentV;
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private float _trailSpeed;

    [Tooltip("Speed to wiggle the scarf")]
    [SerializeField] private float _wiggleSpeed;
    [Tooltip("Wiggle magnitude of the scarf")]
    [SerializeField] private float _wiggleMagnitude;

    [SerializeField] private Transform _wiggleDir;

    [SerializeField] private float _startDealy = 0f;
    private bool _activateTentacle;

    //Positions of the points composing the LineRender of the scarf adjusted with transactions and with the wiggle
    private Vector3[] _realSegmentPoses;


    // Start is called before the first frame update
    void Start()
    {
        _activateTentacle = false;

        _lineRenderer ??= transform.GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _lenght;
        _segmentPoses = new Vector3[_lenght];
        _segmentV = new Vector3[_lenght];

        _realSegmentPoses = new Vector3[_lenght];

        ResetPos();

        StartCoroutine(DelayStart(_startDealy));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_activateTentacle)
            return;

        _segmentPoses[0] = _targetDir.position;

        for (int i = 1; i < _segmentPoses.Length; i++)
        {
            _segmentPoses[i] = Vector3.SmoothDamp(_segmentPoses[i], _segmentPoses[i - 1] + _targetDir.right * _targetDist, ref _segmentV[i], _smoothSpeed + i / _trailSpeed);
        }

        Wiggle();

        _lineRenderer.SetPositions(_segmentPoses);
    }

    private void ResetPos()
    {
        _lineRenderer.enabled = false;

        _segmentPoses[0] = _targetDir.position;
        for (int i = 1; i < _lenght; i++)
        {
            _segmentPoses[i] = _segmentPoses[i - 1] + _targetDir.right * _targetDist;

        }

        _lineRenderer.SetPositions(_segmentPoses);
    }

    private void Wiggle()
    {
        _wiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * _wiggleSpeed) * _wiggleMagnitude);
    }

    private IEnumerator DelayStart(float delay)
    {
        yield return new WaitForSeconds(delay);

        _lineRenderer.enabled = true;
        _activateTentacle = true;
    }
}
