using System;
using UnityEngine;
using Zenject;

public class FactoryUIGameplay: IDisposable
{
    private DiContainer _container;
    private LoadReleaseGameplay _loadReleaseGameplay;
    private GameObject empty = new GameObject("UI_Test");

    private GameObject _gameWindow;
    private GameObject _gameOverWindow;
    private GameObject _menuWindow;
    
    
    // public GameObject GameWindow => _gameWindow;
    //
    // public GameObject GameOverWindow => _gameOverWindow;
    // public GameObject MenuWindow => _menuWindow;

    public FactoryUIGameplay(DiContainer container, LoadReleaseGameplay loadReleaseGameplay)
    {
        _container = container;
        _loadReleaseGameplay = loadReleaseGameplay;
    }

    public void Dispose()
    {
        Debug.Log("Dispose : FactoryUIGameplay");
    }
    
    // public void CreateUI(Transform parent)
    // {
    //     if (parent == null)
    //     {
    //         Debug.LogError("Ошибка создания UI");
    //     }
    //     GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.UINameDic[UIName.GameOverWindow], parent.transform.position, Quaternion.identity, parent.transform);
    //     obj.transform.localPosition = Vector3.zero;
    //     obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
    //     obj.transform.localScale = Vector3.one;
    //     obj.SetActive(false);
    //     
    //     GameObject obj1 = _container.InstantiatePrefab(_loadReleaseGameplay.UINameDic[UIName.GameWindow], parent.transform.position, Quaternion.identity, parent.transform);
    //     RectTransform rect = obj1.GetComponent<RectTransform>();
    //     rect.offsetMin = Vector2.zero;
    //     rect.offsetMax = Vector2.zero;
    //     obj1.transform.localRotation = Quaternion.Euler(Vector3.zero);
    //     obj1.transform.localScale = Vector3.one;
    //     //obj.GetComponent<Canvas>().worldCamera = Camera.main;
    // }
    
    public GameObject CreateUI()
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
        
        return obj;
        
    }
    
}
