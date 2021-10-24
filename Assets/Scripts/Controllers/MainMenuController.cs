using Model;
using Tools;
using Tools.Ads;
using UnityEngine;
using Views;

namespace Controllers
{
    public class MainMenuController : BaseController
    {
        private readonly PlayerData _model;
        private readonly IAdsShower _adsShower;
        private ResourcePath mainMenuPath = new ResourcePath() { Path = "Prefabs/mainMenu" };
        private ResourcePath trailTouchViewPath = new ResourcePath() { Path = "Prefabs/trailTouchView" };
        private IShop _shop;
        private MainMenuView _view;

        public MainMenuController(Transform canvasParent, PlayerData model, IAdsShower adsShower, IShop shop)
        {
            _model = model;
            _adsShower = adsShower;
            _shop = shop;
            _view = CreateView(canvasParent);
            AddGameObjects(_view.gameObject);
            var trailView = CreateTrailTouchView(canvasParent);
            trailView.Init();
            _view.Init(StartGame, ShowAddRequested, _model, PurchaseRequested);
            _model.Gold.Subscribe(OnGoldChanged);
            _view.UpdateGold(_model.Gold.Value);
            _model.NoADS.Subscribe(OnNoADSChanged);
            _view.UpdateNoADS(_model.NoADS.Value);
        }

        private void OnNoADSChanged(int value)
        {
            _view.UpdateNoADS(value);
            Debug.Log($"NoADS changed on: {value}");
        }

        private void OnGoldChanged(int value)
        {
            _view.UpdateGold(value);
            Debug.Log($"Gold changed on: {value}");
        }

        private void PurchaseRequested(string productId)
        {
            _shop.Buy(productId);
        }

        private void StartGame()
        {
            ShowAddRequested();
            _model.State.Value = GameState.Game;
        }

        private MainMenuView CreateView(Transform parent)
        {
            var go = ResourceLoader.LoadGameObject(mainMenuPath);
            var viewGo = GameObject.Instantiate(go, parent);
            var view = viewGo.GetComponent<MainMenuView>();
            return view;
        }

        private TrailTouchView CreateTrailTouchView(Transform parent)
        {
            var go = ResourceLoader.LoadGameObject(trailTouchViewPath);
            var viewGo = GameObject.Instantiate(go, parent);
            var view = viewGo.GetComponent<TrailTouchView>();
            return view;
        }

        private void ShowAddRequested()
        {
            if(_model.NoADS.Value == 0)
                _adsShower.ShowVideo(OnVideoShowSucces);
        }

        private void OnVideoShowSucces()
        {
            Debug.Log("Success");
        }
    }
}