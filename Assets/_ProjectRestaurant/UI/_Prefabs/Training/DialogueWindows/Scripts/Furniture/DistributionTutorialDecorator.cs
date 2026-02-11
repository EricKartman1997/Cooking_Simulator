using System;
using UnityEngine;

public class DistributionTutorialDecorator : MonoBehaviour
{
    [SerializeField] private Outline outline;

    private bool _isBlinking;
    private float _blinkSpeed = 4f;

    public event Action OnDishAccepted;

    private void Update()
    {
        if (!_isBlinking) return;

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

    public void InvokeDishAccepted()
    {
        OnDishAccepted?.Invoke();
    }
}
