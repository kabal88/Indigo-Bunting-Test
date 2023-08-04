using System;

namespace UI.Windows
{
    public class WinWindow : WindowBase
    {
        public event Action NextButtonClicked
        {
            add => _nextButton.Button.onClick.AddListener(() => value());
            remove => _nextButton.Button.onClick.RemoveListener(() => value());
        }
        
        private NextButton _nextButton;
        private void Awake()
        {
            _nextButton = GetComponentInChildren<NextButton>();
            _nextButton.Init();
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}