using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class LoadReleaseGlobalScene : IDisposable
{
    private Dictionary<GlobalPref, GameObject> _globalPrefDic = new Dictionary<GlobalPref, GameObject>();
    private List<GameObject> _loadedPrefabs = new List<GameObject>();

    public IReadOnlyDictionary<GlobalPref, GameObject> GlobalPrefDic => _globalPrefDic;

    public LoadReleaseGlobalScene()
    {
        InitGlobal();
        //Debug.Log("завершил инициализацию LoadReleaseGlobalScene");
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
        //await Addressables.LoadAssetAsync<Scene>(address).Task; // дописать
        
        AsyncOperationHandle<SceneInstance> handle = 
            Addressables.LoadSceneAsync(address, UnityEngine.SceneManagement.LoadSceneMode.Single);

        // Дожидаемся окончания загрузки
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Сцена загружена и активирована.");
        }
        else
        {
            Debug.LogError("Ошибка при загрузке сцены.");
        }
    }
    
    private void InitGlobal()
    {
        
        // Синхронная загрузка через Addressables (блокирующий вызов)
        var loadingPanelOperation = Addressables.LoadAssetAsync<GameObject>("LoadingPanel");
        
        // Ожидаем завершения операции
        var loadingPanelPrefab = loadingPanelOperation.WaitForCompletion();
        
        if (loadingPanelPrefab != null)
        {
            _globalPrefDic.Add(GlobalPref.LoadingPanel, loadingPanelPrefab);
            _loadedPrefabs.Add(loadingPanelPrefab);
            //Debug.Log("Создал панель");
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