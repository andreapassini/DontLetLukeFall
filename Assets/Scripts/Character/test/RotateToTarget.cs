using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTarget : MonoBehaviour
{

    public float rotationSpeed;
    private Vector2 _direction;

    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        _direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        Vector2 cursPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.MoveTowards(transform.position, cursPos, moveSpeed * Time.deltaTime);
    }
    
    private Vector3 _previousPosition;
    private bool _part = false;

    private void FixedUpdate()
    {
        Vector3 previousPosition = _previousPosition;
        Vector3 newPosition = transform.position;
        if (newPosition.x > previousPosition.x)
        {
            _part = false;
        } else if (previousPosition.x > newPosition.x)
        {
            _part = true;
        }
        if (_part)
        {
            //transform.rotation = Quaternion.Euler(0,180,0);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            //transform.rotation = Quaternion.Euler(0,0,0);
            transform.localScale = new Vector3(1, 1, 1);
        }
        _previousPosition = newPosition;
    }
}
