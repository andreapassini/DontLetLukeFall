using System.Collections;
using UnityEngine;
using MoreMountains.Feedbacks;

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

        // At Start Interpolate value of time scale to have a smooth slowmo transition
        [SerializeField]
        private float _interpolationDurationAtStart = .15f;
        [SerializeField]
        private float _interpolationSegmentsAtStart = 25f;

        // At End Interpolate value of time scale to have a smooth slowmo transition
        [SerializeField]
        private float _interpolationDurationAtEnd = .15f;
        [SerializeField]
        private float _interpolationSegmentsAtEnd = 25f;

        // Mixer ref
        private float _slowMoPitch;
        private float _masterPitch;

		// Feedback
		[SerializeField]
        private MMF_Player _slowMoFeedback;

        private IEnumerator _slowMoBack;
        
        void Awake()
        {
            _originalTimeScale = Time.timeScale;
            _slowMoTimeScale = _originalTimeScale * _slowMoMultiplier;
        }

        private void Start()
        {
            AudioManager.instance.GetAudioMixer().GetFloat("MasterPitch", out _masterPitch);
            _slowMoFeedback = transform.GetComponentInChildren<MMF_Player>();
            _slowMoPitch = _masterPitch * _slowMoMultiplier;
        }

        public void ActivateSlowMotion()
        {
            Debug.Log("Activating slow mo");
            _slowMoBack = SlowMoBackInterpolation();
            StartCoroutine(StartTimedSlowMo());

            //StartSlowMoFeel();
        }

        private IEnumerator StartTimedSlowMo()
        {
            _slowMoFeedback?.PlayFeedbacks();

            float unitInterpolation = (_originalTimeScale - _slowMoTimeScale) / _interpolationSegmentsAtStart;

            for (int i = (int)_interpolationSegmentsAtStart; i > 0; i--)
            {
                //Time.timeScale = _slowMoTimeScale + (unitInterpolation * i);
                AudioManager.instance.GetAudioMixer().SetFloat("MasterPitch", _slowMoPitch + (unitInterpolation * i));
                yield return new WaitForSecondsRealtime(_interpolationDurationAtStart/_interpolationSegmentsAtStart);
            }

            //Time.timeScale = _slowMoTimeScale;
            AudioManager.instance.GetAudioMixer().SetFloat("MasterPitch", _slowMoPitch);

            yield return new WaitForSecondsRealtime(_slowMoDuration);

            Debug.Log("Deactivating slow mo");
            StartCoroutine(_slowMoBack);
        } 

        public void DeactivateSlowMotion()
        {
            StopCoroutine(StartTimedSlowMo());

            // With IEnumerator ref, we can stop coroutine and keep its progression/steps/state
            // so when we call start again using IEnumerator ref, we restart from the stopped state
            // without losing progression
            // If the cor of _slowMoBack is already over, it will not restart
            // To restart the endend cor, StartCoroutine(Coroutine())
            StartCoroutine(_slowMoBack);

            //Time.timeScale = _originalTimeScale;
            //StopSlowMoFeel();
            _slowMoFeedback?.StopFeedbacks();
        }

        public IEnumerator SlowMoBackInterpolation()
        {
            float unitInterpolation = (_originalTimeScale - _slowMoTimeScale) / _interpolationSegmentsAtEnd;

            for (int i = 0; i > (int)_interpolationSegmentsAtEnd; i++)
            {
                //Time.timeScale = _slowMoTimeScale + (unitInterpolation * i);
                AudioManager.instance.GetAudioMixer().SetFloat("MasterPitch", _slowMoPitch + (unitInterpolation * i));
                yield return new WaitForSecondsRealtime(_interpolationDurationAtEnd / _interpolationSegmentsAtEnd);
            }

            //Time.timeScale = _originalTimeScale;
            AudioManager.instance.GetAudioMixer().SetFloat("MasterPitch", _masterPitch);

            _slowMoFeedback?.StopFeedbacks();
        }

        private void OnDestroy()
        {
            //Time.timeScale = _originalTimeScale;
            AudioManager.instance.GetAudioMixer().SetFloat("MasterPitch", _masterPitch);
            _slowMoFeedback?.StopFeedbacks();
        }

        private void StartSlowMoFeel()
        {
            _slowMoFeedback?.PlayFeedbacks();
        }

        private void StopSlowMoFeel()
        {
            _slowMoFeedback?.StopFeedbacks();
        }
    }
}