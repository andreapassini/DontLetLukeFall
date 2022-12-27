using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
        private GameObject _player;
        private Image[] _spriteRenderersUI;
        private List<ActionType> _actionsTypes;

        private ISlowMotion _slowMotion;
        [SerializeField]
        private float _minDistanceFromPlayer=2f;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            if (_canvas == null)
            {
                _canvas = GameObject.FindObjectOfType<Canvas>();
            }
            _player = GameObject.FindGameObjectWithTag("Player");
            _actionsTypes = ((ActionType[])Enum.GetValues(typeof(ActionType))).ToList();
            _actionsTypes.Remove(ActionType.Null);
            _actionsTypes.Remove(ActionType.Die);

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

        public void SetSlowMo(ISlowMotion slowMotion)
        {
            _slowMotion = slowMotion;
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
                
                //randomize the effect of the platform
                _spawnedPlatform.GetComponent<Platform>().action = RandomizeEffect();
            }
            _spawnedPlatform.transform.position = spawnPosition;
        }


        private ActionType RandomizeEffect()
        {
            //for now it is only jump
            //return _actionsTypes[Random.Range(0, _actionsTypes.Count)];
            return ActionType.Jump;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
         //   Debug.Log("onBeginDrag");
            _slowMotion.ActivateSlowMotion();
            _canvasGroup.blocksRaycasts = false;
            SpawnInWorld();
            _spawnedPlatform.layer = LayerMask.NameToLayer("UI");
            SetVisibleUI(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            //Debug.Log("OnDrag");
            if (_canvas == null)
            {
                _canvas = GameObject.FindObjectOfType<Canvas>();
            }
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
            Vector3 spawnPosition = _cam.ScreenToWorldPoint(transform.position);
            spawnPosition = new Vector3((int)spawnPosition.x, (int)spawnPosition.y, 0);
            if (Vector3.Distance(spawnPosition, _player.transform.position) > _minDistanceFromPlayer)
            {
                _spawnedPlatform.transform.position = spawnPosition;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
           // Debug.Log("OnEndDrag");
            if (!_slot.reselectedPlatform)
            {
                _slot.isEmpty = true;
                _spawnedPlatform.layer = LayerMask.NameToLayer("PlayerPlatform");
                //_spawnedPlatform.GetComponentInChildren<SpriteMask>().enabled = false;
                _spawnedPlatform.transform.Find("SpriteMask").GetComponent<SpriteMask>().enabled = false;
                Destroy(this.gameObject);
            }
            else
            {
                DestroyPlatform();
                SetVisibleUI(true);
                _slot.reselectedPlatform = false;
            }
            _slowMotion.DeactivateSlowMotion();
            _canvasGroup.blocksRaycasts = true;

            AudioManager.instance.PlayPlacePlatfromSFX();
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