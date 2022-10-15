using System;
using UnityEngine;

namespace DLLF
{
    public interface IAction
    {
        void Invoke();
    }

    public enum ActionType
    {
        JumpUp,
        WalkRight,
        WalkLeft
    }
}