using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RecipeContainerConfig
{
    [SerializeField] private FurnitureName station;
    [SerializeField] private List<IngredientName> ingredients;
    [SerializeField] private IngredientName resultDish;
    [SerializeField] private float cookingTime;

    public FurnitureName Station => station;
    public List<IngredientName> Ingredients => ingredients;
    public IngredientName Result => resultDish;
    public float CookingTime => cookingTime;
}


