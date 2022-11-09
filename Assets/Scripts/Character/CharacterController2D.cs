using System;
using UnityEngine;
using UnityEngine.Events;

namespace DLLF
{
    public class CharacterController2D : MonoBehaviour
    {
        [SerializeField] private bool m_AirControl = false; // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround; // A mask determining what is ground to the character

        [SerializeField]
        private Transform m_GroundCheck; // A position marking where to check if the player is grounded.

        [SerializeField] private Transform m_CeilingCheck; // A position marking where to check for ceilings
        [SerializeField] private Collider2D m_CrouchDisableCollider; // A collider that will be disabled when crouching

        const float k_GroundedRadius = .4f; // Radius of the overlap circle to determine if grounded
        [SerializeField] private bool m_Grounded = true; // Whether or not the player is grounded.
        const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true; // For determining which way the player is currently facing.
        private Vector3 m_Velocity = Vector3.zero;

        private float _gravityScale;

        [Header("Events")] [Space] public UnityEvent OnLandEvent;

        [System.Serializable]
        public class BoolEvent : UnityEvent<bool>
        {
        }

        public BoolEvent OnCrouchEvent;
        [SerializeField] private bool m_wasCrouching = false;

        private void Awake()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            _gravityScale = m_Rigidbody2D.gravityScale;
            if (m_Grounded) m_Rigidbody2D.gravityScale = 0f;
            else m_Rigidbody2D.gravityScale = _gravityScale;
            if (OnLandEvent == null)
                OnLandEvent = new UnityEvent();

            if (OnCrouchEvent == null)
                OnCrouchEvent = new BoolEvent();
        }
        


        public void Move(ActionsManager.IMovementRequest movementRequest)
        {
            bool mustCrouch = false;
            // If crouching, check to see if the character can stand up
            if (!movementRequest.Crouch)
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    mustCrouch = true;
                }
            }

            float move = movementRequest.Speed;
            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // If crouching
                if (mustCrouch)
                {
                    if (!m_wasCrouching)
                    {
                        m_wasCrouching = true;
                        OnCrouchEvent.Invoke(true);
                    }

                    // Reduce the speed by the crouchSpeed multiplier
                    move *= movementRequest.CrouchMultiplier;

                    // Disable one of the colliders when crouching
                    if (m_CrouchDisableCollider != null)
                        m_CrouchDisableCollider.enabled = false;
                }
                else
                {
                    // Enable the collider when not crouching
                    if (m_CrouchDisableCollider != null)
                        m_CrouchDisableCollider.enabled = true;

                    if (m_wasCrouching)
                    {
                        m_wasCrouching = false;
                        OnCrouchEvent.Invoke(false);
                    }
                }

                m_Rigidbody2D.velocity = new Vector2(move, m_Rigidbody2D.velocity.y);
                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }

            // If the player should jump...
            if (m_Grounded && movementRequest.Jump)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Rigidbody2D.gravityScale = _gravityScale;
                m_Rigidbody2D.AddForce(ComputeJumpForce(movementRequest.UnitsToJump, movementRequest.Speed));
            }
        }

        // compute the force necessary to let the character jump for jumpDuration seconds
        // y gravity is multiplied by minus one because it is already negative
        private Vector2 ComputeJumpForce(int unitsToCover, float currentSpeed)
        {
            float desiredYSpeed = (unitsToCover * (Physics2D.gravity.magnitude * m_Rigidbody2D.gravityScale)) / (2 * Mathf.Abs(currentSpeed));
            return new Vector2(0, m_Rigidbody2D.mass * (desiredYSpeed / Time.fixedDeltaTime));
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        /*private void FixedUpdate()
        {
            bool wasGrounded = m_Grounded;
            bool collided = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    Debug.Log("Collision with ground");
                    collided = true;
                    break;
                }
            }

            //i was on the ground but now i'm falling
            if (wasGrounded && !collided)
            {
                Debug.Log("Falling");
                m_Rigidbody2D.gravityScale = _gravityScale;
                m_Grounded = false;
                return;
            }

            //i was in air and i've collided, i've to land
            if (!wasGrounded && collided)
            {
                Debug.Log("Landing");
                m_Rigidbody2D.gravityScale = 0f;
                m_Grounded = true;
                return;

            }

        }*/

        //todo: da rifare usando LayerMask
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!m_Grounded && col.collider.gameObject.CompareTag("FixedPlatform"))
            {
                //landed
                Debug.Log("Collision enter with " + col.collider.gameObject.name);
                m_Grounded = true;
                m_Rigidbody2D.gravityScale = 0.0f;
            }
        }

        private void OnCollisionExit2D(Collision2D col)
        {
            if (m_Grounded && col.collider.gameObject.CompareTag("FixedPlatform"))
            {
                Debug.Log("Collision exit from " + col.collider.gameObject.name);
                m_Grounded = false;
                m_Rigidbody2D.gravityScale = _gravityScale;
            }

        }
    }
}