using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSlot : MonoBehaviour
{
    [HideInInspector]
    public bool isEmpty=true;
    
    public void GeneratePlatform(PlatformUI platform)
    {
        isEmpty = false;
        Instantiate(platform.gameObject,this.gameObject.transform);
    }
}
