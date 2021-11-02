using Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers
{
    public class PurchaseController
    {
        private List<ShopProduct> _products;
        private PlayerData _model;
        private IShop _shop;

        public PurchaseController(List<ShopProduct> products, PlayerData model, IShop shop)
        {
            _products = products;
            _model = model;
            _shop = shop;

            _shop.OnSuccessPurchase.Subscribe(ApplyProductModifications);
        }

        private void ApplyProductModifications(string productId)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return;
            var modification = product.Modification;
            switch (modification.Type)
            {
                case ModificationType.None:
                    break;
                case ModificationType.Gold:
                    _model.Gold.Value += modification.Value;
                    PlayerPrefs.SetInt("Gold", _model.Gold.Value);
                    break;
                case ModificationType.NoADS:
                    _model.NoADS.Value += modification.Value;
                    PlayerPrefs.SetInt("NoADS", _model.NoADS.Value);
                    break;
                default:
                    break;
            }
        }
    }
}