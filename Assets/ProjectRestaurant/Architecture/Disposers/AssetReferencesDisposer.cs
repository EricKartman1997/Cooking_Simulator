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
    
    private List<GameObject> _rawFoodLoadedList = new List<GameObject>();
    private List<GameObject> _cookedFoodLoadedList = new List<GameObject>();
    private List<GameObject> _otherFoodLoadedList = new List<GameObject>();

    public AssetReferencesDisposer(List<AssetReference> rawFoodList, List<AssetReference> cookedFoodList, List<AssetReference> otherFoodList)
    {
        _rawFoodList = rawFoodList;
        _cookedFoodList = cookedFoodList;
        _otherFoodList = otherFoodList;
    }

    // Основной метод инициализации через корутину
    public IEnumerator Initialize(System.Action onAssetLoadedCallback = null)
    {
        yield return LoadRawFoodAddressable(onAssetLoadedCallback);
        yield return LoadCookedFoodAddressable(onAssetLoadedCallback);
        yield return LoadOtherFoodAddressable(onAssetLoadedCallback);
    }
    
    public void RemoveAllFoodAddressable()
    {
        RemoveRawFoodAddressable();
        RemoveCookedFoodAddressable();
        RemoveOtherFoodAddressable();
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
    
}
