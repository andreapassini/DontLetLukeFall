using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This scriptable object is useful to configure a sound in the inspector
[CreateAssetMenu(menuName = "SoundsConfiguration/generalSoundsConfiguration")]
public class GeneralSoundsConfiguration : ScriptableObject
{
    
    public SoundConfiguration[] sounds;
    
}
