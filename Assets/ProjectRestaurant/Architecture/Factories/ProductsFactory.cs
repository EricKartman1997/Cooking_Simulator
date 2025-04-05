using UnityEngine;
using Object = UnityEngine.Object;

public class ProductsFactory
{
     private Product _apple;
     private Product _orange;
     private GameObject _lime;
     private GameObject _blueberry;
     private GameObject _strawberry;
     private GameObject _cherry;
    
     private Product _bakedApple;
     private Product _bakedOrange;

     private GameObject _fruitSalad;
     private GameObject _mixBakedFruit;
     private GameObject _wildBerryCocktail;
     private GameObject _freshnessCocktail;
    
     private Product _meat;
     private Product _fish;
    
     private Product _bakedMeat;
     private Product _bakedFish;
    
     private GameObject _rubbish;

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
    public GameObject GetApple(Transform transform,Transform parent)
    {
       return Object.Instantiate(_apple.gameObject, transform.position, Quaternion.identity,parent);
    }
    
    public GameObject GetOrange(Transform transform,Transform parent)
    {
        return Object.Instantiate(_orange.gameObject, transform.position, Quaternion.identity,parent);
    }
    
    public GameObject GetLime(Transform transform, Transform parent)
    {
        return Object.Instantiate(_lime, transform.position, Quaternion.identity, parent);
    }

    public GameObject GetBlueberry(Transform transform, Transform parent)
    {
        return Object.Instantiate(_blueberry, transform.position, Quaternion.identity, parent);
    }

    public GameObject GetStrawberry(Transform transform, Transform parent)
    {
        return Object.Instantiate(_strawberry, transform.position, Quaternion.identity, parent);
    }

    public GameObject GetCherry(Transform transform, Transform parent)
    {
        return Object.Instantiate(_cherry, transform.position, Quaternion.identity, parent);
    }

    public GameObject GetBakedApple(Transform transform, Transform parent)
    {
        return Object.Instantiate(_bakedApple.gameObject, transform.position, Quaternion.identity, parent);
    }

    public GameObject GetBakedOrange(Transform transform, Transform parent)
    {
        return Object.Instantiate(_bakedOrange.gameObject, transform.position, Quaternion.identity, parent);
    }

    public GameObject GetFruitSalad(Transform transform, Transform parent)
    {
        return Object.Instantiate(_fruitSalad, transform.position, Quaternion.identity, parent);
    }

    public GameObject GetMixBakedFruit(Transform transform, Transform parent)
    {
        return Object.Instantiate(_mixBakedFruit, transform.position, Quaternion.identity, parent);
    }

    public GameObject GetWildBerryCocktail(Transform transform, Transform parent)
    {
        return Object.Instantiate(_wildBerryCocktail, transform.position, Quaternion.identity, parent);
    }

    public GameObject GetFreshnessCocktail(Transform transform, Transform parent)
    {
        return Object.Instantiate(_freshnessCocktail, transform.position, Quaternion.identity, parent);
    }

    public GameObject GetMeat(Transform transform, Transform parent)
    {
        return Object.Instantiate(_meat.gameObject, transform.position, Quaternion.identity, parent);
    }

    public GameObject GetFish(Transform transform, Transform parent)
    {
        return Object.Instantiate(_fish.gameObject, transform.position, Quaternion.identity, parent);
    }

    public GameObject GetBakedMeat(Transform transform, Transform parent)
    {
        return Object.Instantiate(_bakedMeat.gameObject, transform.position, Quaternion.identity, parent);
    }

    public GameObject GetBakedFish(Transform transform, Transform parent)
    {
        return Object.Instantiate(_bakedFish.gameObject, transform.position, Quaternion.identity, parent);
    }

    public GameObject GetRubbish(Transform transform, Transform parent)
    {
        return Object.Instantiate(_rubbish, transform.position, Quaternion.identity, parent);
    }
}
