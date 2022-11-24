﻿using System;
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

        [SerializeField] private ActionsSprites _actionsSprites;

        [SerializeField] private CharacterController2D _characterController;
        [SerializeField] private ActionUIController _actionUIController;

        private Animator _lukeAnimator; // The luke animator (this var is set automatically in Awake via _characterController)
        
        private bool _jump;
        private float _speed;
        private bool _isRunning;

        private Queue<ActionType> _actionsSequence = new Queue<ActionType>();
        private Dictionary<ActionType, ActionDelegate> _actionsMapping;
        private ActionsSequence _actionsTypeSequence;

#if UNITY_EDITOR
        private List<Vector3> actionsPosition = new List<Vector3>();
        // DEBUG
        private void OnDrawGizmos()
        {
            Color gizmosColor = Color.red;
            gizmosColor.a = .5f;
            Gizmos.color = gizmosColor;
            
            foreach (var pos in actionsPosition)
            {
                Gizmos.DrawCube(pos, Vector3.one);
            }
        }
#endif

        public ActionType[] GetActionSequence()
        {
            return _actionsTypeSequence.actions;
        }

        private void Awake()
        {
            _lukeAnimator = _characterController.GetComponent<Animator>(); // Get the animator component
            if (_lukeAnimator == null)
            {
                Debug.Log("You missed to add the animator to Luke!");
            }
            _actionsMapping = new Dictionary<ActionType, ActionDelegate>();
            AutoLinkActionTypesToMethods();
        }

        public void Begin(ActionsSequence actionsSequence)
        {
            _actionsTypeSequence = actionsSequence;
            foreach (var actionType in _actionsTypeSequence.actions)
            {
                _actionsSequence.Enqueue(actionType);
            }
            StartCoroutine(StartActionSequence());
        }

        private void Update()
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

            if (_speed == 0) // In case speed is 0, set iddle animation
            {
                if (_lukeAnimator != null)
                {
                    _lukeAnimator.SetTrigger("IddleTrigger");
                    _lukeAnimator.speed = 1;
                }
            }

        }

        private void SendActionSequenceToActionUIController(float timeToComplete)
        {
            List<Sprite> listOfSpriteToLoad = new List<Sprite>();
            foreach (var action in _actionsTypeSequence.actions)
            {
                listOfSpriteToLoad.Add(_actionsSprites.GetSprite(action));
            }
            _actionUIController.LoadActionSequence(listOfSpriteToLoad, timeToComplete);
        }
        
        private IEnumerator StartActionSequence(bool activate)
		{
            if (activate) {
                yield return new WaitForSecondsRealtime(2);
                while(true) {
                    if (transform.position.x % 5 == 0) {
                        if (_actionsSequence.TryDequeue(out var actionToPerform)) {
                            ActionDelegate actionDelegate = _actionsMapping[actionToPerform];
                            float timeToComplete = actionDelegate.Invoke();
                            if (!_actionUIController.HasBeenLoaded()) {
                                SendActionSequenceToActionUIController(timeToComplete);
                            } else {
                                _actionUIController.NextAction(timeToComplete);
                            }
                            #if UNITY_EDITOR
                            actionsPosition.Add(_characterController.transform.position);
                            #endif
                            Debug.Log("Time to complete for action " + actionToPerform + " is  " + timeToComplete + " (current speed: " + _speed + ")");
                            yield return new WaitForEndOfFrame();
                        }
                    } else {
                        yield return new WaitForEndOfFrame();
					}
				}

                Debug.Log("Actions sequence end");
                _actionUIController.StopSequence();
            }
        }

        private IEnumerator StartActionSequence()
        {
            yield return new WaitUntil(() => _characterController.IsActive);
            while (_actionsSequence.TryDequeue(out var actionToPerform))
            {
               ActionDelegate actionDelegate = _actionsMapping[actionToPerform];
                float timeToComplete = actionDelegate.Invoke();
                if (! _actionUIController.HasBeenLoaded())
                {
                    SendActionSequenceToActionUIController(timeToComplete);
                }
                else
                {
                    _actionUIController.NextAction(timeToComplete);
                }
                #if UNITY_EDITOR
                actionsPosition.Add(_characterController.transform.position);
                #endif
                yield return new WaitForSeconds(timeToComplete);
            }
            Debug.Log("Actions sequence end");
            _actionUIController.StopSequence();
        }

        [ImmediateAction(ActionType.Jump)]
        private float Jump()
        {
            Debug.Log("Activating jump");
            if (_lukeAnimator != null)
            {
                _lukeAnimator.SetTrigger("JumpTrigger"); // set jump animation
                _lukeAnimator.speed = 1;
            }
            _jump = true;
            // if it is running it will cover more units
            return GetTime(movementParams.GetUnitToCoverForJump(_isRunning), _speed);
        }

        [ContinuousAction(ActionType.WalkRight)]
        private float WalkRight()
        {
            Debug.Log("Activating WalkRight");
            if (_lukeAnimator != null)
            {
                _lukeAnimator.SetTrigger("WalkRightTrigger"); // set walk right animation
                _lukeAnimator.speed = 1;
            }
            _speed = movementParams.WalkSpeed;
            _isRunning = false;
            return GetTime(movementParams.UnitsCoveredPerAction, _speed);

        }
        
        
        [ContinuousAction(ActionType.WalkLeft)]
        private float WalkLeft()
        {
            Debug.Log("Activating WalkLeft");
            _lukeAnimator.SetTrigger("WalkLeftTrigger"); // set walk left animation
            if (_lukeAnimator != null)
            {
                _lukeAnimator.speed = 1;
                _speed = -movementParams.WalkSpeed;
            }
            _isRunning = false;
            return GetTime(movementParams.UnitsCoveredPerAction, _speed);

        }
        
        // Continuous action that lets the player run to the right
        // Increase speed to RunSpeed value
        [ContinuousAction(ActionType.RunRight)]
        private float RunRight()
        {
            Debug.Log("Activating RunRight");
            if (_lukeAnimator != null)
            {
                _lukeAnimator.SetTrigger("RunRightTrigger"); // set run right animation; double the speed
                _lukeAnimator.speed = 2;
            }
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
            if (_lukeAnimator != null)
            {
                _lukeAnimator.SetTrigger("RunLeftTrigger"); // set run left animation; double the speed
                _lukeAnimator.speed = 2;
            }
            _speed = -movementParams.RunSpeed;
            _isRunning = true;
            return GetTime(movementParams.UnitsCoveredPerAction, _speed);

        }
        
        // method to calculate time to cover the given space: spaceToCover, at a given speed: speed
        private float GetTime(float spaceToCover, float speed)
        {
            return Mathf.Abs(spaceToCover / speed);
        }


        

        #region AutoSetupWithReflection

        private void AutoLinkActionTypesToMethods()
        {
            var methods = typeof(ActionsManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var method in methods)
            {
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