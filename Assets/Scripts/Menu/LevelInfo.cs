using UnityEngine;

namespace DLLF
{
    [CreateAssetMenu(fileName = "LevelInfo", menuName = "Levels/LevelInfo", order = 1)]
    public class LevelInfo : ScriptableObject // A scriptable object to represent infos of a level
    {
    
        public int levelNumber; // the number of the level (numbers should be starting from 1, so: 1, 2, 3, ...)
        public Sprite image; // an image representing a level
    
    }
}
