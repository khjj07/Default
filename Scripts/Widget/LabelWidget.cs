using TMPro;
using UnityEngine;

namespace Default
{
    public class LabelWidget : WidgetBase
    {
        [HideInInspector]
        public TMP_Text label =>GetComponentInChildren<TextMeshProUGUI>(true);
    }
}