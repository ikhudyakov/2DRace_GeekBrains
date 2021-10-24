using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Model;

namespace Views
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _showRewardedButton;
        [SerializeField] private Button _buyGoldButton;
        [SerializeField] private Button _noAdsButton;
        [SerializeField] private Text _goldAmount;
        [SerializeField] private Text _noAds;

        public void Init(UnityAction startGame, UnityAction rewardAdRequested, PlayerData model, UnityAction<string> purchaseRequested)
        {
            _startButton?.onClick.AddListener(startGame);
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

        protected void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _showRewardedButton.onClick.RemoveAllListeners();
            _buyGoldButton.onClick.RemoveAllListeners();
            _noAdsButton.onClick.RemoveAllListeners();
        }

        public void UpdateGold(int goldValue)
        {
            if(_goldAmount != null)
                _goldAmount.text = goldValue.ToString();
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