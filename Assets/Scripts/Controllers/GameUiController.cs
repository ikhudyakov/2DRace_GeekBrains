using Model;
using Tools;
using UnityEngine;
using Views;

namespace Controllers
{
    public class GameUiController : BaseController
    {
        private readonly Transform _uiRoot;
        private readonly PlayerData _model;
        private ResourcePath path = new ResourcePath() { Path = "Prefabs/GameUI" };
        private GameUiView _view;

        public GameUiController(Transform uiRoot, PlayerData model)
        {
            _uiRoot = uiRoot;
            _model = model;

            var go = ResourceLoader.LoadGameObject(path);
            var prefab = go.GetComponent<GameUiView>();
            _view = GameObject.Instantiate(prefab, uiRoot);
            AddGameObjects(_view.gameObject);
            _view.OnFightRequested += OnFight;
        }

        private void OnFight()
        {
            _model.State.Value = GameState.Fight;
        }
    }
}