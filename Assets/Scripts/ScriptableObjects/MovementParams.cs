using UnityEngine;

namespace DLLF
{
    [CreateAssetMenu(fileName = "CharacterControllerParams", menuName = "Character/ControllerParams", order = 1)]
    public class MovementParams : ScriptableObject
    {

        [Header("Walk")]
        [Tooltip("Walking speed in unit/second")]
        [Range(0.001f, 5f)]
        [SerializeField] private float _walkSpeed;

        [Header("Run")]
        [Tooltip("The increment of speed while running (multiplier relative to walk speed)")]
        [Range(1f, 10f)]
        [SerializeField]
        private float _runIncrement;

        [Header("Crouch")]
        [Tooltip("The decrement of speed while crouching (multiplier relative to walk speed)")]
        [Range(0.001f, 1f)]
        [SerializeField]
        private float _crouchDecrement;



        public float WalkSpeed => _walkSpeed;

        public float RunSpeed => _walkSpeed * _runIncrement;

        public float CrouchDecrement => _crouchDecrement;
    }
}