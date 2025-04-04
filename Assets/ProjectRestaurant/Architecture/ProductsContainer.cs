using System;
using System.Collections.Generic;
using UnityEngine;

public class ProductsContainer: MonoBehaviour
{
    private void Awake()
    {
        InitializeDictionaries();
    }

    [SerializeField] private Product apple;
    [SerializeField] private Product orange;
    [SerializeField] private GameObject lime;
    [SerializeField] private GameObject blueberry;
    [SerializeField] private GameObject strawberry;
    [SerializeField] private GameObject cherry;
    
    [SerializeField] private Product bakedApple;
    [SerializeField] private Product bakedOrange;

    [SerializeField] private GameObject fruitSalad;
    [SerializeField] private GameObject mixBakedFruit;
    [SerializeField] private GameObject wildBerryCocktail;
    [SerializeField] private GameObject freshnessCocktail;
    
    [SerializeField] private Product meat;
    [SerializeField] private Product fish;
    
    [SerializeField] private Product bakedMeat;
    [SerializeField] private Product bakedFish;
    
    [SerializeField] private GameObject rubbish;

    [SerializeField] private List<GameObject> requiredFruitSalad;
    [SerializeField] private List<GameObject> requiredMixBakedFruit;
    [SerializeField] private List<GameObject> requiredFreshnessCocktail;
    [SerializeField] private List<GameObject> requiredWildBerryCocktail;

    private Dictionary<string, ObjsForDistribution> _recipesForSuvide;
    private Dictionary<string, FromOven> _recipesForOven;
    
    // Свойства с публичным геттером и приватным сеттером
    public Product Apple => apple;
    public Product Orange => orange;
    public GameObject Lime => lime;
    public GameObject Blueberry => blueberry;
    public GameObject Strawberry => strawberry;
    public GameObject Cherry => cherry;
    
    public Product BakedApple => bakedApple;
    public Product BakedOrange => bakedOrange;

    public GameObject FruitSalad => fruitSalad;
    public GameObject MixBakedFruit => mixBakedFruit;
    public GameObject WildBerryCocktail => wildBerryCocktail;
    public GameObject FreshnessCocktail => freshnessCocktail;
    
    public Product Meat => meat;
    public Product Fish => fish;
    
    public Product BakedMeat => bakedMeat;
    public Product BakedFish => bakedFish;
    
    public GameObject Rubbish => rubbish;

    public List<GameObject> RequiredFruitSalad => requiredFruitSalad;
    public List<GameObject> RequiredMixBakedFruit => requiredMixBakedFruit;
    public List<GameObject> RequiredFreshnessCocktail => requiredFreshnessCocktail;
    public List<GameObject> RequiredWildBerryCocktail => requiredWildBerryCocktail;
    
    public Dictionary<string, ObjsForDistribution> RecipesForSuvide => _recipesForSuvide;

    public Dictionary<string, FromOven> RecipesForOven => _recipesForOven;

    private void InitializeDictionaries()
    {
        _recipesForSuvide = new Dictionary<string, ObjsForDistribution>()
        {
            { meat.name, bakedMeat.GetComponent<ObjsForDistribution>() },
            { fish.name, bakedFish.GetComponent<ObjsForDistribution>()}
        };
        
        _recipesForOven = new Dictionary<string, FromOven>()
        {
            { meat.name, bakedMeat.GetComponent<FromOven>() },
            { fish.name, bakedFish.GetComponent<FromOven>() },
            { apple.name, bakedApple.GetComponent<FromOven>() },
            { orange.name, bakedOrange.GetComponent<FromOven>() }
        };
    }


}
