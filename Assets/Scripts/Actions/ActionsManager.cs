using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

namespace DLLF
{
    // delegate that represents an action
    // action method will change parameters in order to perform actions and will return the time in seconds needed to
    // complete the action
    public delegate float ActionDelegate();

    public class ActionsManager : MonoBehaviour
    {

        [SerializeField] private MovementParams movementParams;

        [SerializeField] private ActionsSequence _actionsTypeSequence;
        [SerializeField] private ActionsSprites _actionsSprites;

        [SerializeField] private CharacterController2D _characterController;
        [SerializeField] private ActionUIController _actionUIController;

        private bool _jump;
        private float _speed;
        private bool _isRunning;

        private Queue<ActionType> _actionsSequence = new Queue<ActionType>();
        private Dictionary<ActionType, ActionDelegate> _actionsMapping;


        private List<Vector3> actionsPosition = new List<Vector3>();
        // DEBUG
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            foreach (var pos in actionsPosition)
            {
                Gizmos.DrawCube(pos, Vector3.one);
            }
        }

        private void Awake()
        {
            foreach (var actionType in _actionsTypeSequence.actions)
            {
                _actionsSequence.Enqueue(actionType);
            }

            _actionsMapping = new Dictionary<ActionType, ActionDelegate>();
            AutoLinkActionTypesToMethods();
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
                UnitsToJump = _jump ? movementParams.GetUnitToCoverForJump(_isRunning) : 0,
                Speed = _speed,
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
                float timeToComplete = actionDelegate.Invoke();
                actionsPosition.Add(transform.position);
                Debug.Log("Time to complete for action " + actionToPerform + " is  " + timeToComplete + " (current speed: " + _speed + ")");
                yield return new WaitForSeconds(timeToComplete);
            }
        }

        [ImmediateAction(ActionType.Jump)]
        private float Jump()
        {
            Debug.Log("Activating jump");
            _jump = true;
            // if it is running it will cover more units
            return GetTime(movementParams.GetUnitToCoverForJump(_isRunning), _speed);
        }

        [ContinuousAction(ActionType.WalkRight)]
        private float WalkRight()
        {
            Debug.Log("Activating WalkRight");
            _speed = movementParams.WalkSpeed;
            _isRunning = false;
            return GetTime(movementParams.UnitsCoveredPerAction, _speed);

        }
        
        
        [ContinuousAction(ActionType.WalkLeft)]
        private float WalkLeft()
        {
            Debug.Log("Activating WalkLeft");
            _speed = -movementParams.WalkSpeed;
            _isRunning = false;
            return GetTime(movementParams.UnitsCoveredPerAction, _speed);

        }
        
        // Continuous action that lets the player run to the right
        // Increase speed to RunSpeed value
        [ContinuousAction(ActionType.RunRight)]
        private float RunRight()
        {
            Debug.Log("Activating RunRight");
            _speed = movementParams.RunSpeed;
            _isRunning = true;
            return GetTime(movementParams.UnitsCoveredPerAction, _speed);

        }
        
        // Continuous action that lets the player run to the left
        // Increase speed to RunSpeed value
        [ContinuousAction(ActionType.RunLeft)]
        private float RunLeft()
        {
            Debug.Log("Activating RunLeft");
            _speed = -movementParams.RunSpeed;
            _isRunning = true;
            return GetTime(movementParams.UnitsCoveredPerAction, _speed);

        }
        
        // method to calculate time to cover the given space: spaceToCover, at a given speed: speed
        private float GetTime(float spaceToCover, float speed)
        {
            return Mathf.Abs(spaceToCover / speed);
        }


        public interface IMovementRequest
        {
            public float Speed { get; }
            public bool Jump { get; }
            public int UnitsToJump { get; set; }
            public bool Crouch { get; }
            public float CrouchMultiplier { get; }
        }
        
        private struct MovementRequest : IMovementRequest
        {
            public float Speed { get; set; }
            public bool Jump { get; set; }
            public int UnitsToJump { get; set; }
            public bool Crouch { get; set; }
            public float CrouchMultiplier { get; set; }
        }

        #region AutoSetupWithReflection

        private void AutoLinkActionTypesToMethods()
        {
            var methods = typeof(ActionsManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            Debug.Log(methods.Length);
            foreach (var method in methods)
            {
                Debug.Log(method.Name);
                foreach (var customAttribute in method.CustomAttributes)
                {
                    var attributeType = customAttribute.AttributeType;
                    if (attributeType == typeof(ImmediateAction) || attributeType == typeof(ContinuousAction))
                    {
                        Assert.IsNotNull(customAttribute.ConstructorArguments);
                        Assert.IsTrue(customAttribute.ConstructorArguments.Count > 0);
                        ActionType actionType = (ActionType) customAttribute.ConstructorArguments[0].Value;
                        ActionDelegate actionDelegate = (ActionDelegate) Delegate.CreateDelegate(typeof(ActionDelegate), this, method.Name, false);
                        _actionsMapping.Add(actionType, actionDelegate);
                    }
                }
            }
        }
        

        #endregion

    }
    
    


}