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
            case EnumViewFood.RawCutlet:
                return Object.Instantiate(_productsContainer.RawCutletDish, parent);
            default:
                Debug.LogWarning($"Unknown product type: {enumViewFood}");
                return null;
        }
    }
    
    public GameObject GetDecorationTableTop(EnumDecorationTableTop enumView,Transform parent)
    {
        switch (enumView)
        {
            case EnumDecorationTableTop.Default:
                return Object.Instantiate(_productsContainer.DefaultView,parent);
            case EnumDecorationTableTop.TurnOff:
                return Object.Instantiate(_productsContainer.CrossView,parent);
            case EnumDecorationTableTop.NewYear:
                return Object.Instantiate(_productsContainer.NewYearView,parent);
            default:
                Debug.LogWarning($"Unknown view type: {enumView}");
                return null;
        }
    }
    
    public GameObject GetDecorationLowerSurface(EnumDecorationLowerSurface enumView,Transform parent)
    {
        switch (enumView)
        {
            case EnumDecorationLowerSurface.Default:
                return Object.Instantiate(_productsContainer.DefaultView,parent);
            case EnumDecorationLowerSurface.Crock:
                return Object.Instantiate(_productsContainer.CrockView,parent);
            default:
                Debug.LogWarning($"Unknown view type: {enumView}");
                return null;
        }
    }
}
