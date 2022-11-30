using System;
using System.Collections;
using DLLF;
using UnityEngine;
using UnityEngine.Pool;

namespace DLLF
{
    [RequireComponent(typeof(SpriteRenderer))]

    public class SpriteVisualizer : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private IObjectPool<SpriteVisualizer> _pool;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Init(Sprite actionSprite, float fadeOutTime)
        {
            _spriteRenderer.sprite = actionSprite;
            StartCoroutine(FadeOut(fadeOutTime));
        }

        public void SetPool(IObjectPool<SpriteVisualizer> pool)
        {
            _pool = pool;
        }
        private IEnumerator FadeOut(float timeToComplete)
        {
            var spriteRendererColor = _spriteRenderer.color;
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / timeToComplete)
            {
                var tmp = _spriteRenderer.color;
                tmp.a = Mathf.Lerp(1.0f,0.0f,t);
                _spriteRenderer.color = tmp;
                yield return null;
            }
            _pool?.Release(this);
        }
    }

}