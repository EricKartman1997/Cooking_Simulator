using System;
using UnityEngine;
using Zenject;

public class FactoryPlayerGameplay : IDisposable
{
    private DiContainer _container;
    private LoadReleaseGameplay _loadReleaseGameplay;

    public FactoryPlayerGameplay(DiContainer container, LoadReleaseGameplay loadReleaseGameplay)
    {
        _container = container;
        _loadReleaseGameplay = loadReleaseGameplay;
    }

    public void Dispose()
    {
        Debug.Log("FactoryPlayerGameplay.Dispose");
    }

    public void CreatePlayer(Transform point, Transform parent)
    {
        GameObject player = _container.InstantiatePrefab(_loadReleaseGameplay.PrefDic[PlayerName.RobotPlayer], point.position, Quaternion.identity, parent);
        _container.Rebind<Heroik>().FromInstance(player.GetComponent<Heroik>()).AsSingle();
    }
    
    
}
