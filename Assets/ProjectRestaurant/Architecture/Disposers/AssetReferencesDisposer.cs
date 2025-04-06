using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetReferencesDisposer
{
    private List<AssetReference> _rawFoodList;
    private List<AssetReference> _cookedFoodList;
    private List<AssetReference> _otherFoodList;
    private List<AssetReference> _viewDishList;
    private List<AssetReference> _castomsObjectsList;
    
    private List<GameObject> _rawFoodLoadedList = new List<GameObject>();
    private List<GameObject> _cookedFoodLoadedList = new List<GameObject>();
    private List<GameObject> _otherFoodLoadedList = new List<GameObject>();
    private List<GameObject> _viewDishLoadedList = new List<GameObject>();
    private List<GameObject> _castomsObjectsLoadedList = new List<GameObject>();


    public AssetReferencesDisposer(List<AssetReference> rawFoodList, List<AssetReference> cookedFoodList, List<AssetReference> otherFoodList,List<AssetReference> viewDish,List<AssetReference> castomsObjects)
    {
        _rawFoodList = rawFoodList;
        _cookedFoodList = cookedFoodList;
        _otherFoodList = otherFoodList;
        _viewDishList = viewDish;
        _castomsObjectsList = castomsObjects;
    }

    // Основной метод инициализации через корутину
    public IEnumerator Initialize(System.Action onAssetLoadedCallback = null)
    {
        yield return LoadRawFoodAddressable(onAssetLoadedCallback);
        yield return LoadCookedFoodAddressable(onAssetLoadedCallback);
        yield return LoadOtherFoodAddressable(onAssetLoadedCallback);
        yield return LoadViewDishAddressable(onAssetLoadedCallback);
        yield return LoadCastomsObjectsAddressable(onAssetLoadedCallback);
    }
    
    public void RemoveAllFoodAddressable()
    {
        RemoveRawFoodAddressable();
        RemoveCookedFoodAddressable();
        RemoveOtherFoodAddressable();
        RemoveViewDishAddressable();
        RemoveCastomsObjectsAddressable();
    }
    
    private IEnumerator LoadRawFoodAddressable(System.Action onAssetLoadedCallback)
    {
        foreach (var food in _rawFoodList)
        {
            AsyncOperationHandle<GameObject> handle = food.LoadAssetAsync<GameObject>();
            yield return handle; // Ожидаем загрузку
            
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _rawFoodLoadedList.Add(handle.Result);
                onAssetLoadedCallback?.Invoke();
            }
            else
            {
                Debug.Log("Ошибка загрузки: " + handle.Result);
            }
        }
    }
    
    private IEnumerator LoadCookedFoodAddressable(System.Action onAssetLoadedCallback)
    {
        foreach (var food in _cookedFoodList)
        {
            AsyncOperationHandle<GameObject> handle = food.LoadAssetAsync<GameObject>();
            yield return handle;
            
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _cookedFoodLoadedList.Add(handle.Result);
                onAssetLoadedCallback?.Invoke();
            }
            else
            {
                Debug.Log("Ошибка загрузки: " + handle.Result);
            }
        }
    }
    
    private IEnumerator LoadOtherFoodAddressable(System.Action onAssetLoadedCallback)
    {
        foreach (var food in _otherFoodList)
        {
            AsyncOperationHandle<GameObject> handle = food.LoadAssetAsync<GameObject>();
            yield return handle;
            
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _otherFoodLoadedList.Add(handle.Result);
                onAssetLoadedCallback?.Invoke();
            }
            else
            {
                Debug.Log("Ошибка загрузки: " + handle.Result);
            }
        }
    }
    
    private IEnumerator LoadViewDishAddressable(System.Action onAssetLoadedCallback)
    {
        foreach (var food in _viewDishList)
        {
            AsyncOperationHandle<GameObject> handle = food.LoadAssetAsync<GameObject>();
            yield return handle;
            
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _viewDishLoadedList.Add(handle.Result);
                onAssetLoadedCallback?.Invoke();
            }
            else
            {
                Debug.Log("Ошибка загрузки: " + handle.Result);
            }
        }
    }
    
    private IEnumerator LoadCastomsObjectsAddressable(System.Action onAssetLoadedCallback)
    {
        foreach (var food in _castomsObjectsList)
        {
            AsyncOperationHandle<GameObject> handle = food.LoadAssetAsync<GameObject>();
            yield return handle;
            
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _castomsObjectsLoadedList.Add(handle.Result);
                onAssetLoadedCallback?.Invoke();
            }
            else
            {
                Debug.Log("Ошибка загрузки: " + handle.Result);
            }
        }
    }
    
    private void RemoveRawFoodAddressable()
    {
        foreach (var food in _rawFoodLoadedList)
        {
            Addressables.Release(food);
        }
        _rawFoodLoadedList.Clear();
    }
    
    private void RemoveCookedFoodAddressable()
    {
        foreach (var food in _cookedFoodLoadedList)
        {
            Addressables.Release(food);
        }
        _cookedFoodLoadedList.Clear();
    }
    
    private void RemoveOtherFoodAddressable()
    {
        foreach (var food in _otherFoodLoadedList)
        {
            Addressables.Release(food);
        }
        _otherFoodLoadedList.Clear();
    }
    
    private void RemoveViewDishAddressable()
    {
        foreach (var food in _viewDishLoadedList)
        {
            Addressables.Release(food);
        }
        _viewDishLoadedList.Clear();
    }
    
    private void RemoveCastomsObjectsAddressable()
    {
        foreach (var food in _castomsObjectsLoadedList)
        {
            Addressables.Release(food);
        }
        _castomsObjectsLoadedList.Clear();
    }
    
}
