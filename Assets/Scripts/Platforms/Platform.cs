using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DLLF
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Platform : MonoBehaviour
    {
        public ActionType action;
        [HideInInspector]
        public float spawnTime;

        private void Start()
        {
            spawnTime = Time.time;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {

            Debug.Log("CollisionDetected");
            if (collision.gameObject.tag.Equals("Player"))
            {
                Debug.Log("LoadAction");
            }
            else
            {
                Platform platform = collision.gameObject.GetComponent<Platform>();
                if (platform != null)
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