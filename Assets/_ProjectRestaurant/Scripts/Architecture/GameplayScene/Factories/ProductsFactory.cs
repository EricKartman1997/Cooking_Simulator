using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class ProductsFactory: IDisposable
{
    private LoadReleaseGameplay _loadReleaseGameplay;

    public ProductsFactory(LoadReleaseGameplay loadReleaseGameplay)
    {
        _loadReleaseGameplay = loadReleaseGameplay;
    }
    
    public void Dispose()
    {
        Debug.Log("У объекта вызван Dispose : ProductsFactory");
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

     // public GameObject GetProduct(EnumGiveFood enumGiveFood)
     // {
     //     switch (enumGiveFood)
     //     {
     //         case EnumGiveFood.Apple:
     //             return Object.Instantiate(_productsContainer.Apple.gameObject);
     //         case EnumGiveFood.Orange:
     //             return Object.Instantiate(_productsContainer.Orange.gameObject);
     //         case EnumGiveFood.Lime:
     //             return Object.Instantiate(_productsContainer.Lime);
     //         case EnumGiveFood.Cherry:
     //             return Object.Instantiate(_productsContainer.Cherry);
     //         case EnumGiveFood.Blueberry:
     //             return Object.Instantiate(_productsContainer.Blueberry);
     //         case EnumGiveFood.Strawberry:
     //             return Object.Instantiate(_productsContainer.Strawberry);
     //         case EnumGiveFood.Fish:
     //             return Object.Instantiate(_productsContainer.Fish.gameObject);
     //         case EnumGiveFood.Meat:
     //             return Object.Instantiate(_productsContainer.Meat.gameObject);
     //         default:
     //             Debug.LogWarning($"Unknown product type: {enumGiveFood}");
     //             return null;
     //     }
     // }
     
     public GameObject GetProductRef(IngredientName enumGiveFood)
     {
         switch (enumGiveFood)
         {
             case IngredientName.Apple:
                 return _loadReleaseGameplay.IngredientDic[IngredientName.Apple];
             case IngredientName.Orange:
                 return _loadReleaseGameplay.IngredientDic[IngredientName.Orange];
             case IngredientName.Lime:
                 return _loadReleaseGameplay.IngredientDic[IngredientName.Lime];
             case IngredientName.Cherry:
                 return _loadReleaseGameplay.IngredientDic[IngredientName.Cherry];
             case IngredientName.Blueberry:
                 return _loadReleaseGameplay.IngredientDic[IngredientName.Blueberry];
             case IngredientName.Strawberry:
                 return _loadReleaseGameplay.IngredientDic[IngredientName.Strawberry];
             case IngredientName.Fish:
                 return _loadReleaseGameplay.IngredientDic[IngredientName.Fish];
             case IngredientName.Meat:
                 return _loadReleaseGameplay.IngredientDic[IngredientName.Meat];
             case IngredientName.RawCutlet:
                 return _loadReleaseGameplay.IngredientDic[IngredientName.RawCutlet];
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
    private GameObject GetRawCutlet()
    {
        return Object.Instantiate(_loadReleaseGameplay.IngredientDic[IngredientName.RawCutlet]);
    }
    private GameObject GetMediumCutlet()
    {
        return Object.Instantiate(_loadReleaseGameplay.IngredientDic[IngredientName.MediumCutlet]);
    }
    private GameObject GetBurnCutlet()
    {
        return Object.Instantiate(_loadReleaseGameplay.IngredientDic[IngredientName.BurnCutlet]);
    }
    
}
