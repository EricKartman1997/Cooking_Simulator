using System;
using UnityEngine;
using Zenject;

public class FactoryUIMainMenuScene
{
    private IInstantiator _container;
    private LoadReleaseMainMenuScene _loadReleaseMainMenuScene;

    public FactoryUIMainMenuScene(IInstantiator container, LoadReleaseMainMenuScene loadReleaseMainMenuScene)
    {
        _container = container;
        _loadReleaseMainMenuScene = loadReleaseMainMenuScene;
    }
    
    public GameObject Get(PrefUINameMainMenu uiName, Transform parent)
    {
        switch (uiName)
        {
            case PrefUINameMainMenu.UIPanel:
                return _container.InstantiatePrefab(_loadReleaseMainMenuScene.PrefDic[PrefUINameMainMenu.UIPanel], parent);
            default:
                throw new ArgumentException(nameof(uiName));
        }
    }
    
}
