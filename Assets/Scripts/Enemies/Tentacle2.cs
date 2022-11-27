using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle2 : MonoBehaviour
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
    [Tooltip("The more is big the most you have waves when scarf is wiggling")]
    [SerializeField] private float _phaseMultiplier;
    [SerializeField] private Transform _wiggleDir;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer ??= transform.GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _lenght;
        _segmentPoses = new Vector3[_lenght];
        _segmentV = new Vector3[_lenght];

        ResetPos();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _wiggleDir.localRotation = Quaternion.Euler(
            0,
            0,
            Mathf.Sin(Time.time * _wiggleSpeed) * _wiggleMagnitude);

        _segmentPoses[0] = _targetDir.position;

        for (int i = 1; i < _segmentPoses.Length; i++)
        {
            Vector3 targetPos = _segmentPoses[i - 1]
                + (_segmentPoses[i] - _segmentPoses[i - 1])
                .normalized * _targetDist;
            _segmentPoses[i] = Vector3.SmoothDamp(
                _segmentPoses[i],
                targetPos,
                ref _segmentV[i],
                _smoothSpeed);
        }

        _lineRenderer.SetPositions(_segmentPoses);
    }

    private void ResetPos()
    {
        _segmentPoses[0] = _targetDir.position;
        for(int i = 1; i < _lenght; i++)
        {
            _segmentPoses[i] = _segmentPoses[i - 1] + _targetDir.right * _targetDist;

        }

        _lineRenderer.SetPositions(_segmentPoses);
    }
}
