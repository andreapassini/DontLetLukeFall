using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This scriptable object is useful to configure witch sounds are there in the game using the inspector
[CreateAssetMenu(menuName = "SoundsConfiguration/generalSoundsConfiguration")]
public class GeneralSoundsConfiguration : ScriptableObject
{
    
    public SoundConfiguration[] sounds; // A list of sounds each one as a scriptable object
    
}
