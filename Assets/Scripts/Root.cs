using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Root : MonoBehaviour
{
    [SerializeField] private Transform UiRoot;
    [SerializeField] private UnityAdsTools _adsTools;

    private PlayerData _model;
    private MainController _mainController;
    private ShopTools _shopTools;


    private void Awake()
    {
        var analytics = new UnityAnalyticTools();
        _model = new PlayerData(1.0f, analytics);
        _shopTools = new ShopTools(new List<ShopProduct>());
        _mainController = new MainController(_model, UiRoot, _adsTools, _shopTools);
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
