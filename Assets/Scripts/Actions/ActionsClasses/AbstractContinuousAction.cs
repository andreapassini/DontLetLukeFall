namespace DLLF
{
    public abstract class AbstractContinuousAction : IAction
    {
        protected CharacterController CharacterController;

        protected AbstractContinuousAction(CharacterController characterController)
        {
            CharacterController = characterController;
        }

        public abstract void Invoke();
        public abstract bool IsHorizontal();

        public void SetController(CharacterController characterController)
        {
            CharacterController = characterController;
        }

        public abstract ActionType GetActionType();
    }
}