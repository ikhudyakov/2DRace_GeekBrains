using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    public Action Start;
    [SerializeField] private Button startButton;

    public TrailRenderer trailrenderer;
    const float LINE_POS_Z = 10;

    private void Awake() => startButton.onClick.AddListener(OnStartClick);

    private void OnStartClick()
    {
        Start?.Invoke();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            trailrenderer.emitting = false;
            var mousPos = Input.mousePosition;
            trailrenderer.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousPos.x, mousPos.y, LINE_POS_Z));
            return;
        }


        if (Input.GetMouseButton(0))
        {
            trailrenderer.emitting = true;
            var mousPos = Input.mousePosition;
            trailrenderer.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousPos.x, mousPos.y, LINE_POS_Z));
        }
    }
}