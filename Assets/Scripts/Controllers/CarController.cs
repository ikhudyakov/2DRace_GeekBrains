using Ability;
using Model;
using Tools;
using UnityEngine;
using Views;

namespace Controllers
{
    public class CarController : BaseController, IAbilityActivator
    {
        private ResourcePath carPath = new ResourcePath() { Path = "Prefabs/Car" };

        public CarController(PlayerData model)
        {
            var go = ResourceLoader.LoadGameObject(carPath);
            var car = GameObject.Instantiate(go).GetComponent<CarView>();
            AddGameObjects(car.gameObject);
        }

        public GameObject GetViewObject()
        {
            throw new System.NotImplementedException();
        }
    }
}