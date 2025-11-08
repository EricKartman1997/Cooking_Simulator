using System;
using Cinemachine;
using UnityEngine;
using Zenject;

public class FactoryCamerasGameplay : IDisposable
{
    private LoadReleaseGameplay _loadReleaseGameplay;
    private DiContainer _container;
    private GameObject empty = new GameObject("Cameras_Test");

    public FactoryCamerasGameplay(LoadReleaseGameplay loadReleaseGameplay, DiContainer container)
    {
        _loadReleaseGameplay = loadReleaseGameplay;
        _container = container;
    }

    public void Dispose()
    {
        Debug.Log("FactoryCamerasGameplay.Dispose");
    }
    
    public void CreateMainCamera()
    {
        _container.InstantiatePrefab(_loadReleaseGameplay.CamerasDic[CamerasName.MainCamera], empty.transform);
    }
    
    public CinemachineVirtualCamera CreateTopDownCamera()
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.CamerasDic[CamerasName.TopDownCamera], empty.transform);
        return obj.GetComponent<CinemachineVirtualCamera>();;
        //CinemachineVirtualCamera _topDownCamera = obj.GetComponent<CinemachineVirtualCamera>();
        //_container.Rebind<CinemachineVirtualCamera>().FromInstance(_topDownCamera).AsSingle();
    }
}
