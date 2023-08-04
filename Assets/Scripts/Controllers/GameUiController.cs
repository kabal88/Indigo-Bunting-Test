using System;
using System.Collections.Generic;
using Views;

namespace Controllers
{
    public class GameUIController : IDisposable
    {
        public event Action NextButtonClicked
        {
            add => _view.NextButtonClicked += value;
            remove => _view.NextButtonClicked -= value;
        }

        public event Action RestartButtonClicked
        {
            add => _view.RestartButtonClicked += value;
            remove => _view.RestartButtonClicked += value;
        }

        private GameUIView _view;

        public GameUIController(GameUIView view)
        {
            _view = view;
            _view.Init();
        }

        public void HideAllWindows()
        {
            _view.HideAllWindows();
        }

        public void OpenWindow(int windowId)
        {
            _view.OpenWindow(windowId);
        }

        public void CloseWindow(int windowId)
        {
            _view.CloseWindow(windowId);
        }

        public void SetLevel(int value)
        {
            _view.SetLevel(value + 1);
        }

        public void SetUIActive(bool isOn)
        {
            _view.SetActive(isOn);
        }

        public void Dispose()
        {
        }

        public void SetMoney(int value)
        {
            _view.SetMoney(value);
        }
    }
}