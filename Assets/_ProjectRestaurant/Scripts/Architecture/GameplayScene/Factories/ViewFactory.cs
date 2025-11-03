using System;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Collections;

public class ViewFactory: IDisposable
{
    private GameManager _gameManager;
    private MonoBehaviour _coroutineMonoBehaviour;
    private ProductsContainer _productsContainer;
    private bool _isInit;
    
    public bool IsInit => _isInit;

    public ViewFactory(ProductsContainer productsContainer,MonoBehaviour coroutineMonoBehaviour)
    {
        _productsContainer = productsContainer;
        _coroutineMonoBehaviour = coroutineMonoBehaviour;

        _coroutineMonoBehaviour.StartCoroutine(Init());
    }
    
    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : ViewFactory");
    }
    
    private IEnumerator Init()
    {
        while (_gameManager == null)
        {
            _gameManager = StaticManagerWithoutZenject.GameManager;
            yield return null;
        }
        
        // while (_timeGame == null)
        // {
        //     _timeGame = _gameManager.TimeGame;
        //     yield return null;
        // }
        
        Debug.Log("Создать объект: ViewFactory");
        _isInit = true;
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
            case EnumViewFood.Cutlet:
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
