using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLLF
{

    public delegate void ActionDelegate();

    public class ActionsManager : MonoBehaviour
    {
        [SerializeField] private ActionsSequence _actionsTypeSequence;
        [SerializeField] private CharacterController2DParams characterController2DParams;
        [SerializeField] private ActionsSprites _actionsSprites;

        [SerializeField] private float _actionsDuration;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private ActionUIController _actionUIController;


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
            StartCoroutine(JumpCoroutine());
        }

        private IEnumerator JumpCoroutine()
        {
            //activate jump
            Debug.Log("Activating jump");
            yield return new WaitForSeconds(_actionsDuration);
            //deactivate jump
            Debug.Log("Deactivating jump");
        }
        
        private void WalkRight()
        {
            StartCoroutine(WalkRightCoroutine());
        }

        private IEnumerator WalkRightCoroutine()
        {
            Debug.Log("Activating WR");
            yield return new WaitForSeconds(_actionsDuration);
            Debug.Log("Deactivating WR");
        }
        
        private void WalkLeft()
        {
            StartCoroutine(WalkLeftCoroutine());
        }

        private IEnumerator WalkLeftCoroutine()
        {
            Debug.Log("Activating WL");
            yield return new WaitForSeconds(_actionsDuration);
            Debug.Log("Deactivating WL");
        }
    }


}