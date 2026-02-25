using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class LoadReleaseGlobalScene : IDisposable
{
    private Dictionary<GlobalPref, GameObject> _globalPrefDic = new Dictionary<GlobalPref, GameObject>();
    private List<GameObject> _loadedPrefabs = new List<GameObject>();

    public IReadOnlyDictionary<GlobalPref, GameObject> GlobalPrefDic => _globalPrefDic;

    public LoadReleaseGlobalScene()
    {
        InitGlobal();
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
    
    
    public async Task LoadSceneAsync(string address)
    {
        AsyncOperationHandle<SceneInstance> handle = 
            Addressables.LoadSceneAsync(address, UnityEngine.SceneManagement.LoadSceneMode.Single);
        
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            //Debug.Log("Сцена загружена и активирована.");
        }
        else
        {
            Debug.LogError("Ошибка при загрузке сцены.");
        }
    }
    
    private void InitGlobal()
    {
        
        var loadingPanelOperation = Addressables.LoadAssetAsync<GameObject>("LoadingPanel");
        
        var loadingPanelPrefab = loadingPanelOperation.WaitForCompletion();
        
        if (loadingPanelPrefab != null)
        {
            _globalPrefDic.Add(GlobalPref.LoadingPanel, loadingPanelPrefab);
            _loadedPrefabs.Add(loadingPanelPrefab);
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