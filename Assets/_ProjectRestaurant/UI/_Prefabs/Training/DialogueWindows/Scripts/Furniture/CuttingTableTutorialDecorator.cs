using System;
using UnityEngine;

public class CuttingTableTutorialDecorator : MonoBehaviour
{
    public Action PutAppleAction;
    public Action PutOrangeAction;
    public Action CookedSalatAction;
    
    [SerializeField] private Outline outline;

    private bool _isBlinking;
    private float _blinkSpeed = 4f;

    public IngredientName? CreatedIngredient { get; private set; }

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

    public void SetCreatedIngredient(IngredientName ingredient)
    {
        CreatedIngredient = ingredient;
    }

    public void ClearCreatedIngredient()
    {
        CreatedIngredient = null;
    }
}
