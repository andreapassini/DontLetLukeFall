using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatformUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Camera _cam;
    [SerializeField]
    private GameObject _realPlatform;
    [SerializeField]
    private Canvas _canvas;

    private RectTransform _rectTransform;

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
        this.GetComponentInParent<PlatformSlot>().isEmpty = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        GetComponent<PlatformUI>().SpawnInWorld();
        Destroy(this.gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
    
}
