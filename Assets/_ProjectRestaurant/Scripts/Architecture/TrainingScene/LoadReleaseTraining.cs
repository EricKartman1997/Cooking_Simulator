using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class LoadReleaseTraining : IDisposable, IInitializable
{
    private Dictionary<FurnitureNameTraining, GameObject> _furnitureDic = new Dictionary<FurnitureNameTraining, GameObject>();
    private Dictionary<UINameTraining, GameObject> _uiDic = new Dictionary<UINameTraining, GameObject>();

    private List<GameObject> _loadedPrefabs = new List<GameObject>();
    private bool _isLoaded;
    
    public IReadOnlyDictionary<FurnitureNameTraining, GameObject> FurnitureDic => _furnitureDic;
    public IReadOnlyDictionary<UINameTraining, GameObject> UINameDic => _uiDic;
    public bool IsLoaded => _isLoaded;

    public void Dispose()
    {
        ReleasePlayerPrefabs();
        _isLoaded = false;
    }

    public async void Initialize()
    {
        await Task.WhenAll(
            LoadFurniturePrefabsAsync(), 
            LoadUIPrefabsAsync()
        );
        
        await UniTask.Yield();
        
        _isLoaded = true;
    }
    
    private async Task LoadFurniturePrefabsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("CuttingTableTraining"),
            LoadGameObjectAsync("GetTableTraining"),
            LoadGameObjectAsync("GiveTableTraining"),
            LoadGameObjectAsync("DistributionTraining"),
            LoadGameObjectAsync("GarbageTraining"),

        };

        var results = await Task.WhenAll(loadTasks);
        
        _furnitureDic.Add(FurnitureNameTraining.CuttingTableTraining, results[0]);
        _furnitureDic.Add(FurnitureNameTraining.GetTableTraining, results[1]);
        _furnitureDic.Add(FurnitureNameTraining.GiveTableTraining, results[2]);
        _furnitureDic.Add(FurnitureNameTraining.DistributionTraining, results[3]);
        _furnitureDic.Add(FurnitureNameTraining.GarbageTraining, results[4]);
    }
    
    private async Task LoadUIPrefabsAsync()
    {
        var loadTasks = new List<Task<GameObject>>
        {
            LoadGameObjectAsync("TaskTraining"),
            LoadGameObjectAsync("EndTraining"),
            LoadGameObjectAsync("MiniTaskTraining"),
            LoadGameObjectAsync("StartTraining")
        };

        var results = await Task.WhenAll(loadTasks);
        
        _uiDic.Add(UINameTraining.TaskTraining, results[0]);
        _uiDic.Add(UINameTraining.EndTraining, results[1]);
        _uiDic.Add(UINameTraining.MiniTaskTraining, results[2]);
        _uiDic.Add(UINameTraining.StartTraining, results[3]);
    }
    
    private async Task<GameObject> LoadGameObjectAsync(string address)
    {
        var operation = Addressables.LoadAssetAsync<GameObject>(address);
        var prefab = await operation.Task;
        if (prefab != null)
        {
            _loadedPrefabs.Add(prefab);
            return prefab;
        }
        Debug.LogError("Ошибка загрузки LoadGameObjectAsync");
        return null;
    }
    
    private void ReleasePlayerPrefabs()
    {
        foreach (var prefab in _loadedPrefabs)
        {
            Addressables.Release(prefab);
        }
        _loadedPrefabs.Clear();
    }
}

public enum UINameTraining
{
    TaskTraining,
    EndTraining,
    MiniTaskTraining,
    StartTraining
}

public enum FurnitureNameTraining
{
    CuttingTableTraining,
    GetTableTraining,
    GiveTableTraining,
    DistributionTraining,
    GarbageTraining
}