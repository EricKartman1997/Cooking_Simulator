using System;
using UnityEngine;
using Zenject;

public class FactoryUIMainMenuScene
{
    private IInstantiator _container;
    private LoadReleaseMainMenuScene _loadReleaseMainMenuScene;
    private GameObject empty = new GameObject("UI_Test");

    public FactoryUIMainMenuScene(IInstantiator container, LoadReleaseMainMenuScene loadReleaseMainMenuScene)
    {
        _container = container;
        _loadReleaseMainMenuScene = loadReleaseMainMenuScene;
    }
    
    // public GameObject Get(PrefUINameMainMenu uiName, Transform parent)
    // {
    //     switch (uiName)
    //     {
    //         case PrefUINameMainMenu.UIPanel:
    //             return _container.InstantiatePrefab(_loadReleaseMainMenuScene.PrefDic[PrefUINameMainMenu.UIPanel], parent);
    //         default:
    //             throw new ArgumentException(nameof(uiName));
    //     }
    // }
    
    public GameObject CreateUI()
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseMainMenuScene.UIDic[UINameMainMenu.UIPanel], empty.transform);
        
        return obj;
        
    }
    
    // public Canvas CreateShowLoading()
    // {
    //     GameObject empty1 = new GameObject("ShowLoading_Test");
    //     Canvas canvas = _container.InstantiatePrefab(_loadReleaseMainMenuScene.UIDic[UINameMainMenu.CanvasShowLoading], empty1.transform).GetComponent<Canvas>();
    //     canvas.worldCamera = Camera.main;
    //     return canvas;
    // }
    
    public GameObject CreateShowLoading()
    {
        GameObject empty1 = new GameObject("ShowLoading_Test");
        GameObject obj = _container.InstantiatePrefab(_loadReleaseMainMenuScene.GlobalPrefDic[GlobalPref.LoadingPanel], empty1.transform);
        Canvas canvas = obj.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        return obj;
    }
    
}
