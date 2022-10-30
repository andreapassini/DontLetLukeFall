using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This scriptable object is useful to configure a sound in the inspector
[CreateAssetMenu(menuName = "SoundsConfiguration/newSound")]
public class SoundConfiguration : ScriptableObject
{
    
    public Sound sound;
    
}
