using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Zenject;

public class FactoryUIGameplay: IDisposable
{
    private IInstantiator _container;
    private LoadReleaseGameplay _loadReleaseGameplay;

    public FactoryUIGameplay(IInstantiator container, LoadReleaseGameplay loadReleaseGameplay)
    {
        _container = container;
        _loadReleaseGameplay = loadReleaseGameplay;
    }

    public void Dispose()
    {
        Debug.Log("Dispose : FactoryUIGameplay");
    }
    
    public CinemachineVirtualCamera CreateCameras()
    {
        GameObject empty = new GameObject("Cameras_Test");
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.UINameDic[UIName.Cameras], empty.transform.position, Quaternion.identity, empty.transform);
        return obj.GetComponentInChildren<CinemachineVirtualCamera>();

    }
    
    public void CreateUI(Transform parent)
    {
        if (parent == null)
        {
            Debug.LogError("Ошибка создания UI");
        }
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.UINameDic[UIName.GameOverWindow], parent.transform.position, Quaternion.identity, parent.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
        obj.transform.localScale = Vector3.one;
        obj.SetActive(false);
        
        GameObject obj1 = _container.InstantiatePrefab(_loadReleaseGameplay.UINameDic[UIName.GameWindow], parent.transform.position, Quaternion.identity, parent.transform);
        RectTransform rect = obj1.GetComponent<RectTransform>();
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        obj1.transform.localRotation = Quaternion.Euler(Vector3.zero);
        obj1.transform.localScale = Vector3.one;
        //obj.GetComponent<Canvas>().worldCamera = Camera.main;
    }


}
