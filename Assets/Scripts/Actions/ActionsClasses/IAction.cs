using System;
using UnityEngine;

namespace DLLF
{
    public interface IAction
    {
        void Invoke();
        bool IsHorizontal();
        ActionType GetActionType();
    }

    public enum ActionType
    {
        JumpUp,
        WalkRight,
        WalkLeft
    }
}