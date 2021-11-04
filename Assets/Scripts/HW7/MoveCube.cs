using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class MoveCube : MonoBehaviour
{
    [SerializeField]
    private Vector3 _targetPosition = new Vector3(10f, 10f, 0);
    [SerializeField]
    private float _time = 3f;
    [SerializeField]
    private Color _targetColor;

    [SerializeField]
    private List<Transform> _waypoints = new List<Transform>();
    [SerializeField]
    private AnimationCurve _curve;

    private void Start()
    {
        //GetComponent<Renderer>().material.DOColor(_targetColor, _time);
        //transform.DOMove(_targetPosition, _time).From();
        //transform.DORotate(new Vector3(180f, 0f), _time, RotateMode.Fast);

        var sequence = DOTween.Sequence();
        for (int i = 0; i < _waypoints.Count; i++)
        {
            sequence.Append(transform.DOMove(_waypoints[i].transform.position, _time).SetEase(_curve));
            var jumpPosition = _waypoints[i].transform.position;
            jumpPosition.y = jumpPosition.y + 3;
            sequence.Append(transform.DOLocalJump(jumpPosition, 1f, 1, .5f));
        }

        sequence.Play();

    }
}
