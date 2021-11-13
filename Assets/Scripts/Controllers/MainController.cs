using Ability;
using Fight.Controllers;
using Garage;
using Inventory;
using Model;
using Reward.Controllers;
using System;
using System.Collections.Generic;
using Tools;
using Tools.Ads;
using UnityEngine;
using Views;

namespace Controllers
{
    public class MainController : BaseController
    {
        private readonly PlayerData _model;
        private readonly Transform _uiRoot;
        private readonly IAdsShower _adsShower;

        private MainMenuController _menuController;
        private PurchaseController _purchaseController;
        private GameController _gameController;
        private DailyRewardController _dailyRewardController;
        private InventoryController _inventoryController;
        private FightController _fightController;
        private IShop _shop;
        private readonly List<ItemConfig> _itemConfig;
        private readonly List<UpgradeItemConfig> _upgradeItemConfig;

        private ResourcePath trailTouchViewPath = new ResourcePath() { Path = "Prefabs/trailTouchView" };
        private ResourcePath FightViewPath = new ResourcePath() { Path = "Prefabs/FightWindowView" };
        private String RewardViewPath = "Prefabs/DailyRewardWindow";

        public MainController(
            PlayerData model, 
            Transform uiRoot, 
            IAdsShower adsShower, 
            IShop shop, 
            List<ShopProduct> products, 
            List<ItemConfig> itemConfig, 
            List<UpgradeItemConfig> upgradeItemConfig)
        {
            model.State.Subscribe(OnStateChanged);
            _model = model;
            _uiRoot = uiRoot;
            _adsShower = adsShower;
            _shop = shop;
            _itemConfig = itemConfig;
            _upgradeItemConfig = upgradeItemConfig;
            _purchaseController = new PurchaseController(products, model, shop);
            var trailView = CreateTrailTouchView(_uiRoot);
            trailView.Init();
        }

        private void OnStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.None:
                    break;
                case GameState.Start:
                    _gameController?.Dispose();
                    _dailyRewardController?.Dispose();
                    _fightController?.Dispose();

                    _menuController = new MainMenuController(_uiRoot, _model, _adsShower, _shop, _upgradeItemConfig);
                    AddController(_menuController);
                    break;
                case GameState.Game:
                    _inventoryController = new InventoryController(_itemConfig);
                    _inventoryController.ShowInventory();
                    AddController(_inventoryController);
                    _model.Analytic.SendMessage("StartGameActivity", new Dictionary<string, object>());

                    _gameController = new GameController(_uiRoot, _model);
                    AddController(_gameController);

                    _menuController?.Dispose();
                    _dailyRewardController?.Dispose();
                    _fightController?.Dispose();
                    break;
                case GameState.Daily:
                    _dailyRewardController = new DailyRewardController(_model, RewardViewPath, _uiRoot);
                    _gameController?.Dispose();
                    _menuController?.Dispose();
                    _fightController?.Dispose();
                    break;
                case GameState.Fight:
                    _menuController?.Dispose();
                    _gameController?.Dispose();
                    _dailyRewardController?.Dispose();
                    _fightController = new FightController(FightViewPath, _model, _uiRoot);
                    break;
                default:
                    _dailyRewardController?.Dispose();
                    _menuController?.Dispose();
                    _gameController?.Dispose();
                    _fightController?.Dispose();
                    break;
            }
        }
        private TrailTouchView CreateTrailTouchView(Transform parent)
        {
            var go = ResourceLoader.LoadGameObject(trailTouchViewPath);
            var viewGo = GameObject.Instantiate(go, parent);
            var view = viewGo.GetComponent<TrailTouchView>();
            return view;
        }
    }
}