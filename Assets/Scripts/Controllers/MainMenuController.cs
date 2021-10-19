using System;
using UnityEngine;

public class MainMenuController : BaseController
{
    private readonly PlayerData _model;
    private readonly IAdsShower _adsShower;
    private ResourcePath mainMenuPath = new ResourcePath() { Path = "Prefabs/mainMenu" };
    private ResourcePath trailTouchViewPath = new ResourcePath() { Path = "Prefabs/trailTouchView" };
    private ShopTools _shopTools;

    public MainMenuController(Transform canvasParent, PlayerData model, IAdsShower adsShower, ShopTools shopTools)
    {
        _model = model;
        _adsShower = adsShower;
        _shopTools = shopTools;
        var view = CreateView(canvasParent);
        AddGameObjects(view.gameObject);
        var trailView = CreateTrailTouchView(canvasParent);
        trailView.Init();

        view.Init(StartGame, ShowAddRequested, _model);
    }

    private void StartGame()
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
    
    private TrailTouchView CreateTrailTouchView(Transform parent)
    {
        var go = ResourceLoader.LoadGameObject(trailTouchViewPath);
        var viewGo = GameObject.Instantiate(go, parent);
        var view = viewGo.GetComponent<TrailTouchView>();
        return view;
    }

    private void SuccessPurchase()
    {

    }

    private void ShowAddRequested()
    {
        _adsShower.ShowVideo(OnVideoShowSucces);
    }

    private void OnVideoShowSucces()
    {
        Debug.Log("Success");
    }
}
