using UnityEngine;

namespace DLLF
{
    [CreateAssetMenu(fileName = "LevelsInfo", menuName = "Levels/LevelsInfo", order = 1)]
    public class LevelsInfo : ScriptableObject // A scriptable object to represent levels
    {

        public LevelInfo[] levelInfos;

    }
}