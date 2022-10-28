using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLLF
{
    public delegate void ActionDelegate();

    public class ActionsManager : MonoBehaviour
    {
        [SerializeField] private MovementParams movementParams;

        [SerializeField] private ActionsSequence _actionsTypeSequence;
        [SerializeField] private ActionsSprites _actionsSprites;

        [SerializeField] private float _actionsDuration;
        [SerializeField] private CharacterController2D _characterController;
        [SerializeField] private ActionUIController _actionUIController;


        private bool _jump;
        private float _speed;

        // TODO: use a bool to know when a platform from an action interrupts the current action
        // bool that indicates whether the ActionManager has been interrupted during a time-bounded action
        // private bool _interrupted;


        private Queue<ActionType> _actionsSequence = new Queue<ActionType>();
        private Dictionary<ActionType, ActionDelegate> _actionsMapping;

        private void Awake()
        {
            foreach (var actionType in _actionsTypeSequence.actions)
            {
                _actionsSequence.Enqueue(actionType);
            }

            _actionsMapping = new Dictionary<ActionType, ActionDelegate>();
            _actionsMapping[ActionType.Jump] = Jump;
            _actionsMapping[ActionType.WalkRight] = WalkRight;
            _actionsMapping[ActionType.WalkLeft] = WalkLeft;
            _actionsMapping[ActionType.RunRight] = RunRight;
            _actionsMapping[ActionType.RunLeft] = RunLeft;
        }

        void Start()
        {
            StartCoroutine(StartActionSequence());
        }

        private void FixedUpdate()
        {
            var movementRequest = new MovementRequest
            {
                Jump = _jump,
                Speed = _speed,
                JumpDuration = _actionsDuration,
                CrouchMultiplier = movementParams.CrouchDecrement
            };
            _characterController.Move(movementRequest);
            if (_jump) _jump = false;
        }

        private IEnumerator StartActionSequence()
        {
            while (_actionsSequence.TryDequeue(out var actionToPerform))
            {
                ActionDelegate actionDelegate = _actionsMapping[actionToPerform];
                //_actionUIController.AddActionSprite(_actionsSprites.GetSprite(actionToPerform));
                actionDelegate.Invoke();
                yield return new WaitForSeconds(_actionsDuration);
            }
        }

        private void Jump()
        {
            //activate jump
            Debug.Log("Activating jump");
            _jump = true;
        }

        private void WalkRight()
        {
            Debug.Log("Activating WR");
            _speed = movementParams.WalkSpeed;
        }
        
        
        private void WalkLeft()
        {
            Debug.Log("Activating WL");
            _speed = -movementParams.WalkSpeed;
        }
        
        //TODO: aumentare gradualmente la velocità (?)
        // Time-bounded action that lets the player run to the right
        // Increase speed to RunSpeed value for X seconds
        // at the end, fallback to WalkRight action
        private void RunRight()
        {
            Debug.Log("Activating RunRight");
            _speed = movementParams.RunSpeed;
            StartCoroutine(FallBackActionCoroutine(WalkRight));
        }
        
        // Time-bounded action that lets the player run to the left
        // Increase speed to RunSpeed value for X seconds
        // at the end, fallback to WalkLeft action
        private void RunLeft()
        {
            Debug.Log("Activating RunLeft");
            _speed = -movementParams.RunSpeed;
            StartCoroutine(FallBackActionCoroutine(WalkLeft));
        }

        private IEnumerator FallBackActionCoroutine(ActionDelegate fallbackAction)
        {
            yield return new WaitForSeconds(_actionsDuration);
            fallbackAction.Invoke();
        }

        
        public interface IMovementRequest
        {
            public float Speed { get; }
            public bool Jump { get; }
            public float JumpDuration { get; }
            public bool Crouch { get; }
            public float CrouchDamping { get; }
            public float CrouchMultiplier { get; }
        }
        
        private struct MovementRequest : IMovementRequest
        {
            public float Speed { get; set; }
            public bool Jump { get; set; }
            public float JumpDuration { get; set; }
            public bool Crouch { get; set; }
            public float CrouchDamping { get; set; }
            public float CrouchMultiplier { get; set; }
        }

    }
    
    


}