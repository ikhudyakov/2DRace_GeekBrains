using System;
using UnityEngine;

public class MainMenuController : BaseController
{
    private readonly PlayerData _model;
    private ResourcePath mainMenuPath = new ResourcePath() { Path = "Prefabs/mainMenu" };

    public MainMenuController(Transform canvasParent, PlayerData model)
    {
        _model = model;
        var view = CreateView(canvasParent);
        AddGameObjects(view.gameObject);
        view.Start += OnStart;
    }

    private void OnStart()
    {
        _model.State.Value = GameState.Game;
    }

    private MainMenuView CreateView(Transform parent)
    {
        var go = ResourceLoader.LoadGameObject(mainMenuPath);
        var viewGo = GameObject.Instantiate(go, parent);
        var view = viewGo.GetComponent<MainMenuView>();
        return view;

    }
}
