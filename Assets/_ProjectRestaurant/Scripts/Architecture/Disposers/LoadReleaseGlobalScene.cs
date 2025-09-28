using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LoadReleaseGlobalScene : IDisposable
{
    private Dictionary<GlobalPref, GameObject> _globalPrefDic;
    private List<GameObject> _loadedPrefabs;

    public IReadOnlyDictionary<GlobalPref, GameObject> GlobalPrefDic => _globalPrefDic;

    public LoadReleaseGlobalScene()
    {
        _globalPrefDic = new Dictionary<GlobalPref, GameObject>();
        _loadedPrefabs = new List<GameObject>();
        InitGlobal();
        Debug.Log("вошел в конструктор LoadReleaseGlobalScene");
    }

    public void Dispose()
    {
        foreach (var prefab in _loadedPrefabs)
        {
            Addressables.Release(prefab);
        }
        _loadedPrefabs.Clear();
        _globalPrefDic.Clear();
        Debug.Log("Dispose LoadReleaseGlobalScene");
    }
    
    private void InitGlobal()
    {
        
        // Синхронная загрузка через Addressables (блокирующий вызов)
        var loadingPanelOperation = Addressables.LoadAssetAsync<GameObject>(
            "Assets/_ProjectRestaurant/UI/_Prefabs/Global/LoadingPanel/LoadingPanel.prefab");
        
        // Ожидаем завершения операции
        var loadingPanelPrefab = loadingPanelOperation.WaitForCompletion();
        
        if (loadingPanelPrefab != null)
        {
            _globalPrefDic.Add(GlobalPref.LoadingPanel, loadingPanelPrefab);
            _loadedPrefabs.Add(loadingPanelPrefab);
            Debug.Log("Создал панель");
        }
        else
        {
            Debug.LogError("Не удалось загрузить LoadingPanel");
        }
    }
}

public enum GlobalPref
{
    LoadingPanel
}