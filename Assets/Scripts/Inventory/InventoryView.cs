using Items;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryView : IInventoryView
    {
        public void Display(IReadOnlyList<IItem> items)
        {
            foreach (var item in items)
            {
                Debug.Log($"Id Item: {item.Id}. Title Item: {item.Info.Title}");
            }
        }
    }
}
