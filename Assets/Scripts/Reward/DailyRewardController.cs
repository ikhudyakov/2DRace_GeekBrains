using Controllers;
using Model;
using Reward.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using Views;

namespace Reward.Controllers
{
    public class DailyRewardController : BaseController
    {
        private readonly DailyRewardView _dailyRewardView;
        private PlayerData _model;
        private List<SlotRewardView> _slots;

        private bool _rewardReceived = false;
        private int _timer = 0;

        public DailyRewardController(PlayerData model, ResourcePath rewardViewResource, Transform placeForUi)
        {
            var go = ResourceLoader.LoadGameObject(rewardViewResource);
            var prefab = go.GetComponent<DailyRewardView>();
            _dailyRewardView = GameObject.Instantiate(prefab, placeForUi);
            AddGameObjects(_dailyRewardView.gameObject);
            RefreshView();
            _model = model;
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
            _timer = 0;
            if (_dailyRewardView.LastRewardTime.HasValue)
            {
                var timeSpan = DateTime.UtcNow - _dailyRewardView.LastRewardTime.Value;
                if (timeSpan.Seconds > _dailyRewardView.TimeDeadline)
                {
                    _dailyRewardView.LastRewardTime = null;
                    _dailyRewardView.CurrentActiveSlot = 0;
                }
                else if (timeSpan.Seconds < _dailyRewardView.TimeCooldown)
                {
                    _rewardReceived = true;
                    _timer = _dailyRewardView.TimeCooldown - timeSpan.Seconds;
                }
            }
        }

        private void RefreshUi()
        {
            _dailyRewardView.RewardTimer.text = $"Time new award:  {_timer.ToString()}";
            _dailyRewardView.GetRewardButton.interactable = !_rewardReceived;

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
            RefreshRewardsState();

        }
    }
}