namespace DLLF
{
    [System.AttributeUsage(System.AttributeTargets.Method)  
    ]  
    public class TimeBoundedAction : System.Attribute
    {
        public TimeBoundedAction(ActionType actionType, ActionType fallbackActionType)
        {
            ActionType = actionType;
            FallbackActionType = fallbackActionType;
        }
        
        public ActionType ActionType { get;  }

        public ActionType FallbackActionType { get;  }
    }
    
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