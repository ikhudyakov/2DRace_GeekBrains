using Controllers;
using Inventory;
using System.Collections.Generic;

namespace Items
{
    public class ItemRepository : BaseController, IItemsRepository
    {
        private Dictionary<int, IItem> _itemsMapById = new Dictionary<int, IItem>();
        public IReadOnlyDictionary<int, IItem> Items => _itemsMapById;

        public ItemRepository(List<ItemConfig> upgradeItemConfigs)
        {
            PopulateItems(ref _itemsMapById, upgradeItemConfigs);
        }

        private void PopulateItems(ref Dictionary<int, IItem> upgradeHandlersMapByType, List<ItemConfig> configs)
        {
            foreach (var config in configs)
            {
                if (upgradeHandlersMapByType.ContainsKey(config.Id))
                    continue;

                upgradeHandlersMapByType.Add(config.Id, CreateItem(config));
            }
        }

        private IItem CreateItem(ItemConfig config)
        {
            return new Item
            {
                Id = config.Id,
                Info = new ItemInfo { Title = config.Title }
            };
        }

        protected override void OnDispose()
        {
            _itemsMapById.Clear();
            _itemsMapById = null;
        }
    }
}