using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DLLF
{
    public class PlatformUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private Camera _cam;
        [SerializeField]
        private GameObject _realPlatform;
        [SerializeField]
        private Canvas _canvas;

        private RectTransform _rectTransform;

        private PlatformSlot _slot;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            if (_canvas == null)
            {
                _canvas = GameObject.FindObjectOfType<Canvas>();
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            _cam = Camera.main;
            _slot = this.GetComponentInParent<PlatformSlot>();
            _canvasGroup = this.GetComponent<CanvasGroup>();
        }

        public void SpawnInWorld()
        {
            Vector3 spawnPosition = _cam.ScreenToWorldPoint(transform.position);
            spawnPosition = new Vector3(spawnPosition.x, spawnPosition.y, 0);
            GameObject SpawnedPlatform = Instantiate<GameObject>(_realPlatform);
            SpawnedPlatform.transform.position = spawnPosition;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("onBeginDrag");
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag");
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            if (!_slot.reselectedPlatform)
            {
                _slot.isEmpty = true;
                SpawnInWorld();
                Destroy(this.gameObject);
            }
            else
            {
                _slot.reselectedPlatform = false;
            }
            _canvasGroup.blocksRaycasts = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown");
        }

    }
}