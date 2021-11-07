using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Reward.Views
{
    public class CurrencyWindow : MonoBehaviour
    {
        private const string GoldKey = "Gold";
        private const string DiamondKey = "Diamond";

        public static CurrencyWindow Instance { get; private set; }

        [SerializeField]
        private TextMeshProUGUI _goldText;
        [SerializeField]
        private TextMeshProUGUI _diamondText;

        private int Gold
        {
            get => PlayerPrefs.GetInt(GoldKey);
            set => PlayerPrefs.SetInt(GoldKey, value);
        }

        private int Diamond
        {
            get => PlayerPrefs.GetInt(DiamondKey);
            set => PlayerPrefs.SetInt(DiamondKey, value);
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        public void AddGold(int count)
        {
            Gold += count;
            RefreshText();
        }

        public void AddDiamond(int count)
        {
            Diamond += count;
            RefreshText();
        }

        private void Start()
        {
            RefreshText();
        }

        private void RefreshText()
        {
            if (_diamondText != null)
                DOVirtual.Int(int.Parse(_diamondText.text), Diamond, 2f, (v) =>
                {
                    _diamondText.text = v.ToString();
                });
            if (_goldText != null)
                DOVirtual.Int(int.Parse(_goldText.text), Gold, 2f, (v) =>
                {
                    _goldText.text = v.ToString();
                });
        }
    }
}