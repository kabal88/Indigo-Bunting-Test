using System;
using UI;
using UnityEngine;

public class MainUi : MonoBehaviour
{
    public event Action RestartButtonClicked
    {
        add => _restartButton.Button.onClick.AddListener(() => value());
        remove => _restartButton.Button.onClick.RemoveListener(() => value());
    }

    private LevelWidget _levelWidget;
    private MoneyWidget _moneyWidget;
    private RestartButton _restartButton;

    public void Init()
    {
        _levelWidget = GetComponentInChildren<LevelWidget>();
        _moneyWidget = GetComponentInChildren<MoneyWidget>();
        _restartButton = GetComponentInChildren<RestartButton>();
        _restartButton.Init();
    }

    public void SetLevel(int value)
    {
        _levelWidget.SetText($"Level {value}");
    }

    public void SetMoney(int value)
    {
        _moneyWidget.SetText($"$ {value}");
    }
}