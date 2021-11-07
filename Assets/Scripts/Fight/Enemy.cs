using System;
using UnityEngine;

public class Enemy : IEnemy
{
    private const int MaxHealthPlyer = 20;
    private float KCoins = 5;
    private float KPower = 1.5f;

    private string _title;

    private int _moneyPlayer;
    private int _powerPlayer;
    private int _healthPlayer;
    private int _crimePlayer;

    public Action<Enemy> OnUpdate;

    public Enemy(string title)
    {
        _title = title;
    }

    public int Power
    {
        get
        {
            var kHealth = _healthPlayer > MaxHealthPlyer ? 100 : 5;
            var power = Mathf.FloorToInt(_moneyPlayer / KCoins + kHealth + _powerPlayer / KPower);
            return power;
        }
    }

    public void Update(DataPlayer data, DataType type)
    {
        switch (type)
        {
            case DataType.Health:
                _healthPlayer = data.Health;
                break;
            case DataType.Power:
                _powerPlayer = data.Power;
                break;
            case DataType.Money:
                _moneyPlayer = data.Money;
                break;
            case DataType.Crime:
                _crimePlayer = data.Crime;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        Debug.Log($"Notified {_title}, change to {data}");
        OnUpdate?.Invoke(this);
    }
}
