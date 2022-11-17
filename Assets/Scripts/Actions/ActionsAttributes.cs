namespace DLLF
{   
    [System.AttributeUsage(System.AttributeTargets.Method)  
    ]  
    public class ContinuousAction : System.Attribute
    {
        public ContinuousAction(ActionType actionType)
        {
            ActionType = actionType;
        }
        
        public ActionType ActionType { get; }
    }
    
    [System.AttributeUsage(System.AttributeTargets.Method)  
    ]  
    public class ImmediateAction : System.Attribute
    {
        public ImmediateAction(ActionType actionType)
        {
            ActionType = actionType;
        }
        
        public ActionType ActionType { get; }
    }
}