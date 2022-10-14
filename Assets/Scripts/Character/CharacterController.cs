﻿using System;
using System.Collections;
using UnityEngine;

namespace DLLF
{
    //incomplete class just to have the signature 
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class CharacterController : MonoBehaviour
    {
        private bool _DEBUG_walk;
        private bool _DEBUG_jump;
        
        private Rigidbody2D _rigidbody;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        }

        public void Walk(float speed)
        {
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
            _rigidbody.MovePosition(transform.position + speed * Time.deltaTime * Vector3.right);
        }

        public void Jump(float intensity, float jumpAngle)
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            Vector2 forceDirection = (Quaternion.AngleAxis(jumpAngle, Vector3.forward) * Vector2.right).normalized;
            _rigidbody.AddForce(forceDirection.normalized * intensity, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            //is descending
            var dot = Vector2.Dot(_rigidbody.velocity.normalized, Vector2.up);
            if (dot < 0.01f)
            {
                _rigidbody.bodyType = RigidbodyType2D.Kinematic;
                _rigidbody.velocity = Vector2.zero;
            }
        }
    }
}