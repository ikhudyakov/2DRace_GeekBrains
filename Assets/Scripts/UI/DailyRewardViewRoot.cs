using Reward.Views;
using UnityEngine;
using Views;

public class DailyRewardViewRoot : MonoBehaviour
{
    [field: SerializeField]
    public CurrencyWindow CurrencyWindow { get; }
    [field: SerializeField]
    public DailyRewardView RewardView { get; }
}
