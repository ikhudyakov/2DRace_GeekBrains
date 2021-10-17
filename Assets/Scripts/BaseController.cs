using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : IDisposable
{
    private readonly List<BaseController> _controllers = new List<BaseController>();
    private readonly List<GameObject> _gameObjects = new List<GameObject>();

    private bool _isDisposed;

    public void Dispose()
    {
        if (_isDisposed)
            return;

        _isDisposed = true;

        OnDispose();

        foreach (var controller in _controllers)
            controller?.Dispose();

        _controllers.Clear();

        foreach (var gameObject in _gameObjects)
            GameObject.Destroy(gameObject);

        _gameObjects.Clear();
    }

    protected void AddController(BaseController controller)
    {
        _controllers.Add(controller);
    }

    protected void AddGameObjects(GameObject gameObject)
    {
        _gameObjects.Add(gameObject);
    }

    protected virtual void OnDispose()
    {
    }
}
