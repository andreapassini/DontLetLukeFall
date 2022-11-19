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
        private bool _isWithEffect=false;
        private GameObject _spawnedPlatform;
        private Image[] _spriteRenderersUI;
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
            _spriteRenderersUI = GetComponentsInChildren<Image>();
            if (Random.Range(0, 100) < 20)
            {
                _isWithEffect = true;
                foreach (Image renderer in _spriteRenderersUI)
                {
                    renderer.color = new Color(255, 0, 0);
                }
            }
        }

        public void SpawnInWorld()
        {
            Vector3 spawnPosition = _cam.ScreenToWorldPoint(transform.position);
            spawnPosition = new Vector3(spawnPosition.x, spawnPosition.y, 0);
            _spawnedPlatform = Instantiate<GameObject>(_realPlatform);
            if (_isWithEffect)
            {
                SpriteRenderer[] spriteRenderers = _spawnedPlatform.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer renderer in spriteRenderers)
                {
                    renderer.color = new Color(255, 0, 0);
                }
            }
            _spawnedPlatform.transform.position = spawnPosition;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
         //   Debug.Log("onBeginDrag");
            _canvasGroup.blocksRaycasts = false;
            SpawnInWorld();
            _spawnedPlatform.layer = LayerMask.NameToLayer("UI");
            SetVisibleUI(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            //Debug.Log("OnDrag");
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
            Vector3 spawnPosition = _cam.ScreenToWorldPoint(transform.position);
            spawnPosition = new Vector3((int)spawnPosition.x, (int)spawnPosition.y, 0);
            _spawnedPlatform.transform.position = spawnPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
           // Debug.Log("OnEndDrag");
            if (!_slot.reselectedPlatform)
            {
                _slot.isEmpty = true;
                _spawnedPlatform.layer = LayerMask.NameToLayer("Default");
                Destroy(this.gameObject);
            }
            else
            {
                DestroyPlatform();
                SetVisibleUI(true);
                _slot.reselectedPlatform = false;
            }
            _canvasGroup.blocksRaycasts = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
           // Debug.Log("OnPointerDown");
        }

        public void SetVisibleUI(bool isVisible)
        {
            foreach (Image renderer in _spriteRenderersUI)
            {
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, isVisible?255:0);
            }
        }

        public void DestroyPlatform()
        {
            Destroy(_spawnedPlatform.gameObject);
        }
    }
}