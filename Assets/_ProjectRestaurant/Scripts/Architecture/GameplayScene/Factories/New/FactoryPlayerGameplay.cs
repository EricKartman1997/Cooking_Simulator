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

    public void CreatePlayer(CinemachineVirtualCamera camera)
    {
        GameObject empty = new GameObject("Player_Test");
        GameObject player = _container.InstantiatePrefab(_loadReleaseGameplay.PrefDic[PlayerName.RobotPlayer], empty.transform.position, Quaternion.identity, empty.transform);
        _heroik = player.GetComponent<Heroik>();
        EnableCamera(camera, player);
        _container.Rebind<Heroik>().FromInstance(_heroik).AsSingle();
    }
    
    private void EnableCamera(CinemachineVirtualCamera camera, GameObject target)
    {
        if (target == null || camera == null)
        {
            Debug.Log("Camera not enabled");
            return;
        }

        GameObject empty = new GameObject("PointCamera");
        empty.transform.SetParent(target.transform);      // задаём родителя
        empty.transform.localPosition = new Vector3(0, 1.2f, 0); // позиция относительно родителя
        
        camera.Follow = empty.transform;
        camera.LookAt = empty.transform;
        Debug.Log($"Camera enabled -> Follow: {camera.Follow}, LookAt: {camera.LookAt}");
    }
    
    
}
