using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GoogleSpreadsheets;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

public class FactoryEnvironment
{
    private IInstantiator _container;
    private LoadReleaseGameplay _loadReleaseGameplay;
    
    private List<FurnitureItemData> _furnitureItemsList;
    private List<EnvironmentItemData> _environmentItemsList;

    public FactoryEnvironment(IInstantiator container, LoadReleaseGameplay loadReleaseGameplay,IReadStorageData storageData)
    {
        _container = container;
        _loadReleaseGameplay = loadReleaseGameplay;
        _furnitureItemsList = storageData.ItemsFurnitureListRead;
        _environmentItemsList = storageData.ItemsEnvironmentListRead;
    }
    public async UniTask CreateFurnitureGamePlayAsync()
    {
        GameObject empty = new GameObject("Furniture_Test");
        
        foreach (var item in _furnitureItemsList)
        {
            switch (item.Name)
            {
                case "GetTable":
                    CreateGetTable(item, empty.transform);
                    break;
                case "GiveTable":
                    CreateGiveTable(item, empty.transform);
                    break;
                case "CuttingTable":
                    CreateCuttingTable(item, empty.transform);
                    break;
                case "Garbage":
                    CreateGarbage(item, empty.transform);
                    break;
                case "Stove":
                    CreateStove(item, empty.transform);
                    break;
                case "Distribution":
                    CreateDistribution(item, empty.transform);
                    break;
                case "Oven":
                    CreateOven(item, empty.transform);
                    break;
                case "Blender":
                    CreateBlender(item, empty.transform);
                    break;
                case "Suvide":
                    CreateSuvide(item, empty.transform);
                    break;
            }
            await UniTask.Yield();
        }
        // ждать окончание операции
        
    }
    
    public async UniTask CreateEnvironmentGamePlayAsync()
    {
        GameObject empty = new GameObject("Environment_Test");

        foreach (var item in _environmentItemsList)
        {
            switch (item.Name)
            {
                case "Floor":
                    CreateFloor(item, empty.transform);
                    break;
                case "WallDoor":
                    CreateWallDoor(item, empty.transform);
                    break;
                case "WallAngle":
                    CreateWallAngle(item, empty.transform);
                    break;
                case "Wall":
                    CreateWall(item, empty.transform);
                    break;
                case "WallWindow":
                    CreateWallWindow(item, empty.transform);
                    break;
                case "WallTransparent":
                    CreateWallTransparent(item, empty.transform);
                    break;
                case "WallAngleTransparent":
                    CreateWallAngleTransparent(item, empty.transform);
                    break;
                case "AreaTransparent":
                    CreateAreaTransparent(item, empty.transform);
                    break;
            }
            await UniTask.Yield();
        }
        // ждать окончание операции
        
    }

