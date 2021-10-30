using Ability;
using Garage;
using Inventory;
using Model;
using System;
using System.Collections.Generic;
using Tools;
using Tools.Ads;
using UnityEngine;

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
        private InventoryController _inventoryController;
        private IShop _shop;
        private readonly List<ItemConfig> _itemConfig;
        private readonly List<UpgradeItemConfig> _upgradeItemConfig;

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
        }

        private void OnStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.None:
                    break;
                case GameState.Start:
                    _gameController?.Dispose();
                    _menuController = new MainMenuController(_uiRoot, _model, _adsShower, _shop, _upgradeItemConfig);
                    AddController(_menuController);
                    _gameController = null;
                    break;
                case GameState.Game:
                    _inventoryController = new InventoryController(_itemConfig);
                    _inventoryController.ShowInventory();
                    AddController(_inventoryController);
                    _model.Analytic.SendMessage("StartGameActivity", new Dictionary<string, object>());
                    _menuController?.Dispose();
                    _gameController = new GameController(_model);
                    AddController(_gameController);
                    _menuController = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}