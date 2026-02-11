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

    private TimeGameUI _timeGameUI;
    private OrdersUI _ordersUI;
    private ChecksPanalUI _checksPanalUI;
    private MenuUI _menuUI;
    private StatisticWindowUI _statisticWindowUI;
    private NotificationFiredCutletUI _notificationFiredCutletUI;

    public TimeGameUI TimeGameUI => _timeGameUI;

    public OrdersUI OrdersUI => _ordersUI;
    public ChecksPanalUI ChecksPanalUI => _checksPanalUI;
    public MenuUI MenuUI => _menuUI;
    public StatisticWindowUI StatisticWindowUI => _statisticWindowUI;
    public NotificationFiredCutletUI NotificationFiredCutletUI => _notificationFiredCutletUI;

    //GameObject
    public GameObject GameWindow => _gameWindow;
    public GameObject GameOverWindow => _gameOverWindow;
    public GameObject MenuWindow => _menuWindow;

    
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
        
        _gameWindow = obj.GetComponentInChildren<TimeGameUI>(true).gameObject;
        _timeGameUI = obj.GetComponentInChildren<TimeGameUI>(true);
        //_container.Bind<TimeGameUI>().FromInstance(_gameWindow.GetComponent<TimeGameUI>()).AsSingle(); //регистрация в контейнер
        _gameWindow.SetActive(true);
        
        _ordersUI  = obj.GetComponentInChildren<OrdersUI>(true);
        //_container.Bind<OrdersUI>().FromInstance(ordersUI).AsSingle(); //регистрация в контейнер
        
        _checksPanalUI  = obj.GetComponentInChildren<ChecksPanalUI>(true);
        //_container.Bind<ChecksPanalUI>().FromInstance(checksPanalUI).AsSingle(); //регистрация в контейнер
        
        _menuWindow = obj.GetComponentInChildren<MenuUI>(true).gameObject;
        _menuUI = obj.GetComponentInChildren<MenuUI>(true);
        //_container.Bind<MenuUI>().FromInstance(_menuWindow.GetComponent<MenuUI>()).AsSingle(); //регистрация в контейнер
        _menuWindow.SetActive(false);
        
    }

    public void CreateStatisticsWindow()
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.UINameDic[UIName.StatisticsWindow], empty.transform);
        obj.GetComponent<Canvas>().worldCamera = Camera.main;
        _statisticWindowUI = obj.GetComponentInChildren<StatisticWindowUI>(true);
        _statisticWindowUI.gameObject.SetActive(false);
    }
    
    public void CreateNotificationFiredCutlet()
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.UINameDic[UIName.NotificationFiredCutlet], empty.transform);
        obj.GetComponent<Canvas>().worldCamera = Camera.main;
        _notificationFiredCutletUI = obj.GetComponentInChildren<NotificationFiredCutletUI>(true);
        _notificationFiredCutletUI.gameObject.SetActive(false);
    }

    public void HideUI()
    {
        empty.SetActive(false);
    }
    
    public void HideTime()
    {
        _timeGameUI.gameObject.SetActive(false);
    }
    
    public void HideOrder()
    {
        _ordersUI.gameObject.SetActive(false);
    }
    
    public void HideChecks()
    {
        _checksPanalUI.gameObject.SetActive(false);
    }
    
    public void ShowOrder()
    {
        _ordersUI.gameObject.SetActive(true);
    }
    
    public void ShowChecks()
    {
        _checksPanalUI.gameObject.SetActive(true);
    }
    
    public void ShowUI()
    {
        empty.SetActive(true);
    }
    
}
