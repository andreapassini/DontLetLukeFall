using UnityEngine;

namespace DLLF
{
    [CreateAssetMenu(fileName = "LevelsInfo", menuName = "Levels/LevelsInfo", order = 1)]
    public class LevelsInfo : ScriptableObject
    {

        public LevelInfo[] levelInfos;

    }
}