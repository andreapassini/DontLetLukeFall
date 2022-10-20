using UnityEngine;

namespace DLLF
{
    public class WalkLeftAction : AbstractAction
    {

        private float _speed;
        public WalkLeftAction(ActionParameters actionParameters)
        {
            _speed = actionParameters.WalkSpeed;
        }
        
        public override void Invoke()
        {
            CharacterController.Walk(-1 * _speed);
        }

        public override ActionType GetActionType()
        {
            return ActionType.WalkLeft;
        }
    }
}