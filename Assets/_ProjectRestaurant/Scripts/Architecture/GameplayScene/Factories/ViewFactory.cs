using System;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Collections;

public class ViewFactory
{
    //private GameManager _gameManager;
    //private MonoBehaviour _coroutineMonoBehaviour;
    //private ProductsContainer _productsContainer;
    //private bool _isInit;
    
    //public bool IsInit => _isInit;
    private LoadReleaseGameplay _loadReleaseGameplay;
    
    public ViewFactory(LoadReleaseGameplay loadReleaseGameplay)
    {
        _loadReleaseGameplay = loadReleaseGameplay;
    }

    public GameObject GetProduct(ViewDishName enumViewFood,Transform parent)
    {
        switch (enumViewFood)
        {
            case ViewDishName.AppleViewDish:
                return Object.Instantiate(_loadReleaseGameplay.ViewDishDic[ViewDishName.AppleViewDish],parent);
            case ViewDishName.OrangeViewDish:
                return Object.Instantiate(_loadReleaseGameplay.ViewDishDic[ViewDishName.OrangeViewDish], parent);
            case ViewDishName.LimeViewDish:
                return Object.Instantiate(_loadReleaseGameplay.ViewDishDic[ViewDishName.LimeViewDish], parent);
            case ViewDishName.CherryViewDish:
                return Object.Instantiate(_loadReleaseGameplay.ViewDishDic[ViewDishName.CherryViewDish], parent);
            case ViewDishName.BlueberryViewDish:
                return Object.Instantiate(_loadReleaseGameplay.ViewDishDic[ViewDishName.BlueberryViewDish], parent);
            case ViewDishName.StrawberryViewDish:
                return Object.Instantiate(_loadReleaseGameplay.ViewDishDic[ViewDishName.StrawberryViewDish], parent);
            case ViewDishName.FishViewDish:
                return Object.Instantiate(_loadReleaseGameplay.ViewDishDic[ViewDishName.FishViewDish], parent);
            case ViewDishName.MeatViewDish:
                return Object.Instantiate(_loadReleaseGameplay.ViewDishDic[ViewDishName.MeatViewDish], parent);
            case ViewDishName.RawCutletViewDish:
                return Object.Instantiate(_loadReleaseGameplay.ViewDishDic[ViewDishName.RawCutletViewDish], parent);
            default:
                Debug.LogWarning($"Unknown product type: {enumViewFood}");
                return null;
        }
    }
    
    public GameObject GetDecorationTableTop(CustomFurnitureName enumView,Transform parent)
    {
        switch (enumView)
        {
            case CustomFurnitureName.Default:
                return Object.Instantiate(_loadReleaseGameplay.CustomDic[CustomFurnitureName.Default],parent);
            case CustomFurnitureName.TurnOff:
                return Object.Instantiate(_loadReleaseGameplay.CustomDic[CustomFurnitureName.TurnOff],parent);
            case CustomFurnitureName.NewYear:
                return Object.Instantiate(_loadReleaseGameplay.CustomDic[CustomFurnitureName.NewYear],parent);
            default:
                Debug.LogWarning($"Unknown view type: {enumView}");
                return null;
        }
    }
    
    public GameObject GetDecorationLowerSurface(CustomFurnitureName enumView,Transform parent)
    {
        switch (enumView)
        {
            case CustomFurnitureName.Default:
                return Object.Instantiate(_loadReleaseGameplay.CustomDic[CustomFurnitureName.Default],parent);
            case CustomFurnitureName.Crock:
                return Object.Instantiate(_loadReleaseGameplay.CustomDic[CustomFurnitureName.Crock],parent);
            default:
                Debug.LogWarning($"Unknown view type: {enumView}");
                return null;
        }
    }
}
