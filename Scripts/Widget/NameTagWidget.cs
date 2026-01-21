using TMPro;
using UnityEngine;

namespace Default
{
    public class NameTagWidget : FollowTransformWidget
    {
        [HideInInspector]
        public TMP_Text label =>GetComponentInChildren<TextMeshProUGUI>(true);

    }
}