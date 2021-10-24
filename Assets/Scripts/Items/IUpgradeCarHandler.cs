using Garage;

namespace Items
{
    public interface IUpgradeCarHandler
    {
        IUpgradableCar Upgrade(IUpgradableCar upgradableCar);
    }
}
