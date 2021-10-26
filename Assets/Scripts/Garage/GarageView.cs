using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Garage
{
    class GarageView : MonoBehaviour
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _exitButton;

        public void Init(UnityAction upgradeCar, UnityAction exitGarage)
        {
            _upgradeButton?.onClick.AddListener(upgradeCar);
            _exitButton?.onClick.AddListener(exitGarage);
        }

        protected void OnDestroy()
        {
            _upgradeButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }
    }
}
