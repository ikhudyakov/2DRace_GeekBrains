using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Model;
using Garage;
using DG.Tweening;

namespace Views
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _showRewardedButton;
        [SerializeField] private Button _buyGoldButton;
        [SerializeField] private Button _noAdsButton;
        [SerializeField] private Button _garageButton;
        [SerializeField] private Button _resetPurchases;
        [SerializeField] private Button _dailyRewardButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Text _goldAmount;
        [SerializeField] private Text _noAds;
        [SerializeField] private GarageView _garage;

        public void Init(
            UnityAction startGame, 
            UnityAction rewardAdRequested, 
            PlayerData model, 
            UnityAction<string> purchaseRequested, 
            GarageController garage, 
            UnityAction resetPurchases,
            UnityAction dailyReward,
            UnityAction exit
            )
        {
            _garage = garage.View;
            _garage.gameObject.SetActive(false);

            _startButton?.onClick.AddListener(startGame);
            _garageButton?.onClick.AddListener(OpenGarage);
            _resetPurchases?.onClick.AddListener(resetPurchases);
            _dailyRewardButton?.onClick.AddListener(dailyReward);
            _exitButton?.onClick.AddListener(exit);
            _buyGoldButton?.onClick.AddListener(delegate { purchaseRequested("1_gold"); });

            if (model.NoADS.Value == 0)
            {
                _showRewardedButton?.onClick.AddListener(rewardAdRequested);
                _noAdsButton?.onClick.AddListener(delegate { purchaseRequested("2_noads"); });
            }
            else
            {
                _showRewardedButton.gameObject.SetActive(false);
                _noAdsButton.gameObject.SetActive(false);
            }
        }

        public void OpenGarage()
        {
            _garage.Show();
        }

        protected void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _showRewardedButton.onClick.RemoveAllListeners();
            _buyGoldButton.onClick.RemoveAllListeners();
            _noAdsButton.onClick.RemoveAllListeners();
            _garageButton.onClick.RemoveAllListeners();
            _resetPurchases.onClick.RemoveAllListeners();
            _dailyRewardButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        public void UpdateGold(int goldValue)
        {
            if (_goldAmount != null)
            {
                DOVirtual.Int(int.Parse(_goldAmount.text), goldValue, 2f, (v) =>
                {
                    _goldAmount.text = v.ToString();
                });
            }
        }

        public void UpdateNoADS(int noAdsValue)
        {
            if (_noAds != null)
            {
                _noAds.text = noAdsValue == 0 ? "ADS Active" : "NoADS Active";
                bool active = noAdsValue == 0 ? true : false;
                _noAdsButton.gameObject.SetActive(active);
                _showRewardedButton.gameObject.SetActive(active);
            }
        }
    }
}