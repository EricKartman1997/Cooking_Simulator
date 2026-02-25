using UnityEngine;
using Zenject;

public class FactoryUIMainMenuScene
{
    private IInstantiator _container;
    private LoadReleaseMainMenuScene _loadReleaseMainMenuScene;
    private GameObject empty = new GameObject("UI_Test");

    private NotificationTrainingUI _notificationTrainingUI;

    public NotificationTrainingUI NotificationTraining => _notificationTrainingUI;

    public FactoryUIMainMenuScene(IInstantiator container, LoadReleaseMainMenuScene loadReleaseMainMenuScene)
    {
        _container = container;
        _loadReleaseMainMenuScene = loadReleaseMainMenuScene;
    }
    
    public GameObject CreateUI()
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseMainMenuScene.UIDic[UINameMainMenu.UIPanel], empty.transform);
        
        return obj;
    }
    
    public void NotificationTrainingUI()
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseMainMenuScene.UIDic[UINameMainMenu.NotificationTraining], empty.transform);
        obj.GetComponent<Canvas>().worldCamera = Camera.main;
        _notificationTrainingUI = obj.GetComponent<NotificationTrainingUI>();
        obj.gameObject.SetActive(false);
    }
    
    public GameObject CreateShowLoading()
    {
        GameObject empty1 = new GameObject("ShowLoading_Test");
        GameObject obj = _container.InstantiatePrefab(_loadReleaseMainMenuScene.GlobalPrefDic[GlobalPref.LoadingPanel], empty1.transform);
        Canvas canvas = obj.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        return obj;
    }
    
}
