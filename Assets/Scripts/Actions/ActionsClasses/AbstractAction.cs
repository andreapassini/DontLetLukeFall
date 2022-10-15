namespace DLLF
{
    public abstract class AbstractAction : IAction
    {
        protected CharacterController CharacterController;
        
        public abstract void Invoke();

        public void SetController(CharacterController characterController)
        {
            CharacterController = characterController;
        }
        
    }
}