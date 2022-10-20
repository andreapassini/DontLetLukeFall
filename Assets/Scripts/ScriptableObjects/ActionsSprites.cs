using System;
using System.Collections.Generic;
using UnityEngine;

namespace DLLF
{
    [CreateAssetMenu(fileName = "ActionsSprites", menuName = "Actions/ActionSprites", order = 1)]
    public class ActionsSprites : ScriptableObject
    {
        [Serializable]
        private struct TypedSprite
        {
            public Sprite sprite;
            public ActionType actionType;
        }

        [SerializeField] private TypedSprite[] _typedSprites;

        private Dictionary<ActionType, Sprite> _spritesMap;

        private void OnEnable()
        {
            _spritesMap = new Dictionary<ActionType, Sprite>();
            foreach (var typedSprite in _typedSprites)
            {
                _spritesMap.Add(typedSprite.actionType, typedSprite.sprite);
            }
        }

        public Sprite GetSprite(ActionType type)
        {
            return _spritesMap[type];
        }


    }
}