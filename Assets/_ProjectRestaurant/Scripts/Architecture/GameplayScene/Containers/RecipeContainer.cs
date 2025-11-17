using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeContainer", menuName = "Container/RecipeContainer")]
public class RecipeContainer : ScriptableObject
{
    //TODO переделать на Goggle таблицу
    [SerializeField] private RecipeContainerConfig
        bakedFishRecipe,
        bakedMeatRecipe,
        bakedAppleRecipe,
        bakedOrangeRecipe,
        bakedSaladRecipe,
        fruitSaladRecipe,
        suvideMeatRecipe,
        suvideFishRecipe,
        freshnessCocktailRecipe,
        wildBerryCocktailRecipe,
        rawCutletRecipe,
        mediumCutletRecipe,
        burnCutletRecipe;
    
    private bool _isInit;
    private List<RecipeContainerConfig> _listOvenRecipes;
    private List<RecipeContainerConfig> _listCuttingTableRecipes;
    private List<RecipeContainerConfig> _listBlenderRecipes;
    private List<RecipeContainerConfig> _listSuvideRecipes;
    private List<RecipeContainerConfig> _listStoveRecipes;
    
    public List<RecipeContainerConfig> ListOvenRecipes => _listOvenRecipes;
    public List<RecipeContainerConfig> ListCuttingTableRecipes => _listCuttingTableRecipes;
    public List<RecipeContainerConfig> ListBlenderRecipes => _listBlenderRecipes;
    public List<RecipeContainerConfig> ListSuvideRecipes => _listSuvideRecipes;
    public List<RecipeContainerConfig> ListStoveRecipes => _listStoveRecipes;
    public bool IsInit => _isInit;
    
    
    private void OnEnable()
    {
        _listOvenRecipes = new List<RecipeContainerConfig>()
        {
            bakedFishRecipe,
            bakedMeatRecipe,
            bakedAppleRecipe,
            bakedOrangeRecipe,
        };
        
        _listCuttingTableRecipes = new List<RecipeContainerConfig>()
        {
            bakedSaladRecipe,
            fruitSaladRecipe,
            
        };
        
        _listBlenderRecipes = new List<RecipeContainerConfig>()
        {
            freshnessCocktailRecipe,
            wildBerryCocktailRecipe,
            
        };
        
        _listSuvideRecipes = new List<RecipeContainerConfig>()
        {
            suvideMeatRecipe,
            suvideFishRecipe
        };
        
        _listStoveRecipes = new List<RecipeContainerConfig>()
        {
            rawCutletRecipe,
            mediumCutletRecipe,
            burnCutletRecipe
        };
        
        _isInit = true;
    }
}

