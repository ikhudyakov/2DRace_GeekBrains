using UnityEngine;

public class TapeBackgroundController : BaseController
{
    public TapeBackgroundController(IReadOnlySubscriptionProperty<float> leftMove,
        IReadOnlySubscriptionProperty<float> rightMove)
    {
        _view = LoadView();
        _diff = new SubscriptionProperty<float>();

        _leftMove = leftMove;
        _rightMove = rightMove;

        _view.Init(_diff);

        _leftMove.Subscribe(Move);
        _rightMove.Subscribe(Move);
    }

    private readonly ResourcePath _viewPath = new ResourcePath { Path = "Prefabs/background" };
    private TapeBackgroundView _view;
    private readonly SubscriptionProperty<float> _diff;
    private readonly IReadOnlySubscriptionProperty<float> _leftMove;
    private readonly IReadOnlySubscriptionProperty<float> _rightMove;

    protected override void OnDispose()
    {
        _leftMove.Unsubscribe(Move);
        _rightMove.Unsubscribe(Move);

        base.OnDispose();
    }

    private TapeBackgroundView LoadView()
    {
        var objView = Object.Instantiate(ResourceLoader.LoadGameObject(_viewPath));
        AddGameObjects(objView);

        return objView.GetComponent<TapeBackgroundView>();
    }

    private void Move(float value)
    {
        _diff.Value = value;
    }
}

