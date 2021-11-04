using Controllers;
using Inventory;
using Items;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

namespace Garage
{
    public class GarageController : BaseController, IShedController
    {
        private readonly Car _car;

        private readonly UpgradeHandlersRepository _upgradeHandlersRepository;
        private readonly ItemRepository _upgradeItemsRepository;
        private readonly InventoryModel _inventoryModel;
        private ResourcePath garagePath = new ResourcePath() { Path = "Prefabs/garage" };
        private GarageView _view;

        internal GarageView View { get => _view; }

        public GarageController(Transform canvasParent, [NotNull] List<UpgradeItemConfig> upgradeItemConfigs, [NotNull] Car car)
        {
            _car = car;

            _upgradeHandlersRepository = new UpgradeHandlersRepository(upgradeItemConfigs);
            AddController(_upgradeHandlersRepository);

            _upgradeItemsRepository = new ItemRepository(upgradeItemConfigs.Select(value => value.ItemConfig).ToList());
            AddController(_upgradeItemsRepository);

            _inventoryModel = new InventoryModel();
            _inventoryModel.EquipItem(_upgradeItemsRepository.Items[1]);

            _view = CreateView(canvasParent);
            _view.Init(Upgrade);
        }
        private GarageView CreateView(Transform parent)
        {
            var go = ResourceLoader.LoadGameObject(garagePath);
            var viewGo = GameObject.Instantiate(go, parent);
            var view = viewGo.GetComponent<GarageView>();
            return view;
        }


        public void Enter()
        {
            Debug.Log($"Enter: car has speed : {_car.Speed}");
        }

        public void Exit()
        {
            _view.gameObject.SetActive(false);
        }

        public void Upgrade()
        {
            UpgradeCarWithEquippedItems(_car, _inventoryModel.GetEquippedItems(), _upgradeHandlersRepository.UpgradeItems);
            Debug.Log($"Exit: car has speed : {_car.Speed}");
        }

        private void UpgradeCarWithEquippedItems(
            IUpgradableCar upgradableCar, 
            IReadOnlyList<IItem> equippedItems, 
            IReadOnlyDictionary<int, IUpgradeCarHandler> upgradeHandlers)
        {
            foreach (var equippedItem in equippedItems)
            {
                if (upgradeHandlers.TryGetValue(equippedItem.Id, out var handler))
                    handler.Upgrade(upgradableCar);
            }
        }
    }
}