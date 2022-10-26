using UnityEngine;

namespace DLLF
{
    public class WalkLeftAction : AbstractContinuousAction
    {
        [Tooltip("Walking speed in unit/second")]
        [Range(0.001f, 5f)]
        private float _speed;
        
        public WalkLeftAction(ActionParameters actionParameters, CharacterController characterController) 
            : base(characterController)
        {
            _speed = actionParameters.WalkSpeed;
        }
        
        public override void Invoke()
        {
            CharacterController.Walk(_speed, Vector3.left);
        }

        public override bool IsHorizontal()
        {
            return true;
        }

        public override ActionType GetActionType()
        {
            return ActionType.WalkLeft;
        }
    }
}