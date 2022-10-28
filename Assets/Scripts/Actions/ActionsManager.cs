using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

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
        private Dictionary<ActionType, ActionDelegate> _fallbackActionsMapping;

        private void Awake()
        {
            foreach (var actionType in _actionsTypeSequence.actions)
            {
                _actionsSequence.Enqueue(actionType);
            }

            _actionsMapping = new Dictionary<ActionType, ActionDelegate>();
            _fallbackActionsMapping = new Dictionary<ActionType, ActionDelegate>();
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

        [ImmediateAction(ActionType.Jump)]
        private void Jump()
        {
            //activate jump
            Debug.Log("Activating jump");
            _jump = true;
        }

        [ContinuousAction(ActionType.WalkRight)]
        private void WalkRight()
        {
            Debug.Log("Activating WR");
            _speed = movementParams.WalkSpeed;
        }
        
        
        [ContinuousAction(ActionType.WalkLeft)]
        private void WalkLeft()
        {
            Debug.Log("Activating WL");
            _speed = -movementParams.WalkSpeed;
        }
        
        //TODO: aumentare gradualmente la velocità (?)
        // Time-bounded action that lets the player run to the right
        // Increase speed to RunSpeed value for X seconds
        // at the end, fallback to WalkRight action
        [TimeBoundedAction(ActionType.RunRight, ActionType.WalkRight)]
        private void RunRight()
        {
            Debug.Log("Activating RunRight");
            _speed = movementParams.RunSpeed;
            StartCoroutine(FallBackActionCoroutine(ActionType.WalkRight));
        }
        
        // Time-bounded action that lets the player run to the left
        // Increase speed to RunSpeed value for X seconds
        // at the end, fallback to WalkLeft action
        [TimeBoundedAction(ActionType.RunLeft,ActionType.WalkLeft)]
        private void RunLeft()
        {
            Debug.Log("Activating RunLeft");
            _speed = -movementParams.RunSpeed;
            StartCoroutine(FallBackActionCoroutine(ActionType.WalkLeft));
        }

        private IEnumerator FallBackActionCoroutine(ActionType fallbackActionType)
        {
            yield return new WaitForSeconds(_actionsDuration);
            _actionsMapping[fallbackActionType].Invoke();
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
                    if (attributeType == typeof(ImmediateAction) || attributeType == typeof(ContinuousAction) || attributeType == typeof(TimeBoundedAction))
                    {
                        Assert.IsNotNull(customAttribute.ConstructorArguments);
                        Assert.IsTrue(customAttribute.ConstructorArguments.Count > 0);
                        ActionType actionType = (ActionType) customAttribute.ConstructorArguments[0].Value;
                        ActionDelegate actionDelegate = (ActionDelegate) Delegate.CreateDelegate(typeof(ActionDelegate), this, method.Name, false);
                        _actionsMapping.Add(actionType, actionDelegate);
                    }
                }
            }

            // second for because a fallback might be a time bounded action too, so i need to have all the actions before setting fallbacks
            foreach (var method in methods)
            {
                foreach (var customAttribute in method.CustomAttributes)
                {
                    var attributeType = customAttribute.AttributeType;
                    if (attributeType == typeof(TimeBoundedAction))
                    {
                        Assert.IsNotNull(customAttribute.ConstructorArguments);
                        Assert.IsTrue(customAttribute.ConstructorArguments.Count > 1);
                        ActionType actionType = (ActionType) customAttribute.ConstructorArguments[0].Value;
                        ActionType fallbackActionType = (ActionType) customAttribute.ConstructorArguments[1].Value;
                        // fallback of action type (timebounded) is the delegate linked to fallbackActionType type
                        _fallbackActionsMapping.Add(actionType, _actionsMapping[fallbackActionType]);
                    }
                }
            }
        }
        

        #endregion

    }
    
    


}