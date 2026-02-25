using UnityEngine;
using Zenject;

public class ProductsFactory
{
    private LoadReleaseGameplay _loadReleaseGameplay;
    private IInstantiator _instantiator;

    public ProductsFactory(LoadReleaseGameplay loadReleaseGameplay,IInstantiator instantiator)
    {
        _loadReleaseGameplay = loadReleaseGameplay;
        _instantiator = instantiator;
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
     
     public GameObject GetProduct(GameObject pref, Transform transform,Transform parent, bool setActive = true, bool zero = false)
     {
         GameObject obj = _instantiator.InstantiatePrefab(pref, transform.position, Quaternion.identity,parent);
         obj.name = pref.name;
         obj.SetActive(setActive);
         if (zero == true)
         {
             obj.transform.localPosition = Vector3.zero;
             obj.transform.localRotation = Quaternion.identity;
         }
         return obj;
     }
     
     public GameObject GetProduct(IngredientName enumGiveFood, Transform transform,Transform parent)
     {
         GameObject obj1 = GetProductRef(enumGiveFood);
         GameObject obj = _instantiator.InstantiatePrefab(obj1, transform.position, Quaternion.identity,parent);
         return obj;
     }
     
     public GameObject GetProductRef(IngredientName enumGiveFood)
     {
         if (_loadReleaseGameplay.IngredientDic.TryGetValue(enumGiveFood, out var prefab))
         {
             return prefab;
         }

         Debug.LogWarning($"Unknown product type: {enumGiveFood}");
         return null;
     }
     
    private GameObject GetRawCutlet()
    {
        return _instantiator.InstantiatePrefab(_loadReleaseGameplay.IngredientDic[IngredientName.RawCutlet]);
    }
    private GameObject GetMediumCutlet()
    {
        return _instantiator.InstantiatePrefab(_loadReleaseGameplay.IngredientDic[IngredientName.MediumCutlet]);
    }
    private GameObject GetBurnCutlet()
    {
        return _instantiator.InstantiatePrefab(_loadReleaseGameplay.IngredientDic[IngredientName.BurnCutlet]);
    }
    
}
