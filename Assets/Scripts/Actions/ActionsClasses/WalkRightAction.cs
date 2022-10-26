using UnityEngine;

namespace DLLF
{
    public class WalkRightAction : AbstractContinuousAction
    {       
        [Tooltip("Walking speed in unit/second")]
        [Range(0.001f, 5f)]
        private float _speed;
        public WalkRightAction(ActionParameters actionParameters, CharacterController characterController)
            : base(characterController)
        {
            _speed = actionParameters.WalkSpeed;
        }
        public override void Invoke()
        {
            
            CharacterController.Walk(_speed, Vector3.right);
        }

        public override bool IsHorizontal()
        {
            return true;
        }

        public override ActionType GetActionType()
        {
            return ActionType.WalkRight;
        }
    }
    
}