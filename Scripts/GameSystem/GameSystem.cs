using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KCoreKit
{
    
    public class GameSystem : MonoBehaviour
    {
        private static GameSubSystemBase[] _subSystems;
        private static bool _isRunning = false;

        public void Awake()
        {
            _subSystems = GetComponentsInChildren<GameSubSystemBase>();
            foreach (var subSystem in _subSystems)
            {
                subSystem.Setup(this);
            }
        }

        public void Start()
        {
            StartCoroutine(Run());
        }

        public T GetSubSystem<T>() where T : class
        {
            return Array.Find(_subSystems, s => s is T) as T;
        }
        
        public static IEnumerator Run()
        {
            foreach (var subSystem in _subSystems)
            {
                subSystem.OnInitialize();
            }

            _isRunning = true;
            
            while (_isRunning)
            {
                foreach (var subSystem in _subSystems)
                {
                   subSystem.OnUpdate();
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }
}