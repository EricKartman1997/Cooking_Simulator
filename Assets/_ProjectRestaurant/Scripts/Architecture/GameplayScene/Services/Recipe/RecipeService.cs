using System;
using System.Collections.Generic;
using UnityEngine;

public class RecipeService
{
    private readonly Dictionary<FurnitureName,
        Dictionary<RecipeKey, IngredientName>> _recipes
        = new();

    public RecipeService(RecipeContainer recipeContainer)
    {
        Initialize(recipeContainer);
    }

    private void Initialize(RecipeContainer recipeContainer)
    {
        foreach (var cfg in recipeContainer.Recipes)
        {
            if (!_recipes.ContainsKey(cfg.Station))
                _recipes[cfg.Station] = new Dictionary<RecipeKey, IngredientName>();

            var key = new RecipeKey(cfg.Ingredients);

            if (!_recipes[cfg.Station].TryAdd(key, cfg.Result))
            {
                Debug.LogWarning($"Duplicate recipe in {cfg.Station} -> {cfg.Result}");
            }
        }

        Debug.Log($"RecipeService initialized. Stations: {_recipes.Count}");
    }

    public IngredientName GetDish(FurnitureName station, List<Product> provided)
    {
        if (!_recipes.TryGetValue(station, out var dict))
            return IngredientName.Rubbish;

        var key = new RecipeKey(provided);

        if (dict.TryGetValue(key, out var result))
            return result;

        return IngredientName.Rubbish;
    }
}