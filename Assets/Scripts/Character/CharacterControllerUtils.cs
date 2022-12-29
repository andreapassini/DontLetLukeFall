using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLLF {
    public struct FrameInput {
        public float X;
        public bool JumpDown;
        public bool JumpUp;
    }
    

        
    public struct MovementRequest
    {
        public float Speed { get; set; }
        public bool Jump { get; set; }
        public int UnitsToJump { get; set; }
        public bool Crouch { get; set; }
    }

    public interface IPlayerController {
        public Vector3 Velocity { get; }
        public MovementRequest MovementRequest { get; }
        public bool JumpingThisFrame { get; }
        public bool LandingThisFrame { get; }
        public Vector3 RawMovement { get; }
        public bool Grounded { get; }
    }

    public struct RayRange {
        public RayRange(float x1, float y1, float x2, float y2, Vector2 dir) {
            Start = new Vector2(x1, y1);
            End = new Vector2(x2, y2);
            Dir = dir;
        }

        public readonly Vector2 Start, End, Dir;
    }
}