using Ability;
using Model;
using Tools;
using UnityEngine;

namespace Controllers
{
    public class GameController : BaseController
    {
        public GameController(Transform uiRoot, PlayerData model)
        {
            var leftMoveDiff = new SubscriptionProperty<float>();
            var rightMoveDiff = new SubscriptionProperty<float>();

            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, model.CurrentCar);
            AddController(inputGameController);

            var carController = new CarController(model);
            AddController(carController);

            var gameUiController = new GameUiController(uiRoot, model);
            AddController(gameUiController);
        }
    }
}