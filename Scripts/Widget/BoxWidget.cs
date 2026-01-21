using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Default
{
    [RequireComponent(typeof(Image))]
    public class BoxWidget : WidgetBase
    {
        [HideInInspector]
        public Image image =>GetComponent<Image>();
        [HideInInspector]
        public TMP_Text label =>GetComponentInChildren<TextMeshProUGUI>(true);
    }
}