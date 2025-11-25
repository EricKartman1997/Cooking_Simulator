using UnityEngine;

[CreateAssetMenu(fileName = "ChecksContainer", menuName = "Container/ChecksContainer")]
public class CheckContainer: ScriptableObject
{
    [SerializeField] private CheckConfig bakedFish,
        bakedMeat,
        bakedSalad,
        fruitSalad,
        cutletMedium,
        wildBerryCocktail,
        freshnessCocktail;
    
    public CheckConfig BakedFish => bakedFish;

    public CheckConfig BakedMeat => bakedMeat;

    public CheckConfig BakedSalad => bakedSalad;

    public CheckConfig FruitSalad => fruitSalad;

    public CheckConfig CutletMedium => cutletMedium;

    public CheckConfig WildBerryCocktail => wildBerryCocktail;

    public CheckConfig FreshnessCocktail => freshnessCocktail;
}
