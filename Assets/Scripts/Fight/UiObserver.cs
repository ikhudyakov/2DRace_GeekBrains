using TMPro;
using UnityEngine.UI;

public class UiObserver : IEnemy
{
    private readonly TMP_Text _health;
    private readonly TMP_Text _power;
    private readonly TMP_Text _money;
    private readonly TMP_Text _crime;
    private readonly Button _pass;

    public UiObserver(TMP_Text health, TMP_Text power, TMP_Text money, TMP_Text crime, Button pass)
    {
        _health = health;
        _power = power;
        _money = money;
        _crime = crime;
        _pass = pass;
    }

    public void Update(DataPlayer data, DataType type)
    {
        switch (type)
        {
            case DataType.Health:
                if (_health != null)
                    _health.text = $"Player Health: {data.Health}";
                break;
            case DataType.Power:
                if (_power != null)
                    _power.text = $"Player Power: {data.Power}";
                break;
            case DataType.Money:
                if (_money != null)
                    _money.text = $"Player Money: {data.Money}";
                break;
            case DataType.Crime:
                if (_crime != null)
                {
                    _crime.text = $"Player Crime: {data.Crime}";
                    _pass.gameObject.SetActive(data.Crime > 2 ? false : true);
                }
                break;
        }
    }
}