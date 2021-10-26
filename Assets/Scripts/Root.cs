using Ability;
using Controllers;
using Inventory;
using Model;
using Model.Analytic;
using System.Collections.Generic;
using Tools;
using Tools.Ads;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Transform UiRoot;
    [SerializeField] private UnityAdsTools _adsTools;
    [SerializeField] private ItemConfig[] _itemsConfigs;
    [SerializeField] private UpgradeItemConfig[] _upgradeItemsConfigs;
    [SerializeField] private List<ShopProduct> _products;

    private PlayerData _model;
    private MainController _mainController;
    private IShop _shop;


    private void Awake()
    {
        var analytics = new UnityAnalyticTools();
        _model = new PlayerData(1.0f, analytics);
        _shop = new ShopTools(_products);
        _mainController = new MainController(
            _model,
            UiRoot,
            _adsTools,
            _shop,
            _products,
            new List<ItemConfig>(_itemsConfigs),
            new List<UpgradeItemConfig>(_upgradeItemsConfigs));
        _model.State.Value = GameState.Start;
        analytics.SendMessage("GameStart", new Dictionary<string, object>());
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
        _mainController = null;
        _model = null;
    }
}
