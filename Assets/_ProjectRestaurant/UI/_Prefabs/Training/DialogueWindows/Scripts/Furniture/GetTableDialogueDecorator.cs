using System;
using UnityEngine;

public class GetTableTutorialDecorator : MonoBehaviour
{
    public Action TookOrangeAction;
    public Action TookAppleAction;
    [SerializeField] private Outline outline;

    private bool _isBlinking;
    private float _blinkSpeed = 4f;

    private void Update()
    {
        if (_isBlinking == false) return;

        float value = Mathf.PingPong(Time.time * _blinkSpeed, 2f);
        outline.OutlineWidth = value;
    }

    public void StartBlink()
    {
        _isBlinking = true;
    }

    public void StopBlink()
    {
        _isBlinking = false;
        outline.OutlineWidth = 0f;
    }
}