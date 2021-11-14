using DG.Tweening;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalizationView : MonoBehaviour
{
    [SerializeField] private Button _russianButton;
    [SerializeField] private Button _englishButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private float _duration = 0.5f;

    private void Start()
    {
        _russianButton.onClick.AddListener(() => ChangeLanguage(1));
        _englishButton.onClick.AddListener(() => ChangeLanguage(0));
        _closeButton.onClick.AddListener(() => Hide());
    }

    private void OnDestroy()
    {
        _russianButton.onClick.RemoveAllListeners();
        _englishButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();
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

    private void ChangeLanguage(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }

}
