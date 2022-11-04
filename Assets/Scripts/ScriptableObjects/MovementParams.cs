using UnityEditor.UIElements;
using UnityEngine;

namespace DLLF
{
    [CreateAssetMenu(fileName = "CharacterControllerParams", menuName = "Character/ControllerParams", order = 1)]
    public class MovementParams : ScriptableObject
    {

        [Header("General")]
        [Tooltip("The amount of Unity's unit that the actions will cover")]
        [Range(1, 10)]
        [SerializeField]
        private int unitsCoveredPerAction = 3;
        
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

        [Header("Jump")]
        [Tooltip("The covered units multiplier for the jump while running")]
        [Range(1, 10)]
        [SerializeField]
        private int _runningJumpMultiplier = 2;

        
        public float WalkSpeed => _walkSpeed;

        public float RunSpeed => _walkSpeed * _runIncrement;

        public float CrouchDecrement => _crouchDecrement;

        public int UnitsCoveredPerAction => unitsCoveredPerAction;

        public int RunningJumpUnitsCovered => unitsCoveredPerAction * _runningJumpMultiplier;

        
        // method to get proper units to cover with the jump given the running/walking state
        public int GetUnitToCoverForJump(bool isRunning)
        {
            return isRunning ? this.RunningJumpUnitsCovered : this.UnitsCoveredPerAction;
        }

    }
}