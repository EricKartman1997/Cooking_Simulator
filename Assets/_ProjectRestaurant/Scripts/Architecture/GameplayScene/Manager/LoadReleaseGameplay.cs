using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;


public class LoadReleaseGameplay : IDisposable, IInitializable
{
    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    
    private Dictionary<PrefPlayerNameGameplay, GameObject> _playerDic;
    private List<GameObject> _loadedPrefabs;
    
    private bool _isLoaded;
    
    public IReadOnlyDictionary<GlobalPref, GameObject> GlobalPrefDic => _loadReleaseGlobalScene.GlobalPrefDic;
    public IReadOnlyDictionary<PrefPlayerNameGameplay, GameObject> PrefDic => _playerDic;
    public bool IsLoaded => _isLoaded;

    public LoadReleaseGameplay(LoadReleaseGlobalScene loadReleaseGlobalScene)
    {
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
    }

    public void Dispose()
    {
        ReleasePlayerPrefabs();
        _isLoaded = false;
        Debug.Log("Dispose LoadReleaseGameplay");
    }
    
    public async void Initialize()
    {
        await Task.WhenAll(
            InitPlayerPrefabsAsync()
            //InitEnvironmentPrefabsAsync()
            //InitFurniturePrefabsAsync()
            //InitFoodPrefabsAsync()
            //InitUIPrefabsAsync()
        );
        _isLoaded = true;
    }
    
    private async Task InitPlayerPrefabsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadPrefabAsync("PlayerDefault")
        };

        var results = await Task.WhenAll(loadTasks);
        
        _playerDic.Add(PrefPlayerNameGameplay.Default, results[0]);
    }
    
    private async Task<GameObject> LoadPrefabAsync(string address)
    {
        var operation = Addressables.LoadAssetAsync<GameObject>(address);
        var prefab = await operation.Task;
        _loadedPrefabs.Add(prefab);
        return prefab;
    }
    
    private void ReleasePlayerPrefabs()
    {
        foreach (var prefab in _loadedPrefabs)
        {
            Addressables.Release(prefab);
        }
        _loadedPrefabs.Clear();
        _playerDic.Clear();
    }
}

public enum PrefPlayerNameGameplay
{
    Default
}
