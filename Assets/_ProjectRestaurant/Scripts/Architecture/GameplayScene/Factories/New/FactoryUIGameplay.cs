using UnityEngine;
using Zenject;

public class FactoryUIGameplay
{
    private DiContainer _container;
    private LoadReleaseGameplay _loadReleaseGameplay;
    private GameObject empty = new GameObject("UI_Test");

    private GameObject _gameWindow;
    private GameObject _gameOverWindow;
    private GameObject _menuWindow;
    
    public FactoryUIGameplay(DiContainer container, LoadReleaseGameplay loadReleaseGameplay)
    {
        _container = container;
        _loadReleaseGameplay = loadReleaseGameplay;
    }

    public GameObject CreateShowLoading()
    {
        GameObject empty1 = new GameObject("ShowLoading_Test");
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.GlobalPrefDic[GlobalPref.LoadingPanel], empty1.transform);
        Canvas canvas = obj.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        return obj;
    }
    
    public void CreateUI()
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.UINameDic[UIName.MainFrameCanvas], empty.transform);
        obj.GetComponent<Canvas>().worldCamera = Camera.main;
        
        _gameOverWindow = obj.GetComponentInChildren<GameOverUI>(true).gameObject;
        _container.Bind<GameOverUI>().FromInstance(_gameOverWindow.GetComponent<GameOverUI>()).AsSingle(); //регистрация в контейнер
        _gameOverWindow.SetActive(false);
        
        _gameWindow = obj.GetComponentInChildren<TimeGameUI>(true).gameObject;
        _container.Bind<TimeGameUI>().FromInstance(_gameWindow.GetComponent<TimeGameUI>()).AsSingle(); //регистрация в контейнер
        _gameWindow.SetActive(true);
        
        var ordersUI  = obj.GetComponentInChildren<OrdersUI>(true);
        _container.Bind<OrdersUI>().FromInstance(ordersUI).AsSingle(); //регистрация в контейнер
        
        var checksPanalUI  = obj.GetComponentInChildren<ChecksPanalUI>(true);
        _container.Bind<ChecksPanalUI>().FromInstance(checksPanalUI).AsSingle(); //регистрация в контейнер
        
        _menuWindow = obj.GetComponentInChildren<MenuUI>(true).gameObject;
        _container.Bind<MenuUI>().FromInstance(_menuWindow.GetComponent<MenuUI>()).AsSingle(); //регистрация в контейнер
        _menuWindow.SetActive(false);
    }

    public void HideUI()
    {
        empty.SetActive(false);
    }
    
    public void ShowUI()
    {
        empty.SetActive(true);
    }
    
}
