using UnityEngine;

namespace DLLF
{
    [CreateAssetMenu(fileName = "LevelInfo", menuName = "Levels/LevelInfo", order = 1)]
    public class LevelInfo : ScriptableObject
    {
    
        public string sceneName;
        public Sprite image;
    
    }
}
