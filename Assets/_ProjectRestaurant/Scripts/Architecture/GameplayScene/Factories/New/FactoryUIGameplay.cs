using System;
using UnityEngine;
using Zenject;

public class FactoryUIGameplay: IDisposable
{
    private IInstantiator _container;
    private LoadReleaseGameplay _loadReleaseGameplay;
    private GameObject empty = new GameObject("UI_Test");

    public FactoryUIGameplay(IInstantiator container, LoadReleaseGameplay loadReleaseGameplay)
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
        GameObject GameOverWindow = obj.GetComponentInChildren<GameOverUI>(true).gameObject;
        GameObject GameWindow = obj.GetComponentInChildren<TimeGameUI>(true).gameObject;
        GameWindow.SetActive(true);
        GameOverWindow.SetActive(false);
        return obj;
        
    }


}
