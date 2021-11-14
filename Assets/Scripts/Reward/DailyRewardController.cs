using Bundle;
using Controllers;
using Model;
using Reward.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Views;

namespace Reward.Controllers
{
    public class DailyRewardController : BaseController
    {
        private DailyRewardView _dailyRewardView;
        private PlayerData _model;
        private readonly Transform _placeForUi;
        private List<SlotRewardView> _slots;

        private bool _rewardReceived = false;
        private int _timer = 0;

        private GameObject _goDailyRewardView;

        public DailyRewardController(PlayerData model, string rewardViewResource, Transform placeForUi)
        {
            Addressables.LoadAssetAsync<GameObject>(rewardViewResource).Completed += OnLoadDone;
            _model = model;
            _placeForUi = placeForUi;
        }

        private void OnLoadDone(AsyncOperationHandle<GameObject> obj)
        {
            _goDailyRewardView = obj.Result;
            _addressablePrefabs.Add(obj);
            var prefab = _goDailyRewardView.GetComponent<DailyRewardView>();
            _dailyRewardView = GameObject.Instantiate(prefab, _placeForUi);
            AddGameObjects(_dailyRewardView.gameObject);
            RefreshView();
        }

        private void RefreshView()
        {
            InitSlots();
            _dailyRewardView.StartCoroutine(UpdateCoroutine());
            RefreshUi();
            SubscribeButtons();
            _dailyRewardView.AwardBar.fillAmount = _dailyRewardView.CurrentActiveSlot / _dailyRewardView.Rewards.Count;
        }

        private IEnumerator UpdateCoroutine()
        {
            while (true)
            {
                Update();
                yield return new WaitForSeconds(1);
            }
        }

        private void Update()
        {
            RefreshRewardsState();
            RefreshUi();
            _dailyRewardView.AwardBar.fillAmount = (float)_dailyRewardView.CurrentActiveSlot / (float)_dailyRewardView.Rewards.Count;
        }

        private void RefreshRewardsState()
        {
            _rewardReceived = false;
            if (_dailyRewardView.LastRewardTime.HasValue)
            {
                var timeSpan = DateTime.UtcNow - _dailyRewardView.LastRewardTime.Value;
                if (timeSpan.Seconds > _dailyRewardView.TimeDeadline)
                {
                    ResetReward();
                }
                else if (timeSpan.Seconds < _dailyRewardView.TimeCooldown)
                {
                    _rewardReceived = true;
                }
            }
        }

        private void RefreshUi()
        {
            _dailyRewardView.GetRewardButton.interactable = !_rewardReceived;

            if (!_rewardReceived)
            {
                _dailyRewardView.RewardTimer.text = "Time to get your reward";
            }
            else
            {
                if (_dailyRewardView.LastRewardTime != null)
                {
                    var nextClaimTime = _dailyRewardView.LastRewardTime.Value.AddSeconds(_dailyRewardView.TimeCooldown);
                    var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
                    var timeGetReward = $"{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";

                    _dailyRewardView.RewardTimer.text = $"Next reward: {timeGetReward}";
                }
            }
            for (int i = 0; i < _dailyRewardView.Rewards.Count; i++)
            {
                _slots[i].SetData(_dailyRewardView.Rewards[i], i + 1, i <= _dailyRewardView.CurrentActiveSlot);
            }
        }

        private void InitSlots()
        {
            _slots = new List<SlotRewardView>();
            for (int i = 0; i < _dailyRewardView.Rewards.Count; i++)
            {
                var reward = _dailyRewardView.Rewards[i];
                var slotInstance = GameObject.Instantiate(_dailyRewardView.SlotPrefab, _dailyRewardView.SlotsParent, false);
                slotInstance.SetData(reward, i + 1, false);
                _slots.Add(slotInstance);
            }
        }

        private void SubscribeButtons()
        {
            _dailyRewardView.GetRewardButton.onClick.AddListener(ClaimReward);
            _dailyRewardView.ResetButton.onClick.AddListener(ResetReward);
            _dailyRewardView.ExitButton.onClick.AddListener(Exit);
        }

        private void Exit()
        {
            _model.State.Value = GameState.Start;
        }

        private void ResetReward()
        {
            _dailyRewardView.LastRewardTime = null;
            _dailyRewardView.CurrentActiveSlot = 0;
            Notification.CancelNotoficationAndroid();
        }

        private void ClaimReward()
        {
            if (_rewardReceived)
                return;
            var reward = _dailyRewardView.Rewards[_dailyRewardView.CurrentActiveSlot];
            switch (reward.Type)
            {
                case RewardType.None:
                    break;
                case RewardType.Gold:
                    CurrencyWindow.Instance.AddGold(reward.Count);
                    break;
                case RewardType.Diamond:
                    CurrencyWindow.Instance.AddDiamond(reward.Count);
                    break;
                default:
                    break;
            }

            _dailyRewardView.LastRewardTime = DateTime.UtcNow;
            _dailyRewardView.CurrentActiveSlot = (_dailyRewardView.CurrentActiveSlot + 1) % _dailyRewardView.Rewards.Count;
            Notification.CancelNotoficationAndroid();
            Notification.CreateNotifications("Get Daily Reward!", "Enter the game and get free crystals");
            RefreshRewardsState();
        }
    }
}