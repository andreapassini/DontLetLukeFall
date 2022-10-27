using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLLF
{

    public struct MovementRequest
    {
        public float Speed;
        public bool Jump;
        public float JumpDuration;
        public bool Crouch;
        public float CrouchDamping;
    }
    
    public delegate void ActionDelegate();

    public class ActionsManager : MonoBehaviour
    {
        [SerializeField] private ActionsSequence _actionsTypeSequence;
        [SerializeField] private ActionsSprites _actionsSprites;

        [SerializeField] private float _actionsDuration;
        [SerializeField] private CharacterController2D _characterController;
        [SerializeField] private ActionUIController _actionUIController;


        private bool _jump;
        private float _speed;


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
                JumpDuration = _actionsDuration
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
            _speed = 1f;
        }
        
        
        private void WalkLeft()
        {
            Debug.Log("Activating WL");
            _speed = -1f;        
        }

    }


}