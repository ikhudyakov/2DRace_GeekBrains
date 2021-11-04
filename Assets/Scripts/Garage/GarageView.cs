using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Garage
{
    class GarageView : MonoBehaviour
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private float _duration = 0.5f;

        private void Awake()
        {
            _exitButton.onClick.AddListener(Hide);
        }

        public void Hide()
        {
            var sequance = DOTween.Sequence();
            sequance.Insert(0f, transform.DOScale(Vector3.zero, _duration));
            sequance.OnComplete(() =>
            {
                transform.localScale = Vector3.zero;
                gameObject.SetActive(false);
            });
        }

        public void Show()
        {
            gameObject.SetActive(true);
            transform.localScale = Vector3.zero;
            var sequance = DOTween.Sequence();
            sequance.Insert(0f, DOVirtual.Vector3(Vector3.zero, Vector3.one, _duration, (v) =>
            {
                transform.localScale = v;
            }));
            sequance.OnComplete(() =>
            {
                transform.localScale = Vector3.one;
            });
        }

        public void Init(UnityAction upgradeCar)
        {
            _upgradeButton?.onClick.AddListener(upgradeCar);
            _exitButton?.onClick.AddListener(Hide);
        }

        protected void OnDestroy()
        {
            _upgradeButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }
    }
}
