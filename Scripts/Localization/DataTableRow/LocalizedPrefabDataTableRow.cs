using UnityEngine;

namespace KCoreKit
{
    public class LocalizedPrefabDataTableRow : LocalizedDataTableRowBase<GameObject>
    {
        public GameObject EN;
        public GameObject KR;
        public GameObject JP;
        public GameObject CN;

        public override GameObject Get(Language language)
        {
            return language switch
            {
                Language.EN => EN,
                Language.KR => KR,
                Language.JP => JP,
                Language.CN => CN,
                _ => EN
            };
        }
    }
}