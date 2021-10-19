public class PlayerData
{
    public PlayerData(float carSpeed, IAnalyticTools analytic)
    {
        CurrentCar = new Car(carSpeed);
        Analytic = analytic;
        Gold = 0;
    }

    public int Gold { get; set; }
    public Car CurrentCar { get; }

    public SubscriptionProperty<GameState> State { get; set; } = new SubscriptionProperty<GameState>();

    public IAnalyticTools Analytic { get; }

    public ShopTools ShopTools { get; }
}
