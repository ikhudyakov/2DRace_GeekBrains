using Inventory;
using UnityEngine;

namespace Ability
{
    public class AbilityConfig : ScriptableObject
    {
        [SerializeField] private ItemConfig _item;
        [SerializeField] private float _power;
        [SerializeField] private AbilityType _type;
        [SerializeField] private GameObject _view;

        public int Id => _item.Id;
        public float Power => _power;
        public AbilityType Type => _type;
    }
}
