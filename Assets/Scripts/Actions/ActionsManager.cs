using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Mono.Collections.Generic;
using UnityEngine;

namespace DLLF
{
    public class ActionsManager : MonoBehaviour
    {
        [SerializeField] private ActionsSequence _actionsTypeSequence;
        [SerializeField] private ActionParameters _actionParameters;
        [SerializeField] private ActionsSprites _actionsSprites;

        [SerializeField] private float _actionsDuration;
        [SerializeField] private CharacterController _characterController;


        [SerializeField] private bool _active = true;
        
        private Queue<IAction> _actionsSequence = new Queue<IAction>();
        private ActionsFactory _actionsFactory;

        private IAction _activeAction;

        private void Awake()
        {
            _actionsFactory = new ActionsFactory();
            foreach (var actionType in _actionsTypeSequence.actions)
            {
                _actionsSequence.Enqueue(CreateAction(actionType));
            }
        }

        void Start()
        {
            StartActionsSequence();
        }

        public void StartActionsSequence()
        {
            StartCoroutine(ActionsSequenceCoroutine());
        }

        public void StopActionsSequence()
        {
            StopCoroutine(nameof(ActionsSequenceCoroutine));
        }

        public void StopOngoingAction()
        {
            _activeAction = null;
        }

        private IEnumerator ActionsSequenceCoroutine()
        {
            while (_active)
            {
                Debug.Log("\n");
                if (_actionsSequence.Count == 0)
                {
                    _active = false;
                    _activeAction = null;
                    Debug.Log("No more actions to perform");
                    yield break;
                }
                _activeAction = _actionsSequence.Dequeue();
                Debug.Log("Invoking action: " + _activeAction.GetActionType());
                Sprite spriteToBeSentToUi = _actionsSprites.GetSprite(_activeAction.GetActionType());
                // call to ActionsUI to display spriteToBeSentToUi
                yield return new WaitForSeconds(_actionsDuration);
                Debug.Log("Action terminated");
            }

            yield return null;
        }
        
        public void SubstituteCurrentAction(ActionType actionType)
        {
            _activeAction = CreateAction(actionType);
        }

        public void InsertNewActionAsNextAction(ActionType actionType)
        {
            IAction newAction = CreateAction(actionType);
            // if queue is empty, just add the new action
            if (_actionsSequence.Count == 0)
            {
                _actionsSequence.Enqueue(newAction);
                return;
            }
            // otherwise insert it before the actual queue head
            IAction actionAtQueueHead = _actionsSequence.Dequeue();
            var queueAsList = new List<IAction>(_actionsSequence.ToArray());
            queueAsList.Insert(0, newAction);
            queueAsList.Insert(0, actionAtQueueHead);
            _actionsSequence = new Queue<IAction>(queueAsList);
        }

        private IAction CreateAction(ActionType actionType)
        {
            IAction newAction = _actionsFactory.CreateAction(actionType, _actionParameters);
            newAction.SetController(_characterController);
            return newAction;
        }
        

        private void Update()
        {
            _activeAction?.Invoke();
        }
       
    }
}