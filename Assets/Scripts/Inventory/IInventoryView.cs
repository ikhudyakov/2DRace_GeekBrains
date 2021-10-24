using Items;
using System.Collections.Generic;

namespace Inventory
{
    public interface IInventoryView
    {
        void Display(IReadOnlyList<IItem> items);
    }
}
