using UnityEngine;

namespace DLLF
{
    [CreateAssetMenu(fileName = "ActionParams", menuName = "ScriptableObjects/ActionParameters", order = 1)]
    public class ActionParameters : ScriptableObject
    {
        public float walkSpeed = 10f;
    }
}