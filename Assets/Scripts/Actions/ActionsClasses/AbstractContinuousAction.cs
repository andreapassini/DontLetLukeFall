namespace DLLF
{
    public abstract class AbstractContinuousAction : IAction
    {
        
        protected CharacterController CharacterController;

        protected AbstractContinuousAction(CharacterController characterController)
        {
            CharacterController = characterController;
        }
        
        public abstract void Start();
        public void End()
        {
            //does nothing because it is a continuous action
        }

        public abstract bool IsHorizontal();
        public abstract ActionType GetActionType();
        
        
    }
}