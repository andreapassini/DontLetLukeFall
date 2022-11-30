using System;
using System.Collections;
using System.Collections.Generic;
using DLLF;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace DLLF
{
    
//TODO refactor with object pooling
    public class ActionsSpritesSpawner : MonoBehaviour
    {
        [Header("Actions sprites options")]
        [Tooltip("Set whether platform actions should override sprite action when activated")]
        [SerializeField]
        private bool _showPlatformActions = true;

        [Tooltip("The duration of the actions' sprites fade out")] [Range(0.1f, 3f)] [SerializeField]
        private float _actionsFadeOut = 1f;

        [Tooltip("Scaling factor relative to character size")] [Range(0.1f, 1f)]
        [SerializeField]
        private float _scalingFactor = 0.5f;

        [Tooltip("If set to true: use time to complete for sequence action sprite fade out; else, use actionsFadeOut parameter")]
        [SerializeField]
        private bool _overrideTimeToComplete = false;

        [Header("Pooling options")] [SerializeField]
        private int _initialCapacity = 3;
        
        [SerializeField]
        private int _maxSize = 5;

        private IObjectPool<SpriteVisualizer> _pool;
        
        private ActionsSpritesLoader _actionsSpritesLoader;

        void Awake()
        {
            _pool = new ObjectPool<SpriteVisualizer>(CreatePooledObject, OnPooledObjectGet, OnPooledObjectRelease, OnPooledObjectDestroy, defaultCapacity:_initialCapacity, maxSize:_maxSize);
            _actionsSpritesLoader = ActionsSpritesLoader.Instance;
        }
        
        private void OnPooledObjectGet(SpriteVisualizer visualizer)
        {
            visualizer.gameObject.SetActive(true);
        }

        private void OnPooledObjectDestroy(SpriteVisualizer visualizer)
        {
            Destroy(visualizer.gameObject);
        }
        
        private void OnPooledObjectRelease(SpriteVisualizer visualizer)
        {
            visualizer.gameObject.SetActive(false);
        }

        private SpriteVisualizer CreatePooledObject()
        {
            GameObject spriteGameObject = new GameObject("ActionSpriteVisualizer");
            var spriteVisualizer = spriteGameObject.AddComponent<SpriteVisualizer>();
            spriteVisualizer.SetPool(_pool);
            return spriteVisualizer;
        }

        public void SpawnSequenceActionSprite(Transform pTransform, ActionType actionType, float fadeOutTime)
        {
            float timeToUse = _overrideTimeToComplete ? _actionsFadeOut : fadeOutTime;
            SpawnInstance(pTransform, actionType, timeToUse);
        }

        public void SpawnPlatformActionSprite(Transform pTransform, ActionType actionType)
        {
            if (!_showPlatformActions) return;
            SpawnInstance(pTransform, actionType, _actionsFadeOut);
        }

        private void SpawnInstance(Transform pTransform, ActionType actionType, float fadeOutTime)
        {
            Sprite sprite = _actionsSpritesLoader.GetSprite(actionType);
            SpriteVisualizer spriteVisualizer = _pool.Get();
            var transf = spriteVisualizer.transform;
            transf.position = pTransform.position;
            transf.rotation = pTransform.rotation;
            transf.localScale = pTransform.localScale * _scalingFactor;
           
            spriteVisualizer.Init(sprite, fadeOutTime);

        }
        
    }

}