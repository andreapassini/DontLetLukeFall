using System;
using UnityEngine;

namespace DLLF
{
    public interface IAction
    {
        void Start();
        void End();
        bool IsHorizontal();
        ActionType GetActionType();
    }

    public enum ActionType
    {
        Jump,
        WalkRight,
        WalkLeft,
        RunRight,
        RunLeft,
        Crouch
    }
}