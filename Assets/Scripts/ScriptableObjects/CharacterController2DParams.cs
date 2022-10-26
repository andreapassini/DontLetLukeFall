using UnityEngine;

namespace DLLF
{
    [CreateAssetMenu(fileName = "CharacterControllerParams", menuName = "Character/ControllerParams", order = 1)]
    public class CharacterController2DParams : ScriptableObject
    {
        [Header("Jump")]
        [Tooltip("The intensity of the force applied to perform the jump")]
        [Range(0.001f, 1000f)]
        [SerializeField] private float _jumpIntensity;
                
        [Tooltip("Angle between horizontal movement direction and jump direction in degrees")]
        [Range(0.001f, 180.0f)]
        [SerializeField] private float _jumpAngle = 90f;
        
        [Header("Walk")]
        [Tooltip("Walking speed in unit/second")]
        [Range(0.001f, 5f)]
        [SerializeField] private float _walkSpeed;
        

        public float JumpIntensity => _jumpIntensity;
        public float JumpAngle => _jumpAngle;

        public float WalkSpeed => _walkSpeed;
    }
}