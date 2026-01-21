using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Default
{
    public class LabelBoxWidget : WidgetBase {
        [HideInInspector]
        public Image image =>GetComponent<Image>();
        
        [HideInInspector]
        public TMP_Text label =>GetComponentInChildren<TextMeshProUGUI>(true);

    }
}