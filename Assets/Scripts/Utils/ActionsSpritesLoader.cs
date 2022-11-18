using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DLLF
{
    public class ActionsSpritesLoader
    {
        private static readonly string _actionsSpritesPath = "ActionsSprites";
        private static ActionsSpritesLoader _instance;
        public static ActionsSpritesLoader Instance => _instance ??= new ActionsSpritesLoader();

        private Dictionary<ActionType, Sprite> _typedSprites;
        
        public bool Loaded { get; private set; }

        public void LoadSprites()
        {
            if (Loaded) return;
            _typedSprites = new Dictionary<ActionType, Sprite>();
            Sprite[] sprites = Resources.LoadAll<Sprite>(_actionsSpritesPath);
            Debug.Log("Loaded " + sprites.Length + " sprites");
            foreach (var sprite in sprites)
            {
                ActionType actionType;
                bool nameExists = Enum.TryParse(sprite.name, out actionType);
                
                //if sprite has a name of action type insert it
                if (nameExists) InsertNewTypedSprite(actionType, sprite);
                //else ignore it
                else Debug.LogWarning(sprite.name + " is not an ActionType!");
            }
            Debug.Log(_typedSprites.Count + "/" + sprites.Length +" of sprites where actions' sprites");
            HashSet<ActionType> missingActionTypes = CheckMissingTypes(_typedSprites);
            if (missingActionTypes.Count != 0)
            {
                LogMissingActionTypes(missingActionTypes);
            }

            Loaded = true;
        }

        private static void LogMissingActionTypes(HashSet<ActionType> missingActionTypes)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(missingActionTypes.Count);
            sb.AppendLine(" ActionTypes do not have a linked Sprite: ");
            foreach (var type in missingActionTypes)
            {
                sb.AppendLine("- " + type);
            }
            Debug.LogWarning(sb);
        }

        public Sprite[] GetAllSprites()
        {
            Sprite[] sprites = new Sprite[_typedSprites.Keys.Count];
            _typedSprites.Values.CopyTo(sprites, 0);
            return sprites;
        }

        public Sprite GetSprite(ActionType type)
        {
            if (_typedSprites.TryGetValue(type, out var sprite))
            {
                return sprite;
            }
            return null;
        }

        private ActionsSpritesLoader()
        {
            if (!Loaded) LoadSprites();
        }

        private void InsertNewTypedSprite(ActionType type, Sprite sprite)
        {
            bool inserted = _typedSprites.TryAdd(type, sprite);
            if (!inserted)
            {
                Debug.LogWarning("Duplicated sprite for action type = " + type);
            }
        }

        private HashSet<ActionType> CheckMissingTypes(Dictionary<ActionType, Sprite> typedSprites)
        {
            HashSet<ActionType> availableActionTypes = ((ActionType[])Enum.GetValues(typeof(ActionType))).ToHashSet();
            availableActionTypes.Remove(ActionType.Null);
            HashSet<ActionType> loadedActionTypes = typedSprites.Keys.ToHashSet();
            availableActionTypes.ExceptWith(loadedActionTypes);
            return availableActionTypes;
        }
    }
}