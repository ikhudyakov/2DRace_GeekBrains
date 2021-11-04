using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CustomButton : Button
{
    public const string TypeFieldName = nameof(_type);
    public const string DurationFieldName = nameof(_duration);
    public const string PowerFieldName = nameof(_power);
    public const string EasingFieldName = nameof(_easing);

    [SerializeField] private AnimationType _type;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _power = 50f;
    [SerializeField] private Ease _easing = Ease.InBounce;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        ShowAnimation();
    }

    private void ShowAnimation()
    {
        switch (_type)
        {
            case AnimationType.None:
                break;
            case AnimationType.Scale:
                transform.DOPunchScale(Vector3.one * (1 + (_power / 100)), _duration, 3)
                    .SetEase(_easing)
                    .OnComplete(()=>transform.localScale = Vector3.one);
                break;

            case AnimationType.Rotate:
                transform.DOShakeRotation(_duration, _power)
                    .SetEase(_easing)
                    .OnComplete(() => transform.rotation = Quaternion.identity);
                break;
        }
    }

}
