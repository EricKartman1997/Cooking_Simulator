using System;
using UnityEngine;
using Zenject;

public class FactoryPlayerGameplay : IDisposable
{
    private IInstantiator _container;
    private LoadReleaseGameplay _loadReleaseGameplay;
    public void Dispose()
    {
        Debug.Log("FactoryPlayerGameplay.Dispose");
    }

    public GameObject CreatePlayer(Transform point, Transform parent)
    {
        return _container.InstantiatePrefab(_loadReleaseGameplay.PrefDic[PrefPlayerNameGameplay.Default], point.position, Quaternion.identity, parent);
    }
    
    
}
