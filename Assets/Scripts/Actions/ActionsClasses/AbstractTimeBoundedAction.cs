namespace DLLF
{
    public abstract class AbstractTimeBoundedAction : IAction
    {
        protected CharacterController CharacterController;

        protected AbstractTimeBoundedAction(CharacterController characterController)
        {
            CharacterController = characterController;
        }

        public abstract void Start();
        public abstract void End();
        public abstract bool IsHorizontal();
        public abstract ActionType GetActionType();

    }
}