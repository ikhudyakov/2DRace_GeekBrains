using UnityEngine;

public class BaseInputController : BaseController
{
    public BaseInputController(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, Car car)
    {
        _view = LoadView();
        _view.Init(leftMove, rightMove, car.Speed);
    }

    private readonly ResourcePath _viewPath = new ResourcePath { Path = "Prefabs/gyroscopeMove" };
    private BaseInputView _view;

    private BaseInputView LoadView()
    {
        GameObject objView = Object.Instantiate(ResourceLoader.LoadGameObject(_viewPath));
        AddGameObjects(objView);
        return objView.GetComponent<BaseInputView>();
    }
}
