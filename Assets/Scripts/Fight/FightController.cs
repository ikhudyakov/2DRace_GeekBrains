using Controllers;
using Fight.Views;
using Model;
using Tools;
using UnityEngine;

namespace Fight.Controllers
{
    public class FightController : BaseController
    {
        private readonly ResourcePath _fightResource;
        private readonly PlayerData _model;
        private FightWindowView _fightWindowView;

        public FightController(ResourcePath fightResource, PlayerData model, Transform uiRoot)
        {
            _fightResource = fightResource;
            _model = model;

            var go = ResourceLoader.LoadGameObject(_fightResource);
            var prefab = go.GetComponent<FightWindowView>();
            _fightWindowView = GameObject.Instantiate(prefab, uiRoot);
            AddGameObjects(_fightWindowView.gameObject);
            _fightWindowView.ExitRequested += Exit;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            if (_fightWindowView != null)
                _fightWindowView.ExitRequested -= Exit;
        }

        private void Exit()
        {
            _model.State.Value = GameState.Game;
        }
    }
}