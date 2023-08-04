using UnityEngine;

public class MainUi : MonoBehaviour
{
    private LevelWidget _levelWidget;
    private MoneyWidget _moneyWidget;

    public void Init()
    {
        _levelWidget = GetComponentInChildren<LevelWidget>();
        _moneyWidget = GetComponentInChildren<MoneyWidget>();
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