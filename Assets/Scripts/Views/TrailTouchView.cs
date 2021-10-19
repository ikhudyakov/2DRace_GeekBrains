using JoostenProductions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrailTouchView : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trailRenderer;

    private Dictionary<int, TrailRenderer> _renderByTouch = new Dictionary<int, TrailRenderer>();
    private List<TrailRenderer> _trailPool = new List<TrailRenderer>();

    public void Init()
    {
        UpdateManager.SubscribeToUpdate(OnUpdate);
    }

    private void OnUpdate()
    {
        foreach (var touch in Input.touches)
        {
            ProcessToush(touch);
        }
    }

    private void ProcessToush(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                StartTouch(touch);
                break;
            case TouchPhase.Moved:
                TouchInProgress(touch);
                break;
            case TouchPhase.Stationary:
                TouchInProgress(touch);
                break;
            case TouchPhase.Ended:
                ClearTouch(touch);
                break;
            case TouchPhase.Canceled:
                ClearTouch(touch);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ClearTouch(Touch touch)
    {
        if(_renderByTouch.TryGetValue(touch.fingerId, out var render))
        {
            render.Clear();
            render.gameObject.SetActive(false);
            render.emitting = false;
            _renderByTouch.Remove(touch.fingerId);
        }
    }

    private void TouchInProgress(Touch touch)
    {
        _renderByTouch[touch.fingerId].transform.position = GetToushPosition(touch);
    }

    private void StartTouch(Touch touch)
    {
        var existingTrail = _trailPool.FirstOrDefault(r => !_renderByTouch.Values.Contains(r));
        if(existingTrail == null)
        {
            existingTrail = GameObject.Instantiate(_trailRenderer, this.transform);
            existingTrail.gameObject.SetActive(false);
            _trailPool.Add(existingTrail);
        }

        _renderByTouch[touch.fingerId] = existingTrail;
        existingTrail.transform.position = GetToushPosition(touch);
        existingTrail.emitting = true;
        existingTrail.gameObject.SetActive(true);
    }

    private Vector3 GetToushPosition(Touch touch)
    {
        var pos = Camera.main.ScreenToWorldPoint(touch.position);
        pos.z = this.transform.position.z;
        return pos;
    }

    private void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(OnUpdate);
    }
}