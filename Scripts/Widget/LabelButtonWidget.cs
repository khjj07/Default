using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KCoreKit
{
    [RequireComponent(typeof(Button))]
    public class LabelButtonWidget : LabelWidget
    {
        [HideInInspector]
        public Button button => GetComponent<Button>();
        
        public void AddOnClickAction(UnityAction action)
        {
            button.onClick.AddListener(action);
        }
    }
}