using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FactoryEnvironment : IDisposable
{
    private IInstantiator _container;
    private LoadReleaseGameplay _loadReleaseGameplay;
    private List<Transform> _pointsList; //заполнить

    public FactoryEnvironment(IInstantiator container, LoadReleaseGameplay loadReleaseGameplay)
    {
        _container = container;
        _loadReleaseGameplay = loadReleaseGameplay;
    }
    public void Dispose()
    {
        Debug.Log("FactoryEnvironment.Dispose");
    }

    public void CreateFurnitureGamePlay(Transform parentFurniture)
    {
        CreateGiveTable(_pointsList[0], parentFurniture); // не заполнено
        CreateGiveTable(_pointsList[1], parentFurniture);
        CreateCuttingTable(_pointsList[2], parentFurniture);
        CreateGetTable(_pointsList[3], parentFurniture);
        CreateGetTable(_pointsList[4], parentFurniture);
        CreateGetTable(_pointsList[5], parentFurniture);
        CreateGetTable(_pointsList[6], parentFurniture);
        CreateGetTable(_pointsList[7], parentFurniture);
        CreateGetTable(_pointsList[8], parentFurniture);
        CreateGetTable(_pointsList[9], parentFurniture);
        CreateGetTable(_pointsList[10], parentFurniture);
        CreateGetTable(_pointsList[11], parentFurniture);
        CreateGetTable(_pointsList[12], parentFurniture);
        CreateSuvide(_pointsList[13], parentFurniture);
        CreateBlender(_pointsList[14], parentFurniture);
        CreateGarbage(_pointsList[15], parentFurniture);
        CreateOven(_pointsList[16], parentFurniture);
        CreateDistribution(_pointsList[17], parentFurniture);
        CreateStove(_pointsList[18], parentFurniture);
    }
    
    public GameObject CreateGetTable(Transform point, Transform parent)
    {
        return _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.GetTable], point.position, Quaternion.identity, parent);
    }
    
    public GameObject CreateGiveTable(Transform point, Transform parent)
    {
        return _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.GiveTable], point.position, Quaternion.identity, parent);
    }
    
    public GameObject CreateCuttingTable(Transform point, Transform parent)
    {
        return _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.CuttingTable], point.position, Quaternion.identity, parent);
    }
    
    public GameObject CreateGarbage(Transform point, Transform parent)
    {
        return _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Garbage], point.position, Quaternion.identity, parent);
    }
    
    public GameObject CreateOven(Transform point, Transform parent)
    {
        return _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Oven], point.position, Quaternion.identity, parent);
    }
    
    public GameObject CreateBlender(Transform point, Transform parent)
    {
        return _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Blender], point.position, Quaternion.identity, parent);
    }
    
    public GameObject CreateSuvide(Transform point, Transform parent)
    {
        return _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Suvide], point.position, Quaternion.identity, parent);
    }
    
    public GameObject CreateStove(Transform point, Transform parent)
    {
        return _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Stove], point.position, Quaternion.identity, parent);
    }
    
    public GameObject CreateDistribution(Transform point, Transform parent)
    {
        return _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Distribution], point.position, Quaternion.identity, parent);
    }
}
