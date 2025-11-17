using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeService: IDisposable
{
    private RecipeContainer _recipeContainer;
    //private ProductsContainer _productsContainer;
    private ProductsFactory _productsFactory;
    
    // Словари для разных типов станций
    private readonly Dictionary<RecipeKey, Product> _ovenRecipes = new();
    private readonly Dictionary<RecipeKey, Product> _cuttingTableRecipes = new();
    private readonly Dictionary<RecipeKey, Product> _blenderRecipes = new();
    private readonly Dictionary<RecipeKey, Product> _suvideRecipes = new();
    private readonly Dictionary<RecipeKey, Product> _stoveRecipes = new();
    
    //public bool IsInit { get; private set; }

    public RecipeService(RecipeContainer recipeContainer,ProductsFactory productsFactory)
    {
        _recipeContainer = recipeContainer;
        _productsFactory = productsFactory;
        
        InitializeDictionaries();
        Debug.Log("Создать объект: RecipeService");
        
    }
    
    public void Dispose()
    {
        Debug.Log("SuvideFurniture Dispose");
    }
    
    // Метод для получения блюда по типу станции
    public Product GetDish(StationType stationType, List<Product> providedProducts)
    {
        var validIngredients = providedProducts
            .Where(i => i != null)
            .ToList();
    
        var key = new RecipeKey(validIngredients);
    
        // Для отладки
        Debug.Log($"Searching for: {string.Join(", ", validIngredients.Select(i => i.Type))}");
    
        return stationType switch
        {
            StationType.Oven => FindInDictionary(_ovenRecipes, key),
            StationType.CuttingTable => FindInDictionary(_cuttingTableRecipes, key),
            StationType.Suvide => FindInDictionary(_suvideRecipes, key),
            StationType.Stove => FindInDictionary(_stoveRecipes, key),
            StationType.Blender => FindInDictionary(_blenderRecipes, key),
            _ => null
        };
    }

    private void InitializeDictionaries()
    {
        // Ждем инициализации контейнера
        if (!_recipeContainer.IsInit)
        {
            Debug.LogWarning("RecipeContainer не инициализирован! Выполняем ручную инициализацию...");
            //_recipeContainer.InitializeManually();
        }

        // Заполняем словари
        FillDictionary(_ovenRecipes, _recipeContainer.ListOvenRecipes);
        FillDictionary(_cuttingTableRecipes, _recipeContainer.ListCuttingTableRecipes);
        FillDictionary(_blenderRecipes, _recipeContainer.ListBlenderRecipes);
        FillDictionary(_suvideRecipes, _recipeContainer.ListSuvideRecipes);
        FillDictionary(_stoveRecipes, _recipeContainer.ListStoveRecipes);
        
        Debug.Log($"RecipeService инициализирован. Рецептов: " +
                  $"Духовка={_ovenRecipes.Count}, " +
                  $"Разделочный стол={_cuttingTableRecipes.Count}, " +
                  $"Блендер={_blenderRecipes.Count}");
    }
    
    private void FillDictionary(Dictionary<RecipeKey, Product> targetDictionary, List<RecipeContainerConfig> configs)
    {
        if (configs == null) return;
        
        foreach (var config in configs)
        {
            if (config == null || config.ListIngredients == null || config.Dish == null)
            {
                Debug.LogWarning("Пропущен невалидный рецепт");
                continue;
            }
            
            var key = new RecipeKey(config.ListIngredients);
            if (!targetDictionary.TryAdd(key, config.Dish))
            {
                Debug.LogWarning($"Дубликат рецепта: {key} для {config.Dish.name}");
            }
        }
    }

    private Product FindInDictionary(Dictionary<RecipeKey, Product> dictionary, RecipeKey key)
    {
        //TODO переделать на Enum IngredientName -> return IngredientName
        if (dictionary.TryGetValue(key, out Product result))
        {
            return result;
        }
        Debug.LogWarning($"Recipe not found. Available keys:");
        return _productsFactory.GetRubbish().GetComponent<Rubbish>();
        //TODO нахуй переделать 
    }
    
    
}

public enum StationType
{
    Oven,
    CuttingTable,
    Blender,
    Suvide,
    Stove,
    MeatGrinder
}
