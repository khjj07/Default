using UnityEngine;

namespace KCoreKit
{
    public abstract class LocalizedComponentBase :  MonoBehaviour
    {  
        
        protected LocalizationSystem localizationSystem;

        public void Start()
        {
            localizationSystem = GameSystem.GetInstance().GetSubSystem<LocalizationSystem>();
            localizationSystem.OnChange += OnChange;
        }

        public abstract void OnChange();
    }
}