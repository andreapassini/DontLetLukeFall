using UnityEngine;

namespace DLLF
{
    public class JumpUpAction : AbstractAction
    {
        [Tooltip("The intensity of the force applied to perform the jump")]
        [Range(0.001f, 100f)]
        private float _intensity;
        
        [Tooltip("Angle between X-axis and jump direction in degrees")]
        [Range(0.001f, 180.0f)]
        private float _jumpAngle;
        
        public JumpUpAction(ActionParameters actionParameters)
        {
            _intensity = actionParameters.JumpIntensity;
            _jumpAngle = actionParameters.JumpAngle;
        }
        
        public override void Invoke()
        {
            CharacterController.Jump(_intensity, _jumpAngle);
        }

        public override ActionType GetActionType()
        {
            return ActionType.JumpUp;
        }
    }
}