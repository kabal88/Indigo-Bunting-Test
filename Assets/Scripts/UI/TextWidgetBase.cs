using TMPro;
using UnityEngine;

namespace UI
{
    public class TextWidgetBase : MonoBehaviour
    {
        protected TextMeshProUGUI Text;

        private void Awake()
        {
            Text = GetComponent<TextMeshProUGUI>();
        }

        public virtual void SetText(string text)
        {
            Text.text = text;
        }
    }
}