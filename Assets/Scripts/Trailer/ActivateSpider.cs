using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpider : MonoBehaviour
{
    private Animator _animator;
    private bool _activated = false;
    private static readonly int _Spider = Animator.StringToHash("spider");

    void Start()
    {
        _animator = transform.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_activated)
        {
            _activated = true;
            _animator.SetTrigger(_Spider);
        }
    }
}
