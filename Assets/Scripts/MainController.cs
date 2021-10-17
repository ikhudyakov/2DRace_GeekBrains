using System;
using UnityEngine;

public class MainController : BaseController
{
    private readonly PlayerData _model;
    private readonly Transform _uiRoot;
    private MainMenuController _menuController;
    private GameController _gameController;
    public MainController(PlayerData model, Transform uiRoot)
    {
        model.State.Subscribe(OnStateChanged);
        _model = model;
        _uiRoot = uiRoot;
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
                _menuController = new MainMenuController(_uiRoot, _model);
                break;
            case GameState.Game:
                _menuController?.Dispose();
                _menuController = null;
                _gameController = new GameController(_model);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}
