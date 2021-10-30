using System.Collections.Generic;

public class DataPlayer
{
    private string _titleData;

    private int _countMoney;
    private int _countHealth;
    private int _countPower;
    private int _countCrime;

    private List<IEnemy> _listeners = new List<IEnemy>();

    public DataPlayer(string titleData)
    {
        _titleData = titleData;
    }

    public string TitleData => _titleData;

    public int Money 
    { 
        get => _countMoney;
        set
        {
            if (_countMoney != value)
            {
                if (value >= 0)
                {
                    _countMoney = value;
                    Notify(DataType.Money);
                }
            }
        }
    }

    public int Health
    {
        get => _countHealth;
        set
        {
            if (_countHealth != value)
            {
                if (value >= 0)
                {
                    _countHealth = value;
                    Notify(DataType.Health);
                }
            }
        }
    }

    public int Power
    {
        get => _countPower;
        set
        {
            if (_countPower != value)
            {
                if (value >= 0)
                {
                    _countPower = value;
                    Notify(DataType.Power);
                }
            }
        }
    }
    
    public int Crime
    {
        get => _countCrime;
        set
        {
            if (_countCrime != value)
            {
                if (value >= 0 && value <= 5)
                {
                    _countCrime = value;
                    Notify(DataType.Crime);
                }
            }
        }
    }

    public void Subscribe(IEnemy enemy)
    {
        _listeners.Add(enemy);
    }

    public void Unsubscribe(IEnemy enemy)
    {
        _listeners.Remove(enemy);
    }

    private void Notify(DataType type)
    {
        foreach (var enemy in _listeners)
        {
            enemy.Update(this, type);
        }
    }
}
