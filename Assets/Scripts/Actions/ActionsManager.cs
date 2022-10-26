using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLLF
{
    //TODO: ciclo azioni da sequenze
    //TODO: azioni piattaforme vengono aggiunte da sole e hanno una propria coroutine per terminarle
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
        
        //TODO: integrare con ActionUIController
        private IEnumerator ActionsSequenceCoroutine()
        {
            while (_active)
            {
                IAction nextAction;
                if (_actionsSequence.TryDequeue(out nextAction))
                {
                    //there is an action
                    if (nextAction.IsHorizontal()) SubstituteHorizontalAction(nextAction);
                    else SubstituteVerticalAction(nextAction);
                    Sprite actionSprite = _actionsSprites.GetSprite(nextAction.GetActionType());
                    //ActionsUIController.AddSprite(actionSprite)
                }
            }

            yield return null;
        }

        private void SubstituteVerticalAction(IAction nextAction)
        {
            Debug.Log("Stopping active vertical action: " + _activeVerticalAction.GetActionType());
            _activeVerticalAction.End();
            _activeVerticalAction = nextAction;
            Debug.Log("Starting new vertical action: " + _activeVerticalAction.GetActionType());
            _activeVerticalAction.Start();
        }

        private void SubstituteHorizontalAction(IAction nextAction)
        {
            Debug.Log("Stopping active horizontal action: " + _activeHorizontalAction.GetActionType());
            _activeHorizontalAction.End();
            _activeHorizontalAction = nextAction;
            Debug.Log("Starting new horizontal action: " + _activeHorizontalAction.GetActionType());
            _activeHorizontalAction.Start();
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

    }
}