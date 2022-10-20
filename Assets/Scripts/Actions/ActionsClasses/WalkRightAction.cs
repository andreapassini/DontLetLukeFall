using UnityEngine;

namespace DLLF
{
    public class WalkRightAction : AbstractAction
    {       
        [Tooltip("Walking speed in unit/second")]
        [Range(0.001f, 5f)]
        private float _speed;
        public WalkRightAction(ActionParameters actionParameters)
        {
            _speed = actionParameters.WalkSpeed;
        }
        public override void Invoke()
        {
            CharacterController.Walk(_speed);
        }

        public override ActionType GetActionType()
        {
            return ActionType.WalkRight;
        }
    }
    
}