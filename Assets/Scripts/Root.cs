using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Transform UiRoot;

    private PlayerData _model;
    private MainController _mainController;


    private void Start()
    {
        _model = new PlayerData(new Car(3.0f));
        _mainController = new MainController(_model, UiRoot);
        _model.State.Value = GameState.Start;
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
        _mainController = null;
        _model = null;
    }
}
