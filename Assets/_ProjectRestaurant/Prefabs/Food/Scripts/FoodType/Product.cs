using UnityEngine;

public abstract class Product : MonoBehaviour
{
    [SerializeField] private IngredientName type;
    public IngredientName Name => type;
}

public enum ProductType 
{ 
    Meat,
    Fish,
    Apple,
    Orange,
    Lime,
    Blueberry,
    Cherry,
    Strawberry,
    RawCutlet,
    BakedApple,
    BakedOrange,
    MediumCutlet,
    Other,
}
