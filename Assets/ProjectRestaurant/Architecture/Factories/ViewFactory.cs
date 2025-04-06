using UnityEngine;

public class ViewFactory
{
    private ProductsContainer _productsContainer;

    public ViewFactory(ProductsContainer productsContainer)
    {
        _productsContainer = productsContainer;
    }

    public GameObject GetProduct(EnumViewFood enumViewFood,Transform parent)
    {
        switch (enumViewFood)
        {
            case EnumViewFood.Apple:
                return Object.Instantiate(_productsContainer.AppleDish,parent);
            case EnumViewFood.Orange:
                return Object.Instantiate(_productsContainer.OrangeDish, parent);
            case EnumViewFood.Lime:
                return Object.Instantiate(_productsContainer.LimeDish, parent);
            case EnumViewFood.Cherry:
                return Object.Instantiate(_productsContainer.CherryDish, parent);
            case EnumViewFood.Blueberry:
                return Object.Instantiate(_productsContainer.BlueberryDish, parent);
            case EnumViewFood.Strawberry:
                return Object.Instantiate(_productsContainer.StrawberryDish, parent);
            case EnumViewFood.Fish:
                return Object.Instantiate(_productsContainer.FishDish, parent);
            case EnumViewFood.Meat:
                return Object.Instantiate(_productsContainer.MeatDish, parent);
            default:
                Debug.LogWarning($"Unknown product type: {enumViewFood}");
                return null;
        }
    }
    
    public GameObject GetViewTable(EnumView enumView,Transform parent)
    {
        switch (enumView)
        {
            case EnumView.Default:
                return null;
            case EnumView.Other:
                return Object.Instantiate(_productsContainer.OtherView,parent);
            default:
                Debug.LogWarning($"Unknown view type: {enumView}");
                return null;
        }
    }
}
