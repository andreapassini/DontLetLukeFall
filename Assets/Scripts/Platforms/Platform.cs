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

        private void Start()
        {
            _actionsManager = FindObjectOfType<ActionsManager>();
            spawnTime = Time.time;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("CollisionDetected");
            if (collision.gameObject.tag.Equals("Player"))
            {
                Debug.Log("LoadAction");
                _actionsManager.StartPlatformAction(action);
            }
            else if(collision.gameObject.layer.Equals("PlayerPlatform"))
            {
                Platform platform = collision.gameObject.GetComponent<Platform>();
                if (platform != null || action!=ActionType.Null)
                {
                    if (platform.spawnTime < spawnTime)
                    {
                       platform.action=action;
                    }
                }
            }
        }
    }
}