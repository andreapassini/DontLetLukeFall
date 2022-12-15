using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DLLF
{
    public class PlatformBar : MonoBehaviour
    {
        [SerializeField]
        private float _cooldownNewPlatform = 3f;
        private float _timer = 0f;
        [SerializeField]
        private PlatformSlot[] _platformSlots;
        [SerializeField]
        private List<PlatformUI> _platforms = new List<PlatformUI>();

        private PlatformSequence _platformSequence;
        private Queue<int> _platformsQueue = new Queue<int>();
        private LevelManager levelManager;

        private void Awake()
        {
            if (_platformSlots.Length == 0)
            {
                _platformSlots = GameObject.FindObjectsOfType<PlatformSlot>();
            }

            // Check if null, load from Resources
            _platformSequence ??= Resources.Load<PlatformSequence>(
                "LevelsPlatforms/"
                + SceneManager.GetActiveScene().name);
        }

		private void Start()
		{
            ;
        }

		// Update is called once per frame
		void Update()
        {
            if (!AllSlotFull())
            {
                if (_timer > _cooldownNewPlatform)
                {
                    _timer = 0f;
                    CreateNewPlatform();
                }
                else
                {
                    _timer += Time.deltaTime;
                }
            }
            else
            {
                _timer = 0f;
            }
        }

        public void CreateNewPlatform()
        {
            PlatformUI selectedPlatform = _platforms[getNextPlatform()];
            //PlatformUI selectedPlatform = _platforms[UnityEngine.Random.Range(0, _platforms.Count - 1)];
            _platformSlots.Where(s => s.isEmpty).First().GeneratePlatform(selectedPlatform);
        }

        private bool AllSlotFull()
        {
            return _platformSlots.All(s => !s.isEmpty);
        }

        private int getNextPlatform()
        {
            if (_platformsQueue.TryDequeue(out var actionToPerform))
                return actionToPerform;
            
            return 0;
        }

        private int getPlatfromUIFromEnum(PlatformType platformType)
        {
            for(int i = 0; i < _platforms.Count; i++)
            {
                if (_platforms[i].ToString().Equals(platformType.ToString() + "UI")){
                    return i;
                }
            }

            return UnityEngine.Random.Range(0, _platforms.Count - 1);
        }

        public void EnqueuePlatforms(PlatformSequence sequence)
        {
            _platformSequence = sequence;

            foreach(var platform in _platformSequence.platforms)
            {
                int nextIndex = getPlatfromUIFromEnum(platform);

                if (nextIndex == -1)
                    continue;

                _platformsQueue.Enqueue(nextIndex);
            }

            // Full the platform before starting the level
            CreateNewPlatform();
            CreateNewPlatform();
            CreateNewPlatform();
        }
    }
}