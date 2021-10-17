public class GameController : BaseController
{
    public GameController(PlayerData model)
    {
        var leftMoveDiff = new SubscriptionProperty<float>();
        var rightMoveDiff = new SubscriptionProperty<float>();

        var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
        AddController(tapeBackgroundController);

        var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, model.CurrentCar);
        AddController(inputGameController);

        var carController = new CarController(model);
        AddController(carController);
    }
}
