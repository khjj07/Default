using UnityEngine;
using UnityEngine.UI;

namespace Default
{
    [RequireComponent(typeof(Button))]
    public class ButtonWidget : BoxWidget
    {
        [HideInInspector]
        public Button button => GetComponent<Button>();
        
    }
}