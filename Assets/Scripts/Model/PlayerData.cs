using Garage;
using Model.Analytic;
using Tools;
using UnityEngine;

namespace Model
{
    public class PlayerData
    {
        public PlayerData(float carSpeed, IAnalyticTools analytic)
        {
            State = new SubscriptionProperty<GameState>();
            CurrentCar = new Car(carSpeed);
            Analytic = analytic;
            Gold = new SubscriptionProperty<int>();
            NoADS = new SubscriptionProperty<int>();

            if (PlayerPrefs.HasKey("Gold"))
            {
                Gold.Value = PlayerPrefs.GetInt("Gold");
            }
            
            if (PlayerPrefs.HasKey("NoADS"))
            {
                NoADS.Value = PlayerPrefs.GetInt("NoADS");
            }
        }

        public SubscriptionProperty<int> Gold { get; }
        public SubscriptionProperty<int> NoADS { get; }
        public Car CurrentCar { get; }

        public SubscriptionProperty<GameState> State { get; }

        public IAnalyticTools Analytic { get; }

        public ShopTools ShopTools { get; }
    }
}