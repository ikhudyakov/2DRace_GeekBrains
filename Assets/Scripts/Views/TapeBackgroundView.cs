using Tools;
using UnityEngine;

namespace Views
{
    public class TapeBackgroundView : MonoBehaviour
    {
        [SerializeField]
        private Background[] _backgrounds;

        private IReadOnlySubscriptionProperty<float> _diff;

        public void Init(IReadOnlySubscriptionProperty<float> diff)
        {
            _diff = diff;
            _diff.Subscribe(Move);
        }

        protected void OnDestroy()
        {
            _diff?.Subscribe(Move);
        }

        private void Move(float value)
        {
            foreach (var background in _backgrounds)
                background.Move(-value);
        }
    }
}