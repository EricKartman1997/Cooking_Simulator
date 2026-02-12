using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GiveTableTutorialDecorator : MonoBehaviour,IPause
{
    public Action PutSalatAction;
    
    [SerializeField] private Outline outline;
    [SerializeField] private List<IngredientName> allowedIngredients = new() {IngredientName.FruitSalad };

    private IHandlerPause _pauseHandler;
    private bool _isPause;
    
    private bool _isBlinking;
    private float _blinkSpeed = 4f;

    
    [Inject]
    private void ConstructZenject(IHandlerPause pauseHandler)
    {
        _pauseHandler = pauseHandler;
        _pauseHandler.Add(this);
    }
    
    private void Update()
    {
        if (_isPause == true) return;
        if (!_isBlinking) return;

        float value = Mathf.PingPong(Time.time * _blinkSpeed, 2f);
        outline.OutlineWidth = value;
    }

    public void OnDisable()
    {
        _pauseHandler.Remove(this);
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

    public bool CanAccept(IngredientName ingredient)
    {
        return allowedIngredients.Contains(ingredient);
    }
    
    public void SetPause(bool isPaused)
    {
        _isPause = isPaused;
        outline.OutlineWidth = 0f;
    }
}