using System;
using UnityEngine;
using Zenject;

public class FactoryPlayerGameplay : IDisposable
{
    private IInstantiator _container;
    private LoadReleaseGameplay _loadReleaseGameplay;

    public FactoryPlayerGameplay(IInstantiator container, LoadReleaseGameplay loadReleaseGameplay)
    {
        _container = container;
        _loadReleaseGameplay = loadReleaseGameplay;
    }

    public void Dispose()
    {
        Debug.Log("FactoryPlayerGameplay.Dispose");
    }

    public GameObject CreatePlayer(Transform point, Transform parent)
    {
        return _container.InstantiatePrefab(_loadReleaseGameplay.PrefDic[PlayerName.RobotPlayer], point.position, Quaternion.identity, parent);
    }
    
    
}
