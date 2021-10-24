using Items;
using System.Collections.Generic;

namespace Inventory
{
    public interface IInvetoryModel
    {
        IReadOnlyList<IItem> GetEquippedItems();
        void EquipItem(IItem item);
        void UnEquipItem(IItem item);
    }
}
