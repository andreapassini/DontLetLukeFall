using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This scriptable object is useful to configure sounds in the inspector
[CreateAssetMenu(menuName = "SoundsConfiguration")]
public class SoundsConfiguration : ScriptableObject
{
    
    public Sound[] sounds;
    
}
