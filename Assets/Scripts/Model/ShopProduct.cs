using System;
using UnityEngine.Purchasing;

namespace Model
{
    [Serializable]
    public class ShopProduct
    {
        public string Id;
        public ProductType CurrentProductType;
        public ProductModification Modification;
    }
}