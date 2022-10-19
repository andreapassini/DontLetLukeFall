using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DLLF
{
    public class PlatformSlot : MonoBehaviour, IDropHandler
    {
        [HideInInspector]
        public bool isEmpty = true;

        [HideInInspector]
        public bool reselectedPlatform=false;

        public void GeneratePlatform(PlatformUI platform)
        {
            isEmpty = false;
            Instantiate(platform.gameObject, this.gameObject.transform);
        }

        public void OnDrop(PointerEventData eventData)
        {
            if(eventData.pointerDrag != null)
            {
                Debug.Log("Droped");
                reselectedPlatform = true;
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector3(1,0,0);
            }
        }
    }
}