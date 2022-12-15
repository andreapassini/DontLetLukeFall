using System.Collections;
using UnityEngine;

namespace DLLF
{
    public class TimedSlowMotion : MonoBehaviour, ISlowMotion
    {
        private float _originalTimeScale;
        private float _slowMoTimeScale;
        
        [SerializeField]
        private float _slowMoMultiplier;
        [SerializeField]
        private float _slowMoDuration;
        
        void Awake()
        {
            _originalTimeScale = Time.timeScale;
            _slowMoTimeScale = _originalTimeScale * _slowMoMultiplier;
        }
        
        public void ActivateSlowMotion()
        {
            Debug.Log("Activating slow mo");
            StartCoroutine(StartTimedSlowMo());
        }

        private IEnumerator StartTimedSlowMo()
        {
            Time.timeScale = _slowMoTimeScale;
            yield return new WaitForSecondsRealtime(_slowMoDuration);
            Debug.Log("Deactivating slow mo");
            Time.timeScale = _originalTimeScale;
        } 

        public void DeactivateSlowMotion()
        {
            StopCoroutine(nameof(StartTimedSlowMo));
            Time.timeScale = _originalTimeScale;
        }
        
    }
}