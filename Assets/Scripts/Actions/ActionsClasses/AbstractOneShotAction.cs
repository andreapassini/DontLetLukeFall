namespace DLLF
{
    public abstract class AbstractOneShotAction : IAction
    {
        protected CharacterController CharacterController;

        protected AbstractOneShotAction(CharacterController characterController)
        {
            CharacterController = characterController;
        }

        protected bool Executed { get; private set; }

        public virtual void Invoke()
        {
            if (!Executed)
            {
                Executed = true;
            }
        }

        public abstract bool IsHorizontal();

        public void SetController(CharacterController characterController)
        {
            CharacterController = characterController;
        }

        public abstract ActionType GetActionType();
    }
}