using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

namespace DLLF
{
    public class ActionsFactory
    {
        private static readonly Dictionary<ActionType, Type> ActionTypeMapping;
        
        /*static constructor is called at most once before ANY other non-static constructor invokation
        or member access*/
        static ActionsFactory()
        {
            ActionTypeMapping = new Dictionary<ActionType, Type>
            {
            };
        }

        /// <summary>Method to create an action</summary>
        /// <param name="type">The type of the action to be created</param>
        /// <param name="actionParameters">A scriptable object with the actions' parameters</param>
        /// <param name="characterController">The character control whose action will refer to</param>
        /// <exception cref="ActionTypeNotLinkedToActionClass">This exception is thrown if the action type is not linked to a class</exception>
        public IAction CreateAction(ActionType type, [NotNull] ActionParameters actionParameters, CharacterController characterController)
        {
            Type actionType = ActionTypeMapping[type];
            if (actionType == null)
            {
                throw new ActionTypeNotLinkedToActionClass("Action type " + type.ToString() + " is not linked to any class");
            }
            object[] parameters = {actionParameters, characterController};
            IAction instance = (IAction)Activator.CreateInstance(actionType, args:parameters);
            return instance;
        }
    }
}