using System.Collections.Generic;
using UnityEngine;

public class ProductsContainer: MonoBehaviour
{
    private void Start()
    {
        InitializeDictionaries();
        // StaticManagerWithoutZenject.ViewFactory = new ViewFactory(this);
        // StaticManagerWithoutZenject.ProductsFactory = new ProductsFactory(this);
        // StaticManagerWithoutZenject.HelperScriptFactory = new HelperScriptFactory();
        // StaticManagerWithoutZenject.BootstrapLVL2 = new BootstrapLVL2(GetComponent<FieldsForScriptContainer>());
    }

    [SerializeField] private Apple apple;
    [SerializeField] private Orange orange;
    [SerializeField] private Lime lime;
    [SerializeField] private Blueberry blueberry;
    [SerializeField] private Strawberry strawberry;
    [SerializeField] private Cherry cherry;
    
    [SerializeField] private BakedApple bakedApple;
    [SerializeField] private BakedOrange bakedOrange;

    [SerializeField] private FruitSalad fruitSalad;
    [SerializeField] private BakedSalad bakedFruit;
    [SerializeField] private WildBerryCocktail wildBerryCocktail;
    [SerializeField] private FreshnessCocktail freshnessCocktail;
    
    [SerializeField] private Meat meat;
    [SerializeField] private Fish fish;
    [SerializeField] private RawCutlet rawCutlet;
    
    [SerializeField] private BakedMeat bakedMeat;
    [SerializeField] private BakedFish bakedFish;
    [SerializeField] private MediumCutlet mediumCutlet;
    [SerializeField] private BurnCutlet burnCutlet;
    
    [SerializeField] private Rubbish rubbish;

    [SerializeField] private List<GameObject> requiredFruitSalad;
    [SerializeField] private List<GameObject> requiredMixBakedFruit;
    [SerializeField] private List<GameObject> requiredFreshnessCocktail;
    [SerializeField] private List<GameObject> requiredWildBerryCocktail;

    //private Dictionary<string, ObjsForDistribution> _recipesForSuvide;
    private Dictionary<string, Product> _recipesForOven;
    
    [SerializeField] private GameObject appleDish;
    [SerializeField] private GameObject orangeDish;
    [SerializeField] private GameObject limeDish;
    [SerializeField] private GameObject blueberryDish;
    [SerializeField] private GameObject strawberryDish;
    [SerializeField] private GameObject cherryDish;
    
    [SerializeField] private GameObject meatDish;
    [SerializeField] private GameObject fishDish;
    [SerializeField] private GameObject rawCutletDish;
    
    [SerializeField] private GameObject defaultView;
    [SerializeField] private GameObject crossView;
    [SerializeField] private GameObject crockView;
    [SerializeField] private GameObject newYearView;
    
    // Свойства с публичным геттером и приватным сеттером
    public GameObject Apple => apple.gameObject;
    public GameObject Orange => orange.gameObject;
    public GameObject Lime => lime.gameObject;
    public GameObject Blueberry => blueberry.gameObject;
    public GameObject Strawberry => strawberry.gameObject;
    public GameObject Cherry => cherry.gameObject;
    
    public GameObject BakedApple => bakedApple.gameObject;
    public GameObject BakedOrange => bakedOrange.gameObject;

    public GameObject FruitSalad => fruitSalad.gameObject;
    public GameObject MixBakedFruit => bakedFruit.gameObject;
    public GameObject WildBerryCocktail => wildBerryCocktail.gameObject;
    public GameObject FreshnessCocktail => freshnessCocktail.gameObject;
    
    public GameObject Meat => meat.gameObject;
    public GameObject Fish => fish.gameObject;
    public GameObject RawCutlet => rawCutlet.gameObject;
    
    public GameObject BakedMeat => bakedMeat.gameObject;
    public GameObject BakedFish => bakedFish.gameObject;
    public GameObject BurnCutlet => burnCutlet.gameObject;
    public GameObject MediumCutlet => mediumCutlet.gameObject;
    
    public GameObject Rubbish => rubbish.gameObject;

    public List<GameObject> RequiredFruitSalad => requiredFruitSalad;
    public List<GameObject> RequiredMixBakedFruit => requiredMixBakedFruit;
    public List<GameObject> RequiredFreshnessCocktail => requiredFreshnessCocktail;
    public List<GameObject> RequiredWildBerryCocktail => requiredWildBerryCocktail;
    
    //public Dictionary<string, ObjsForDistribution> RecipesForSuvide => _recipesForSuvide;
    public Dictionary<string, Product> RecipesForOven => _recipesForOven;
    
    public GameObject AppleDish => appleDish;
    public GameObject OrangeDish => orangeDish;
    public GameObject LimeDish => limeDish;
    public GameObject BlueberryDish => blueberryDish;
    public GameObject StrawberryDish => strawberryDish;
    public GameObject CherryDish => cherryDish;
    public GameObject MeatDish => meatDish;
    public GameObject FishDish => fishDish;
    public GameObject RawCutletDish => rawCutletDish;

    public GameObject DefaultView => defaultView;
    public GameObject CrossView => crossView;
    public GameObject CrockView => crockView;
    public GameObject NewYearView => newYearView;
    
    private void InitializeDictionaries()
    {
        // _recipesForSuvide = new Dictionary<string, ObjsForDistribution>()
        // {
        //     { meat.name, bakedMeat.GetComponent<ObjsForDistribution>() },
        //     { fish.name, bakedFish.GetComponent<ObjsForDistribution>()}
        // };
        
        _recipesForOven = new Dictionary<string, Product>()
        {
            { meat.name, bakedMeat.GetComponent<CookedFood>() },
            { fish.name, bakedFish.GetComponent<CookedFood>() },
            { apple.name, bakedApple.GetComponent<CookedIngredient>() },
            { orange.name, bakedOrange.GetComponent<CookedIngredient>() }
        };
    }
}
