using UnityEngine;

[CreateAssetMenu(fileName = "ChecksFactory", menuName = "Factory/ChecksFactory")]
public class ChecksFactory: ScriptableObject
{
    [SerializeField] private CheckConfig bakedFish,
        bakedMeat,
        bakedSalad,
        fruitSalad,
        cutletMedium,
        wildBerryCocktail,
        freshnessCocktail;

    private bool _isInit;
    public bool IsInit => _isInit;
    public float BakedFish => bakedFish.Score;

    public float BakedMeat => bakedMeat.Score;

    public float BakedSalad => bakedSalad.Score;

    public float FruitSalad => fruitSalad.Score;

    public float CutletMedium => cutletMedium.Score;

    public float WildBerryCocktail => wildBerryCocktail.Score;

    public float FreshnessCocktail => freshnessCocktail.Score;

    private void OnEnable()
    {
        _isInit = true;
    }

    public GameObject GetCheckPrefab(CheckType type, Check check, Transform parent = null)
    {
        CheckConfig config = GetConfig(type);
        GameObject instance = Instantiate(config.Prefab, parent);
        CheckUI checkUI = instance.GetComponent<CheckUI>();
        checkUI.Init(check);
        return instance;
    }
    
    public Check GetCheck(CheckType type)
    {
        return GetCheckInstance(type);
    }
    
    private CheckConfig GetConfig(CheckType type)
    {
        switch (type)
        {
            case CheckType.BakedFish:
                return bakedFish;
            case CheckType.BakedMeat:
                return bakedMeat;
            case CheckType.BakedSalad:
                return bakedSalad;
            case CheckType.FruitSalad:
                return fruitSalad;
            case CheckType.CutletMedium:
                return cutletMedium;
            case CheckType.WildBerryCocktail:
                return wildBerryCocktail;
            case CheckType.FreshnessCocktail:
                return freshnessCocktail;
            default:
                throw new System.ArgumentException($"Не известный CheckType: {type}");
        }
        
    }
    
    private Check GetCheckInstance(CheckType type)
    {
        CheckConfig config = GetConfig(type);
        switch (type)
        {
            case CheckType.BakedFish:
                return new BakedFishCheck(config.Prefab, config.StartTime, config.Score, config.Dish);
            case CheckType.BakedMeat:
                return new BakedMeatCheck(config.Prefab, config.StartTime, config.Score, config.Dish);
            case CheckType.BakedSalad:
                return new BakedSaladCheck(config.Prefab, config.StartTime, config.Score, config.Dish);
            case CheckType.FruitSalad:
                return new FruitSaladCheck(config.Prefab, config.StartTime, config.Score, config.Dish);
            case CheckType.CutletMedium:
                return new CutletMediumCheck(config.Prefab, config.StartTime, config.Score, config.Dish);
            case CheckType.WildBerryCocktail:
                return new WildBerryCocktailCheck(config.Prefab, config.StartTime, config.Score, config.Dish);
            case CheckType.FreshnessCocktail:
                return new FreshnessCocktailCheck(config.Prefab, config.StartTime, config.Score, config.Dish);
            default:
                throw new System.ArgumentException($"Не известный Check: {type}");
        }
    }
}