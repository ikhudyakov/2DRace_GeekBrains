using Inventory;
using Items;
using System.Collections.Generic;

namespace Controllers
{
    internal class InventoryController : BaseController, IInventoryController
    {
        private readonly IInvetoryModel _inventoryModel;
        private readonly IItemsRepository _itemsRepository;
        private readonly IInventoryView _inventoryView;
        public InventoryController(List<ItemConfig> itemConfigs)
        {
            _inventoryModel = new InventoryModel();
            _itemsRepository = new ItemRepository(itemConfigs);
            _inventoryView = new InventoryView();
        }

        public void ShowInventory()
        {
            foreach (var item in _itemsRepository.Items.Values)
            {
                _inventoryModel.EquipItem(item);
            }
            var equippedItems = _inventoryModel.GetEquippedItems();
            _inventoryView.Display(equippedItems);
        }
    }
}