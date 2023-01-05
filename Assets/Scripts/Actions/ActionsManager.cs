using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
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
        [SerializeField] private CharacterController2D _characterController;
        [SerializeField] private ActionUIController _actionUIController;

        [SerializeField] private ActionsSpritesSpawner _actionsSpritesSpawner;

        [SerializeField] private float _startDelay;
        
        
        private bool _jump;
        private float _speed;
        private bool _isRunning;
        private bool _crouching;
        
        private Queue<ActionType> _actionsSequence = new Queue<ActionType>();
        private Dictionary<ActionType, ActionDelegate> _actionsMapping;
        private ActionsSequence _actionsTypeSequence;
        private ActionsSpritesLoader _actionsSpritesLoader;

        private bool _needToContinueWithTheActionSequence = true; // This var is set to false when Luke Win

        public ActionType[]
            GetActionSequence() // Function to get the action sequence used for the ActionVisualizer useful when creating a level
        {
            return _actionsTypeSequence.actions;
        }

        public ActionType[]
            GetActionSequenceViaLevelManager(
                LevelManager levelManager) // Function to get the action sequence via the level managerused for the ActionScriptVisualizer useful when creating a level
        {
            ActionsSequence actionsSequence = levelManager.GetLevelActionsSequence();
            Begin(actionsSequence);
            return _actionsTypeSequence.actions;
        }

        private void Awake()
        {
            _characterController ??= transform.GetComponentInParent<CharacterController2D>();
            _actionsSpritesLoader = ActionsSpritesLoader.Instance;
            if (!_actionsSpritesLoader.Loaded) _actionsSpritesLoader.LoadSprites();
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

        public void StartPlatformAction(ActionType actionType)
        {
            var actionDelegate = _actionsMapping[actionType];
            actionDelegate.Invoke();
            _actionsSpritesSpawner.SpawnPlatformActionSprite(_characterController.transform, actionType);
        }


        private void Update()
        {
            var movementRequest = new MovementRequest
            {
                Jump = _jump,
                UnitsToJump = _jump ? movementParams.GetUnitToCoverForJump(_isRunning) : 0,
                Speed = _speed,
                Crouch = _crouching
            };
            _characterController.Move(movementRequest);
            if (_jump) _jump = false;
        }


        private IEnumerator StartActionSequence()
        {
            SendActionSequenceToActionUIController(0.01f);
            _actionsSequence.Dequeue(); //dequeuing placeholder
            yield return new WaitForSecondsRealtime(_startDelay);
            while (_actionsSequence.TryDequeue(out var actionToPerform) && _needToContinueWithTheActionSequence)
            {
                ActionDelegate actionDelegate = _actionsMapping[actionToPerform];

                float timeToComplete = actionDelegate.Invoke();
                _actionsSpritesSpawner.SpawnSequenceActionSprite(_characterController.transform, actionToPerform,
                    timeToComplete);
                _actionUIController.NextAction(timeToComplete);
#if UNITY_EDITOR
                actionsPosition.Add(_characterController.transform.position);
#endif

                AudioManager.instance.PlayActionSFX();

                yield return new WaitForSeconds(timeToComplete);
            }

            _actionUIController.StopSequence();
        }


        #region Actions
        [ContinuousAction(ActionType.Crouch)]
        private float Crouch()
        {
            _crouching = true;
            _speed = movementParams.CrouchSpeed;
            return GetTime(movementParams.UnitsCoveredPerAction, _speed);
        }

		[ImmediateAction(ActionType.Jump)]
        private float Jump()
        {
            _jump = true;
            // if it is running it will cover more units
            return GetTime(movementParams.GetUnitToCoverForJump(_isRunning), _speed);
        }

        //TODO for every action that changes horizontal direction rotate the player or flip the sprite render on Y axis
        [ContinuousAction(ActionType.WalkRight)]
        private float WalkRight()
        {

            _speed = movementParams.WalkSpeed;
            _isRunning = false;
            _crouching = false;
            return GetTime(movementParams.UnitsCoveredPerAction, _speed);
        }


        [ContinuousAction(ActionType.WalkLeft)]
        private float WalkLeft()
        {

            _speed = -movementParams.WalkSpeed;
            _isRunning = false;
            _crouching = false;
            return GetTime(movementParams.UnitsCoveredPerAction, _speed);
        }

        // Continuous action that lets the player run to the right
        // Increase speed to RunSpeed value
        [ContinuousAction(ActionType.RunRight)]
        private float RunRight()
        {

            _speed = movementParams.RunSpeed;
            _isRunning = true;
            _crouching = false;
            return GetTime(movementParams.UnitsCoveredPerAction, _speed);
        }

        // Continuous action that lets the player run to the left
        // Increase speed to RunSpeed value
        [ContinuousAction(ActionType.RunLeft)]
        private float RunLeft()
        {

            _speed = -movementParams.RunSpeed;
            _isRunning = true;
            _crouching = false;
            return GetTime(movementParams.UnitsCoveredPerAction, _speed);
        }

        [ImmediateAction(ActionType.Stop)]
        private float Stop()
        {


            _speed = 0;
            _isRunning = false;
            _crouching = false;
            _jump = false;
            return 3f;
        }

        [ImmediateAction(ActionType.Die)]
        private float Die()
        {


            _speed = 0;
            _isRunning = false;
            _crouching = false;
            _jump = false;
            StopCoroutine(nameof(StartActionSequence));
            _actionUIController.StopSequence();
            EventManager.TriggerEvent(LevelManager.OnLevelFailedEventName);
            return 100f;
        }

        #endregion
        
        public void animationDeath() // This method is to play the death animation when Luke die (fall)
        {
            _speed = 0;
            _isRunning = false;
            _crouching = false;
            _jump = false;
            _characterController.animationDeath(); // play the death animation
        }
        
        public void animationWon() // This method is to play an animation when Luke arrive to the end of the level
        {
            Stop();
            _needToContinueWithTheActionSequence = false;
            _actionUIController.StopSequence();
            _characterController.animationWon(); // play an animation that Luke has won
        }

        // method to calculate time to cover the given space: spaceToCover, at a given speed: speed
        private float GetTime(float spaceToCover, float speed)
        {
            return Mathf.Abs(spaceToCover / speed);
        }

        private void SendActionSequenceToActionUIController(float timeToComplete)
        {
            List<Sprite> listOfSpriteToLoad = new List<Sprite>();
            foreach (var action in _actionsTypeSequence.actions)
            {
                listOfSpriteToLoad.Add(_actionsSpritesLoader.GetSprite(action));
            }

            _actionUIController.LoadActionSequence(listOfSpriteToLoad, timeToComplete);
        }


        #region GIZMOS

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

        #endregion

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
                        ActionType actionType = (ActionType)customAttribute.ConstructorArguments[0].Value;
                        ActionDelegate actionDelegate =
                            (ActionDelegate)Delegate.CreateDelegate(typeof(ActionDelegate), this, method.Name, false);
                        _actionsMapping.Add(actionType, actionDelegate);
                    }
                }
            }
        }

        #endregion
    }
}