using System;
using Cinemachine;
using UnityEngine;
using Zenject;

public class FactoryPlayerGameplay : IDisposable
{
    private DiContainer _container;
    private LoadReleaseGameplay _loadReleaseGameplay;
    private Heroik _heroik;

    public FactoryPlayerGameplay(DiContainer container, LoadReleaseGameplay loadReleaseGameplay)
    {
        _container = container;
        _loadReleaseGameplay = loadReleaseGameplay;
    }

    public void Dispose()
    {
        Debug.Log("FactoryPlayerGameplay.Dispose");
    }

    public void CreatePlayer(Transform point, Transform parent, CinemachineVirtualCamera camera)
    {
        GameObject player = _container.InstantiatePrefab(_loadReleaseGameplay.PrefDic[PlayerName.RobotPlayer], point.position, Quaternion.identity, parent);
        _heroik = player.GetComponent<Heroik>();
        EnableCamera(camera, player.transform);
        _container.Rebind<Heroik>().FromInstance(_heroik).AsSingle();
    }
    
    public void EnableCamera(CinemachineVirtualCamera camera, Transform target)
    {
        if (_heroik == null || camera == null)
        {
            Debug.Log("Camera not enabled");
            return;
        }

        // if (_heroik.PointCamera == null)
        // {
        //     Debug.LogWarning("_heroik.PointCamera is null!");
        //     return;
        // }

        camera.Follow = target;
        camera.LookAt = target;
        Debug.Log($"Camera enabled -> Follow: {camera.Follow}, LookAt: {camera.LookAt}");
    }
    
    
}
