using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DLLF
{
    public class PlatformBar : MonoBehaviour
    {
        [SerializeField]
        private float _cooldownNewPlatform = 3f;
        private float _timer = 0f;
        [SerializeField]
        private PlatformSequence _platformSequence;
        [SerializeField]
        private PlatformSlot[] _platformSlots;
        [SerializeField]
        private List<PlatformUI> _platforms = new List<PlatformUI>();

        private void Awake()
        {
            if (_platformSlots.Length == 0)
            {
                _platformSlots = GameObject.FindObjectsOfType<PlatformSlot>();
            }
        }

		private void Start()
		{
            // Full the platform before starting the level
            CreateNewPlatform();
            CreateNewPlatform();
            CreateNewPlatform();
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

        private void CreateNewPlatform()
        {
            PlatformUI selectedPlatform = _platforms[UnityEngine.Random.Range(0, _platforms.Count - 1)];
            _platformSlots.Where(s => s.isEmpty).First().GeneratePlatform(selectedPlatform);
        }

        private bool AllSlotFull()
        {
            return _platformSlots.All(s => !s.isEmpty);
        }


    }
}