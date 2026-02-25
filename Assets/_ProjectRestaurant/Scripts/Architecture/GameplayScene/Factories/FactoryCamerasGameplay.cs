using Cinemachine;
using UnityEngine;
using Zenject;

public class FactoryCamerasGameplay
{
    private LoadReleaseGameplay _loadReleaseGameplay;
    private DiContainer _container;
    private IInstantiator _instantiator;
    private GameObject empty = new GameObject("Cameras_Test");

    public FactoryCamerasGameplay(LoadReleaseGameplay loadReleaseGameplay, DiContainer container)
    {
        _loadReleaseGameplay = loadReleaseGameplay;
        _container = container;
    }
    
    public void CreateMainCamera()
    {
        _container.InstantiatePrefab(_loadReleaseGameplay.CamerasDic[CamerasNameGameplay.MainCamera], empty.transform);
    }
    
    public CinemachineVirtualCamera CreateTopDownCamera()
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.CamerasDic[CamerasNameGameplay.TopDownCamera], empty.transform);
        return obj.GetComponent<CinemachineVirtualCamera>();;
    }
}
