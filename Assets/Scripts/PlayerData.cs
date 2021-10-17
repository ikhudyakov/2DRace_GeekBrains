using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public PlayerData(Car car)
    {
        CurrentCar = car;
    }

    public Car CurrentCar { get; }

    public SubscriptionProperty<GameState> State { get; set; } = new SubscriptionProperty<GameState>();
}
