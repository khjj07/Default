using System.Collections.Generic;
using KCoreKit;
using TMPro;
using UnityEngine;

namespace KCoreKit
{
    public class LocalizedSpriteDataTableRow : LocalizedDataTableRowBase<Sprite>
    {
        public Texture2D EN;
        public Texture2D KR;
        public Texture2D JP;
        public Texture2D CN;
        
        public override Sprite Get(Language language)
        {
            return language switch
            {
                Language.EN => EN.ToSprite(),
                Language.KR => KR.ToSprite(),
                Language.JP => JP.ToSprite(),
                Language.CN => CN.ToSprite(),
                _ => EN.ToSprite()
            };
        }
    }
}