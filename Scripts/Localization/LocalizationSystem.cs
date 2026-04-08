using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KCoreKit
{
    public enum Language
    {
        EN,
        KR,
        JP,
        CN
    }

    public class LocalizationSystem : GameSubSystemBase
    {
        [SerializeField] private Language defaultLanguage = Language.EN;

        private Language _language;

        private List<LocalizedTextDataTableRow> _textDataTableRows;
        private List<LocalizedFontDataTableRow> _fontDataTableRows;
        private List<LocalizedSpriteDataTableRow> _spriteDataTableRows;
        private List<LocalizedPrefabDataTableRow> _prefabDataTableRows;

        public Action OnChange;

        public override IEnumerator OnInitialize()
        {
            var dataTableSystem = GameSystem.GetSubSystem<DataTableSystem>();
            _textDataTableRows = dataTableSystem?.FindAllRows<LocalizedTextDataTableRow>();
            _fontDataTableRows = dataTableSystem?.FindAllRows<LocalizedFontDataTableRow>();
            _spriteDataTableRows = dataTableSystem?.FindAllRows<LocalizedSpriteDataTableRow>();
            _prefabDataTableRows = dataTableSystem?.FindAllRows<LocalizedPrefabDataTableRow>();
            SetLanguage(defaultLanguage);
            yield return null;
        }

        public void SetLanguage(Language language)
        {
            _language = language;
            OnChange?.Invoke();
        }

        public Language GetLanguage()
        {
            return _language;
        }

        public string GetLocalizedText(string key)
        {
            return _textDataTableRows.Find(x => x.id == key).Get(_language);
        }

        public TMP_FontAsset GetFontAsset()
        {
            return _fontDataTableRows.Find(x => x.id == _language.ToString()).fontAsset;
        }

        public Sprite GetLocalizedSprite(string key)
        {
            return _spriteDataTableRows.Find(x => x.id == key).Get(_language);
        }

        public GameObject GetLocalizedPrefab(string key)
        {
            return _prefabDataTableRows.Find(x => x.id == key).Get(_language);
        }
    }
}