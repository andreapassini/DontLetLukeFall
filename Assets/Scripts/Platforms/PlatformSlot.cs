using System;
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

        private LevelManager _levelManager;

        private void Awake()
        {
            _levelManager = FindObjectOfType<LevelManager>();
        }

        public void GeneratePlatform(PlatformUI platform)
        {
            isEmpty = false;
            var spawned = Instantiate(platform.gameObject, this.gameObject.transform);
            spawned.GetComponent<PlatformUI>().SetSlowMo(_levelManager.GetSlowMo());
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