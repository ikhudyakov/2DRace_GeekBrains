using Garage;
using Model.Analytic;
using Tools;

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
        }

        public SubscriptionProperty<int> Gold { get; }
        public SubscriptionProperty<int> NoADS { get; }
        public Car CurrentCar { get; }

        public SubscriptionProperty<GameState> State { get; }

        public IAnalyticTools Analytic { get; }

        public ShopTools ShopTools { get; }
    }
}