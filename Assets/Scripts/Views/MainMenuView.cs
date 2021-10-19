using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class MainMenuView : MonoBehaviour
{
    public Action Start;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _showRewardedButton;
    [SerializeField] private IAPButton _buyGoldButton;
    [SerializeField] private Text _goldAmount;

    private PlayerData _model;
    const float LINE_POS_Z = 10;

    public void Init(UnityAction startGame, UnityAction rewardAdRequested, PlayerData model)
    {
        _startButton?.onClick.AddListener(startGame);
        _showRewardedButton?.onClick.AddListener(rewardAdRequested);
        _buyGoldButton.GetComponent<BuyGoldView>().Init(model);
        _model = model;
    }

    protected void OnDestroy()
    {
        _startButton.onClick.RemoveAllListeners();
        _showRewardedButton.onClick.RemoveAllListeners();
    }

    void Update()
    {
        _goldAmount.text = _model.Gold.ToString();
    }
}