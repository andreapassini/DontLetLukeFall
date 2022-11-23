using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DLLF
{
    [RequireComponent(typeof(CinemachineCameraOffset))]
    public class CameraOffsetHandler : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField]
        private Vector3 TopLeft;
        [SerializeField]
        private Vector3 TopRight;
        [SerializeField]
        private Vector3 BottomLeft;
        [SerializeField]
        private Vector3 BottomRight;
        [SerializeField]
        private float timeCamera;
        private float timeStart;
        private CinemachineCameraOffset offset;
        [SerializeField]
        private CharacterController2D player;
        private Vector3 target;
        private Vector3 lastTarget;
        private Vector3 lastPosition;
        void Start()
        {
            offset = GetComponent<CinemachineCameraOffset>();
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            lastTarget = target;
            if (player.Velocity.x < 0)
            {
                target = player.Velocity.y < 0 ? BottomLeft : TopLeft;
            }
            else
            {
                target = player.Velocity.y < 0 ? BottomRight : TopRight;
            }
            if (target.x != lastTarget.x || target.y != lastTarget.y)
            {
                Debug.Log("reset");
                timeStart = 0;
                lastPosition = offset.m_Offset;
            }
            else if(offset.m_Offset.x != target.x || offset.m_Offset.y != target.y)
            {
                timeStart += Time.deltaTime;
            }
            if (timeStart <= timeCamera)
            {
                offset.m_Offset = Vector3.Lerp(lastPosition, target, timeStart/timeCamera);
            }
        }
    }
}