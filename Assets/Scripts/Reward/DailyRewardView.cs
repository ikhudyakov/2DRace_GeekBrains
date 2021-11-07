using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Reward.Views
{
    public class DailyRewardView : MonoBehaviour
    {
        private const string LastTimeKey = "LastRewardTime";
        private const string ActiveSlotKey = "ActiveSlot";

        #region Fields
        [Header("Time Settings")]
        [SerializeField]
        private int _timeCooldown = 86400;
        [SerializeField]
        private int _timeDeadline = 172800;

        [Space]
        [Header("Reward Settings")]
        [SerializeField]
        private List<Reward> _rewards;

        [Header("UI")]
        [SerializeField]
        private TMP_Text _rewardTimer;
        [SerializeField]
        private Transform _slotsParent;
        [SerializeField]
        private SlotRewardView _slotPrefab;
        [SerializeField]
        private Button _getRewardButton;
        [SerializeField]
        private Button _resetButton;
        [SerializeField]
        private Button _exitButton;
        [SerializeField]
        private Image _awardBar;
        #endregion

        public int TimeCooldown => _timeCooldown;
        public int TimeDeadline => _timeDeadline;
        public List<Reward> Rewards => _rewards;
        public TMP_Text RewardTimer => _rewardTimer;
        public Transform SlotsParent => _slotsParent;
        public SlotRewardView SlotPrefab => _slotPrefab;
        public Button GetRewardButton => _getRewardButton;
        public Button ResetButton => _resetButton;
        public Button ExitButton => _exitButton;
        public Image AwardBar => _awardBar;

        public int CurrentActiveSlot
        {
            get => PlayerPrefs.GetInt(ActiveSlotKey);
            set => PlayerPrefs.SetInt(ActiveSlotKey, value);
        }

        public DateTime? LastRewardTime
        {
            get
            {
                var data = PlayerPrefs.GetString(LastTimeKey);
                if (string.IsNullOrEmpty(data))
                    return null;
                return DateTime.Parse(data);
            }
            set
            {
                if (value != null)
                    PlayerPrefs.SetString(LastTimeKey, value.ToString());
                else
                    PlayerPrefs.DeleteKey(LastTimeKey);
            }
        }


        private void OnDestroy()
        {
            _getRewardButton.onClick.RemoveAllListeners();
            _resetButton.onClick.RemoveAllListeners();
        }
    }
}