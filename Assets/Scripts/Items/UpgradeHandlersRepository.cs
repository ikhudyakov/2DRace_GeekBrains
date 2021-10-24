using Controllers;
using Inventory;
using System;
using System.Collections.Generic;

namespace Items
{
    public class UpgradeHandlersRepository : BaseController
    {
        private Dictionary<int, IUpgradeCarHandler> _upgradeItemsMapById = new Dictionary<int, IUpgradeCarHandler>();
        public IReadOnlyDictionary<int, IUpgradeCarHandler> UpgradeItems => _upgradeItemsMapById;

        public UpgradeHandlersRepository(List<UpgradeItemConfig> upgradeItemConfigs)
        {
            PopulateItems(ref _upgradeItemsMapById, upgradeItemConfigs);
        }

        private void PopulateItems(ref Dictionary<int, IUpgradeCarHandler> upgradeItemsMapByType, List<UpgradeItemConfig> configs)
        {
            foreach (var config in configs)
            {
                if (upgradeItemsMapByType.ContainsKey(config.Id))
                    continue;
                upgradeItemsMapByType.Add(config.Id, CreateHandlerByType(config));
            }
        }

        private IUpgradeCarHandler CreateHandlerByType(UpgradeItemConfig config)
        {
            switch (config.UpgradeType)
            {
                case UpgradeType.Speed:
                    return new SpeedUpgradeCarHandler(config.ValueUpgrade);
                default:
                    return StubUpgradeCarHandler.Default;
            }
        }

        protected override void OnDispose()
        {
            _upgradeItemsMapById.Clear();
            _upgradeItemsMapById = null;
        }
    }
}
