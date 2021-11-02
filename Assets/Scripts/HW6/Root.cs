using UnityEngine;

namespace hw6
{
    public class Root : MonoBehaviour
    {
        [SerializeField]
        private DailyRewardView _rewardView;

        private DailyRewardController _controller;

        private void Start()
        {
            _controller = new DailyRewardController(_rewardView);
        }
    }
}