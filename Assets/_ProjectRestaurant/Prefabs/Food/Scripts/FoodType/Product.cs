using UnityEngine;

public abstract class Product : MonoBehaviour
{
    [SerializeField] private ProductType type;
    public ProductType Type => type;
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
