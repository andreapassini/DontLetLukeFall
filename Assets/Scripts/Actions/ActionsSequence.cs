using UnityEngine;

namespace DLLF
{
    [CreateAssetMenu(fileName = "ActionsSequence", menuName = "Actions/ActionsSequence", order = 1)]
    public class ActionsSequence : ScriptableObject
    {
        public ActionType[] actions;
    }
}