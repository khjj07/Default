using TMPro;
using UnityEngine;

namespace KCoreKit
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizedLabelWidget : LocalizedComponentBase
    {
        private TMP_Text _textComponent;
        public string key;
      
        public void Awake()
        {
            _textComponent = GetComponent<TMP_Text>();
        }
        
        public override void OnChange()
        {
            _textComponent.font = localizationSystem.GetFontAsset();
            _textComponent.text = localizationSystem.GetLocalizedText(key);
        }
        
    }
}