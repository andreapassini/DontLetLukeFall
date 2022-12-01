using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DLLF
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Platform : MonoBehaviour
    {
        public ActionType action = ActionType.Null;
        [HideInInspector]
        public float spawnTime;
        private ActionsManager _actionsManager;
        private int _playerPlatformLayer;

        private void Awake()
        {
            _playerPlatformLayer = LayerMask.NameToLayer("PlayerPlatform");
        }

        private void Start()
        {
            _actionsManager = FindObjectOfType<ActionsManager>();
            spawnTime = Time.time;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log(gameObject.name + "trigger detected");
            if (col.gameObject.tag.Equals("Player"))
            {
                if (action != ActionType.Null)
                {
                    Debug.Log("LoadAction");
                    _actionsManager.StartPlatformAction(action);
                }
            }
            
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            Debug.Log(gameObject.name + " collision detected");
            if (col.gameObject.layer == _playerPlatformLayer)
            {
                Platform platform = col.gameObject.GetComponent<Platform>();
                if (platform != null || action != ActionType.Null)
                {
                    if (platform.spawnTime < spawnTime)
                    {
                        platform.action = action;
                    }
                }
            }
        }
    }
}