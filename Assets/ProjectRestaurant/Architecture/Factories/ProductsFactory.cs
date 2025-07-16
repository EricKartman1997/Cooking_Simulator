using System;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Collections;

public class ProductsFactory: IDisposable
{
    private GameManager _gameManager;
    private CoroutineMonoBehaviour _coroutineMonoBehaviour;
    private ProductsContainer _productsContainer;

    public ProductsFactory(ProductsContainer productsContainer,CoroutineMonoBehaviour coroutineMonoBehaviour)
    {
        _productsContainer = productsContainer;
        _coroutineMonoBehaviour = coroutineMonoBehaviour;

        _coroutineMonoBehaviour.StartCoroutine(Init());
    }
    
    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : ProductsFactory");
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
        
        Debug.Log("Создать объект: ProductsFactory");
    }


    public GameObject GetCutlet(EnumRoasting roasting)
    {
        switch (roasting)
        {
            case EnumRoasting.Raw:
                return GetRawCutlet();
            
            case EnumRoasting.Medium:
                return GetMediumCutlet();
            
            case EnumRoasting.Burn:
                return GetBurnCutlet();
        }

        return null;
    }
    
    public GameObject GetProduct(GameObject pref,Transform transform,Transform parent, bool setActive = true)
     {
         GameObject obj = Object.Instantiate(pref, transform.position, Quaternion.identity,parent);
         obj.name = pref.name;
         obj.SetActive(setActive);
         return obj;
     }
     
     public GameObject GetProduct(GameObject pref, bool setActive = true)
     {
         GameObject obj = Object.Instantiate(pref);
         obj.name = pref.name;
         obj.SetActive(setActive);
         return obj;
     }
     
     public GameObject GetProduct(GameObject pref, Transform transform,Transform parent, bool setActive = true, bool zero = false)
     {
         GameObject obj = Object.Instantiate(pref, transform.position, Quaternion.identity,parent);
         obj.name = pref.name;
         obj.SetActive(setActive);
         if (zero == true)
         {
             obj.transform.localPosition = Vector3.zero;
             obj.transform.localRotation = Quaternion.identity;
         }
         return obj;
     }

     public GameObject GetProduct(EnumGiveFood enumGiveFood)
     {
         switch (enumGiveFood)
         {
             case EnumGiveFood.Apple:
                 return Object.Instantiate(_productsContainer.Apple.gameObject);
             case EnumGiveFood.Orange:
                 return Object.Instantiate(_productsContainer.Orange.gameObject);
             case EnumGiveFood.Lime:
                 return Object.Instantiate(_productsContainer.Lime);
             case EnumGiveFood.Cherry:
                 return Object.Instantiate(_productsContainer.Cherry);
             case EnumGiveFood.Blueberry:
                 return Object.Instantiate(_productsContainer.Blueberry);
             case EnumGiveFood.Strawberry:
                 return Object.Instantiate(_productsContainer.Strawberry);
             case EnumGiveFood.Fish:
                 return Object.Instantiate(_productsContainer.Fish.gameObject);
             case EnumGiveFood.Meat:
                 return Object.Instantiate(_productsContainer.Meat.gameObject);
             default:
                 Debug.LogWarning($"Unknown product type: {enumGiveFood}");
                 return null;
         }
     }
     
     public GameObject GetProductRef(EnumGiveFood enumGiveFood)
     {
         switch (enumGiveFood)
         {
             case EnumGiveFood.Apple:
                 return _productsContainer.Apple.gameObject;
             case EnumGiveFood.Orange:
                 return _productsContainer.Orange.gameObject;
             case EnumGiveFood.Lime:
                 return _productsContainer.Lime;
             case EnumGiveFood.Cherry:
                 return _productsContainer.Cherry;
             case EnumGiveFood.Blueberry:
                 return _productsContainer.Blueberry;
             case EnumGiveFood.Strawberry:
                 return _productsContainer.Strawberry;
             case EnumGiveFood.Fish:
                 return _productsContainer.Fish.gameObject;
             case EnumGiveFood.Meat:
                 return _productsContainer.Meat.gameObject;
             case EnumGiveFood.RawCutlet:
                 return _productsContainer.RawCutlet.gameObject;
             default:
                 Debug.LogWarning($"Unknown product type: {enumGiveFood}");
                 return null;
         }
     }
     
    // public GameObject GetApple(Transform transform,Transform parent)
    // {
    //    return Object.Instantiate(_apple.gameObject, transform.position, Quaternion.identity,parent);
    // }
    //
    // public GameObject GetOrange(Transform transform,Transform parent)
    // {
    //     return Object.Instantiate(_orange.gameObject, transform.position, Quaternion.identity,parent);
    // }
    //
    // public GameObject GetLime(Transform transform, Transform parent)
    // {
    //     return Object.Instantiate(_lime, transform.position, Quaternion.identity, parent);
    // }
    //
    // public GameObject GetBlueberry(Transform transform, Transform parent)
    // {
    //     return Object.Instantiate(_blueberry, transform.position, Quaternion.identity, parent);
    // }
    //
    // public GameObject GetStrawberry(Transform transform, Transform parent)
    // {
    //     return Object.Instantiate(_strawberry, transform.position, Quaternion.identity, parent);
    // }
    //
    // public GameObject GetCherry(Transform transform, Transform parent)
    // {
    //     return Object.Instantiate(_cherry, transform.position, Quaternion.identity, parent);
    // }
    //
    // public GameObject GetBakedApple(Transform transform, Transform parent)
    // {
    //     return Object.Instantiate(_bakedApple.gameObject, transform.position, Quaternion.identity, parent);
    // }
    //
    // public GameObject GetBakedOrange(Transform transform, Transform parent)
    // {
    //     return Object.Instantiate(_bakedOrange.gameObject, transform.position, Quaternion.identity, parent);
    // }
    //
    // public GameObject GetFruitSalad(Transform transform, Transform parent)
    // {
    //     return Object.Instantiate(_fruitSalad, transform.position, Quaternion.identity, parent);
    // }
    //
    // public GameObject GetMixBakedFruit(Transform transform, Transform parent)
    // {
    //     return Object.Instantiate(_mixBakedFruit, transform.position, Quaternion.identity, parent);
    // }
    //
    // public GameObject GetWildBerryCocktail(Transform transform, Transform parent)
    // {
    //     return Object.Instantiate(_wildBerryCocktail, transform.position, Quaternion.identity, parent);
    // }
    //
    // public GameObject GetFreshnessCocktail(Transform transform, Transform parent)
    // {
    //     return Object.Instantiate(_freshnessCocktail, transform.position, Quaternion.identity, parent);
    // }
    //
    // public GameObject GetMeat(Transform transform, Transform parent)
    // {
    //     return Object.Instantiate(_meat.gameObject, transform.position, Quaternion.identity, parent);
    // }
    //
    // public GameObject GetFish(Transform transform, Transform parent)
    // {
    //     return Object.Instantiate(_fish.gameObject, transform.position, Quaternion.identity, parent);
    // }
    //
    // public GameObject GetBakedMeat(Transform transform, Transform parent)
    // {
    //     return Object.Instantiate(_bakedMeat.gameObject, transform.position, Quaternion.identity, parent);
    // }
    //
    // public GameObject GetBakedFish(Transform transform, Transform parent)
    // {
    //     return Object.Instantiate(_bakedFish.gameObject, transform.position, Quaternion.identity, parent);
    // }
    //
    // public GameObject GetRubbish(Transform transform, Transform parent)
    // {
    //     return Object.Instantiate(_rubbish, transform.position, Quaternion.identity, parent);
    // }
    public GameObject GetRawCutlet()
    {
        return Object.Instantiate(_productsContainer.RawCutlet).gameObject;
    }
    public GameObject GetMediumCutlet()
    {
        return Object.Instantiate(_productsContainer.MediumCutlet).gameObject;
    }
    public GameObject GetBurnCutlet()
    {
        return Object.Instantiate(_productsContainer.BurnCutlet).gameObject;
    }
    
}
