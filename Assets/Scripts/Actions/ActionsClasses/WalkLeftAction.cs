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
            throw new System.NotImplementedException();
        }
        
    }
}