    public void CreateOtherEnvironmentGamePlay()
    {
        GameObject empty = new GameObject("Environment_Test");
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.EnvironmentDic[OtherObjsName.Floor], empty.transform.position, Quaternion.identity, empty.transform);
    }
    
    public void CreateLightsGamePlay()
    {
        GameObject empty = new GameObject("Lights_Test");
        GameObject obj = _container.InstantiatePrefab(
            _loadReleaseGameplay.EnvironmentDic[OtherObjsName.LightMain],
            empty.transform
        );
    }
    
    private GameObject CreateFloor(EnvironmentItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.EnvironmentDic[OtherObjsName.Floor], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        return obj;
    }
    
    private GameObject CreateWall(EnvironmentItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.EnvironmentDic[OtherObjsName.Wall], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        return obj;
    }
    
    private GameObject CreateWallAngle(EnvironmentItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.EnvironmentDic[OtherObjsName.WallAngle], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        return obj;
    }
    
    private GameObject CreateWallDoor(EnvironmentItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.EnvironmentDic[OtherObjsName.WallDoor], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        return obj;
    }
    
    private GameObject CreateWallWindow(EnvironmentItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.EnvironmentDic[OtherObjsName.WallWindow], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        return obj;
    }
    
    private GameObject CreateWallTransparent(EnvironmentItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.EnvironmentDic[OtherObjsName.WallTransparent], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        return obj;
    }
    
    private GameObject CreateWallAngleTransparent(EnvironmentItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.EnvironmentDic[OtherObjsName.WallAngleTransparent], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        return obj;
    }
    
    private GameObject CreateAreaTransparent(EnvironmentItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.EnvironmentDic[OtherObjsName.AreaTransparent], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        obj.GetComponent<TransparentArea>().Init(parent);
        return obj;
    }
    
    private GameObject CreateGetTable(FurnitureItemData itemData, Transform parent)
    {
        //itemData.ShowConnectionTheInternet();
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.GetTable], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        obj.GetComponent<GetTable>().Init(itemData.GiveFood, itemData.ViewFood);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }
    
    private GameObject CreateGiveTable(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.GiveTable], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }
    
    private GameObject CreateCuttingTable(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.CuttingTable], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }

    private GameObject CreateGarbage(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Garbage], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }

    private GameObject CreateOven(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Oven], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }

    private GameObject CreateBlender(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Blender], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }

    private GameObject CreateSuvide(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Suvide], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }

    private GameObject CreateStove(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Stove], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }

    private GameObject CreateDistribution(FurnitureItemData itemData, Transform parent)
    {
        //itemData.ShowConnectionTheInternet();
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Distribution], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }
}

// [Serializable]
// public class FurnitureItemData
// {
//     public int Id;
//     public string Name;
//     public Vector3 Position;
//     public Vector3 Rotation;
//     public CustomFurnitureName DecorationTableTop;
//     public CustomFurnitureName DecorationLowerSurface;
//     public IngredientName GiveFood;
//     public ViewDishName ViewFood;
//
//     public void Show()
//     {
//         Debug.Log($"ID = {Id}, Name = {Name}, Position = {Position}, Rotation = {Rotation}," +
//                   $" DecorationTableTop = {DecorationTableTop}," +
//                   $" DecorationLowerSurface = {DecorationLowerSurface}, GiveFood = {GiveFood}," +
//                   $" ViewFood = {ViewFood}");
//     }
// }



[Serializable]
public class FurnitureItemData
{
    public int Id;
    public string Name;

    public Vector3Data Position;
    public Vector3Data Rotation;

    public CustomFurnitureName DecorationTableTop;
    public CustomFurnitureName DecorationLowerSurface;
    public IngredientName GiveFood;
    public ViewDishName ViewFood;

    [NonSerialized] private Vector3? _cachedPosition;
    [NonSerialized] private Vector3? _cachedRotation;

    [JsonIgnore] // <= ВАЖНО
    public Vector3 PositionVector
    {
        get
        {
            if (_cachedPosition == null)
                _cachedPosition = Position.ToVector3();
            return _cachedPosition.Value;
        }
    }

    [JsonIgnore] // <= ВАЖНО
    public Vector3 RotationVector
    {
        get
        {
            if (_cachedRotation == null)
                _cachedRotation = Rotation.ToVector3();
            return _cachedRotation.Value;
        }
    }
}

[Serializable]
public class EnvironmentItemData
{
    public int Id;
    public string Name;

    public Vector3Data Position;
    public Vector3Data Rotation;

    [NonSerialized] private Vector3? _cachedPosition;
    [NonSerialized] private Vector3? _cachedRotation;

    [JsonIgnore] // <= ВАЖНО
    public Vector3 PositionVector
    {
        get
        {
            if (_cachedPosition == null)
                _cachedPosition = Position.ToVector3();
            return _cachedPosition.Value;
        }
    }

    [JsonIgnore] // <= ВАЖНО
    public Vector3 RotationVector
    {
        get
        {
            if (_cachedRotation == null)
                _cachedRotation = Rotation.ToVector3();
            return _cachedRotation.Value;
        }
    }
}


[Serializable]
public struct Vector3Data
{
    public float x;
    public float y;
    public float z;

    public Vector3Data(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3Data(Vector3 v)
    {
        x = v.x;
        y = v.y;
        z = v.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}






