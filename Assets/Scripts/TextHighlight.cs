
using UnityEngine;
using UnityEngine.EventSystems;  
using UnityEngine.UI;

namespace DLLF
{
    [RequireComponent(typeof(Text))]
    public class TextHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Text _theText;
        
        [SerializeField]
        private Color _highlightColor;

        [SerializeField]
        private int _highlightSize;

        private Color _originalColor;
        private int _originalSize;

        private void Awake()
        {
            _theText = GetComponent<Text>();
            _originalColor = _theText.color;
            _originalSize = _theText.fontSize;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _theText.color = _highlightColor;
            _theText.fontSize = _highlightSize;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _theText.color = _originalColor;
            _theText.fontSize = _originalSize;
        }

    }
}
