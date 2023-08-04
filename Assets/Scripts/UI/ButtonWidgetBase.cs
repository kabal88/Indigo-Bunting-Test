using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonWidgetBase : MonoBehaviour
    {
        protected Button _button;

        public Button Button => _button;

        public void Init()
        {
            _button = GetComponent<Button>();
        }
    }
}