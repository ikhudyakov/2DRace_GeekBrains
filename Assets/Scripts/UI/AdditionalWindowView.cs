using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace UI
{
    public class AdditionalWindowView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private float _duration = 0.5f;

        private void Awake()
        {
            _closeButton.onClick.AddListener(Hide);
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
    }
}