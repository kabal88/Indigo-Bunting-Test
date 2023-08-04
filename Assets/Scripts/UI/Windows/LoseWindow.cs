using System;

namespace UI.Windows
{
    public class LoseWindow : WindowBase
    {
        public event Action RestartButtonClicked
        {
            add => _restartButton.Button.onClick.AddListener(() => value());
            remove => _restartButton.Button.onClick.RemoveListener(() => value());
        }
        
        private RestartButton _restartButton;
        private void Awake()
        {
            _restartButton = GetComponentInChildren<RestartButton>();
            _restartButton.Init();
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