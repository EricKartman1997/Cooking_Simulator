using Cinemachine;
using UnityEngine;
using Zenject;

public class FactoryPlayerGameplay
{
    private DiContainer _container;
    private LoadReleaseGameplay _loadReleaseGameplay;
    private Heroik _heroik;
    private ISoundsService _soundsService;

    public FactoryPlayerGameplay(DiContainer container, LoadReleaseGameplay loadReleaseGameplay,ISoundsService soundsService)
    {
        _container = container;
        _loadReleaseGameplay = loadReleaseGameplay;
        _soundsService = soundsService;
    }
    
    public void CreatePlayer(CinemachineVirtualCamera camera)
    {
        GameObject empty = new GameObject("Player_Test");
        GameObject player = _container.InstantiatePrefab(_loadReleaseGameplay.PlayerDic[PlayerName.RobotPlayer], new Vector3(0.19f,0,-6.12f), Quaternion.identity, empty.transform);
        player.GetComponent<HeroikSoundsControl>().InitStateMachine();
        EnableCamera(camera, player);
    }
    
    public void CreatePlayerTraining(CinemachineVirtualCamera camera)
    {
        GameObject empty = new GameObject("Player_Test");
        GameObject player = _container.InstantiatePrefab(_loadReleaseGameplay.PlayerDic[PlayerName.RobotPlayer], new Vector3(0.19f,0,-6.12f), Quaternion.identity, empty.transform);
        player.GetComponent<HeroikSoundsControl>();
        EnableCamera(camera, player);
    }
    
    private void EnableCamera(CinemachineVirtualCamera camera, GameObject target)
    {
        if (target == null || camera == null)
        {
            //Debug.Log("Camera not enabled");
            return;
        }

        GameObject empty = new GameObject("PointCamera");
        empty.transform.SetParent(target.transform);      // задаём родителя
        empty.transform.localPosition = new Vector3(0, 1.2f, 0); // позиция относительно родителя
        
        camera.Follow = empty.transform;
        camera.LookAt = empty.transform;
        //Debug.Log($"Camera enabled -> Follow: {camera.Follow}, LookAt: {camera.LookAt}");
    }
    
    
}
