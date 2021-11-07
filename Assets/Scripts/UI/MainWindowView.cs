using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI
{
    public class MainWindowView : MonoBehaviour
    {
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _textChangeButton;
        [SerializeField] private AdditionalWindowView _child;
        [SerializeField] private Text _text;

        private void Awake()
        {
            _openButton.onClick.AddListener(OpenPopup);
            _textChangeButton.onClick.AddListener(TweenText);
            _child.gameObject.SetActive(false);
        }

        private void TweenText()
        {
            _text.DOText("Новый супер мощный текст", 1.0f).SetEase(Ease.Linear);
        }

        void OpenPopup()
        {
            _child.Show();
        }
    }
}