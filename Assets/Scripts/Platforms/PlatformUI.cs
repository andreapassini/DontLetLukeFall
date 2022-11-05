using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DLLF
{
    [RequireComponent(typeof(CanvasGroup))]
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
        [SerializeField]
        [Min(0.1f)]
        private float _scaleFactor = 1.6f;
        private bool _isWithEffect=false;
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
            if (Random.Range(0, 100) < 20)
            {
                _isWithEffect = true;
                Image[] spriteRenderersUI = GetComponentsInChildren<Image>();
                foreach (Image renderer in spriteRenderersUI)
                {
                    renderer.color = new Color(255, 0, 0);
                }
            }
        }

        public void SpawnInWorld()
        {
            Vector3 spawnPosition = _cam.ScreenToWorldPoint(transform.position);
            spawnPosition = new Vector3(spawnPosition.x, spawnPosition.y, 0);
            GameObject SpawnedPlatform = Instantiate<GameObject>(_realPlatform);
            SpawnedPlatform.AddComponent(typeof(Platform));
            if (_isWithEffect)
            {
                SpriteRenderer[] spriteRenderers = SpawnedPlatform.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer renderer in spriteRenderers)
                {
                    renderer.color = new Color(255, 0, 0);
                }
            }
            SpawnedPlatform.transform.position = spawnPosition;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("onBeginDrag");
            _canvasGroup.blocksRaycasts = false;
            this.transform.localScale = this.transform.localScale * _scaleFactor;
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
                this.transform.localScale = this.transform.localScale / _scaleFactor;
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