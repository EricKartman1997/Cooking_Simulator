using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FactoryCamerasMenuScene
{
    private LoadReleaseMainMenuScene _loadRelease;
    private DiContainer _container;
    private GameObject empty = new GameObject("Cameras_Test");

    public FactoryCamerasMenuScene(LoadReleaseMainMenuScene loadRelease, DiContainer container)
    {
        _loadRelease = loadRelease;
        _container = container;
    }

    public void CreateMainCamera()
    {
        _container.InstantiatePrefab(_loadRelease.CamerasDic[CamerasNameMainMenu.MainCamera], empty.transform);
    }
}
