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

        // Interpolate value of time scale to have a smooth slowmo transition
        [SerializeField]
        private float _interpolationDuration = .15f;
        [SerializeField]
        private float _interpolationSegments = 5f;

        // Mixer ref
        private float _slowMoPitch;
        private float _masterPitch;

        // Feel
         

        private IEnumerator _slowMoBack;
        
        void Awake()
        {
            _originalTimeScale = Time.timeScale;
            _slowMoTimeScale = _originalTimeScale * _slowMoMultiplier;
        }

        private void Start()
        {
            AudioManager.instance.GetAudioMixer().GetFloat("MasterPitch", out _masterPitch);
            _slowMoPitch = _masterPitch * _slowMoMultiplier;

        }

        public void ActivateSlowMotion()
        {
            Debug.Log("Activating slow mo");
            _slowMoBack = SlowMoBackInterpolation();
            StartCoroutine(StartTimedSlowMo());
        }

        private IEnumerator StartTimedSlowMo()
        {
            float unitInterpolation = (_originalTimeScale - _slowMoTimeScale) / _interpolationSegments;

            for (int i = (int)_interpolationSegments; i > 0; i--)
            {
                Time.timeScale = _slowMoTimeScale + (unitInterpolation * i);
                AudioManager.instance.GetAudioMixer().SetFloat("MasterPitch", _slowMoPitch + (unitInterpolation * i));
                yield return new WaitForSecondsRealtime(_interpolationDuration/_interpolationSegments);
            }

            Time.timeScale = _slowMoTimeScale;
            AudioManager.instance.GetAudioMixer().SetFloat("MasterPitch", _slowMoPitch);

            yield return new WaitForSecondsRealtime(_slowMoDuration);

            Debug.Log("Deactivating slow mo");
            StartCoroutine(_slowMoBack);
        } 

        public void DeactivateSlowMotion()
        {
            StopCoroutine(nameof(StartTimedSlowMo));

            // With IEnumerator ref, we can stop coroutine and keep its progression/steps/state
            // so when we call start again using IEnumerator ref, we restart from the stopped state
            // without losing progression
            // If the cor of _slowMoBack is already over, it will not restart
            // To restart the endend cor, StartCoroutine(Coroutine())
            StartCoroutine(_slowMoBack);

            Time.timeScale = _originalTimeScale;
        }

        public IEnumerator SlowMoBackInterpolation()
        {
            float unitInterpolation = (_originalTimeScale - _slowMoTimeScale) / _interpolationSegments;

            for (int i = (int)_interpolationSegments; i > 0; i--)
            {
                Time.timeScale = _slowMoTimeScale + (unitInterpolation * i);
                AudioManager.instance.GetAudioMixer().SetFloat("MasterPitch", _slowMoPitch + (unitInterpolation * i));
                yield return new WaitForSecondsRealtime(_interpolationDuration / _interpolationSegments);
            }

            Time.timeScale = _originalTimeScale;
            AudioManager.instance.GetAudioMixer().SetFloat("MasterPitch", _masterPitch);
        }

        private void OnDestroy()
        {
            Time.timeScale = _originalTimeScale;
        }
    }
}