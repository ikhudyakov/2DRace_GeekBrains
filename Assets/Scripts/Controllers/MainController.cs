using System;
using System.Collections.Generic;
using UnityEngine;

public class MainController : BaseController
{
    private readonly PlayerData _model;
    private readonly Transform _uiRoot;
    private readonly IAdsShower _adsShower;
    private MainMenuController _menuController;
    private GameController _gameController;
    private ShopTools _shopTools;
    public MainController(PlayerData model, Transform uiRoot, IAdsShower adsShower, ShopTools shopTools)
    {
        model.State.Subscribe(OnStateChanged);
        _model = model;
        _uiRoot = uiRoot;
        _adsShower = adsShower;
        _shopTools = shopTools;
    }

    private void OnStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.None:
                break;
            case GameState.Start:
                _gameController?.Dispose();
                _gameController = null;
                _menuController = new MainMenuController(_uiRoot, _model, _adsShower, _shopTools);
                break;
            case GameState.Game:
                _model.Analytic.SendMessage("StartGameActivity", new Dictionary<string, object>());
                _menuController?.Dispose();
                _menuController = null;
                _gameController = new GameController(_model);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}
