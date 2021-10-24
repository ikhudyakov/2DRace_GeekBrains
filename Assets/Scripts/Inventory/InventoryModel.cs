using Items;
using System.Collections.Generic;

namespace Inventory
{
    public class InventoryModel : IInvetoryModel
    {
        private readonly List<IItem> _items = new List<IItem>();

        public void EquipItem(IItem item)
        {
            if (_items.Contains(item))
                return;

            _items.Add(item);
        }

        public void UnEquipItem(IItem item)
        {
            if (!_items.Contains(item))
                return;

            _items.Remove(item);
        }

        public IReadOnlyList<IItem> GetEquippedItems()
        {
            return _items;
        }
    }
}
