using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Upgrade item", menuName = "Upgrade item")]
    public class UpgradeItemConfig : ScriptableObject
    {
        [SerializeField]
        private ItemConfig _itemConfig;

        [SerializeField]
        private UpgradeType _upgradeType;

        [SerializeField]
        private float _valueUpgrade;

        public int Id => _itemConfig.Id;

        public ItemConfig ItemConfig => _itemConfig;

        public UpgradeType UpgradeType => _upgradeType;

        public float ValueUpgrade => _valueUpgrade;
    }

    public enum UpgradeType
    {
        None,
        Speed,
        Control
    }
}