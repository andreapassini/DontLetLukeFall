using System;
using System.Collections;
using UnityEngine;

namespace DLLF
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class CharacterController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        }

        public void Walk(float speed, Vector3 direction)
        {
        }

        public void Jump(float intensity, float jumpAngle)
        {
        }
        
    }
}