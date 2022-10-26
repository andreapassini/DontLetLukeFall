using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private ActionUIController _actionUIController;


        [SerializeField] private bool _active = true;
        
        private Queue<IAction> _actionsSequence = new Queue<IAction>();
        private ActionsFactory _actionsFactory;

        private IAction _activeHorizontalAction;
        private IAction _activeVerticalAction;

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
            /*
            // set the first n sprite in action ui sprite 
            int nOfDisplayedActions = _actionUiController.GetNumberOfDisplayedActions();
            if (_actionsSequence.Count < 3)
            {
                Debug.LogWarning("Beware: less than " + nOfDisplayedActions +"actions  in the sequence!");
            }
            for (int i = 0; i < nOfDisplayedActions; i++)
            {
                //retrieve action sprite using action type of i-th action
                Sprite actionSprite = _actionsSprites.GetSprite(_actionsTypeSequence.actions[i]);
                _actionUIController.AddActionSprite(actionSprite);
            }*/
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

        private IEnumerator ActionsSequenceCoroutine()
        {
            while (_active)
            {
                if (_actionsSequence.Count == 0)
                {
                    _active = false;
                    _activeHorizontalAction = null;
                    _activeVerticalAction = null;
                    Debug.Log("No more actions to perform");
                    yield break;
                }
                IAction nextAction = _actionsSequence.Dequeue();
                if (nextAction.IsHorizontal())
                {
                    _activeHorizontalAction = nextAction;
                }
                else
                {
                    _activeVerticalAction = nextAction;
                }
                Debug.Log("Invoking action: " + nextAction.GetActionType());
                Sprite spriteToBeSentToUi = _actionsSprites.GetSprite(nextAction.GetActionType());
                _actionUIController.AddActionSprite(spriteToBeSentToUi);
                yield return new WaitForSeconds(_actionsDuration);
                Debug.Log("Action terminated");
            }

            yield return null;
        }
        
        public void SubstituteHorizontalAction(ActionType actionType)
        {
            _activeHorizontalAction = CreateAction(actionType);
        }

        private IAction CreateAction(ActionType actionType)
        {
            IAction newAction = _actionsFactory.CreateAction(actionType, _actionParameters, _characterController);
            return newAction;
        }
        

        private void Update()
        {
            _activeHorizontalAction?.Invoke();
            _activeVerticalAction?.Invoke();
        }
       
    }
}