using System.Collections.Generic;
using UnityEngine;

public class GiveTableTutorialDecorator : MonoBehaviour
{
    [SerializeField] private Outline outline;

    [SerializeField]
    private List<IngredientName> allowedIngredients = new()
    {
        IngredientName.Apple,
        IngredientName.Orange,
        IngredientName.FruitSalad
    };

    private bool _isBlinking;
    private float _blinkSpeed = 4f;

    public IngredientName? CurrentIngredientOnTable { get; private set; }

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

    public bool CanAccept(IngredientName ingredient)
    {
        return allowedIngredients.Contains(ingredient);
    }

    public void SetCurrentIngredient(IngredientName ingredient)
    {
        CurrentIngredientOnTable = ingredient;
    }

    public void ClearCurrentIngredient()
    {
        CurrentIngredientOnTable = null;
    }
}