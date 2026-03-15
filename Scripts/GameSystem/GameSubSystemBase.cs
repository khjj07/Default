using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KCoreKit
{
    public abstract class GameSubSystemBase : MonoBehaviour
    {
        protected GameSystem GameSystem;
        public void Setup(GameSystem gameSystem)
        {
            GameSystem = gameSystem;
        }

        public virtual void OnInitialize()
        {
            
        }

        public virtual void OnUpdate()
        {
            
        }
    }
}