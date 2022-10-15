using System;
using UnityEngine;

namespace DLLF
{
    public interface IAction
    {
        void Invoke();
        void SetController(CharacterController characterController);
    }

    public enum ActionType
    {
        JumpUp,
        WalkRight,
        WalkLeft
    }
}