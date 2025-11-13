using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GoogleSpreadsheets;
using UnityEngine;
using Zenject;

public class FactoryEnvironment : IDisposable
{
    private IInstantiator _container;
    private LoadReleaseGameplay _loadReleaseGameplay;
    
    public List<FurnitureItemData> _itemsList;

    public FactoryEnvironment(IInstantiator container, LoadReleaseGameplay loadReleaseGameplay)
    {
        _container = container;
        _loadReleaseGameplay = loadReleaseGameplay;
    }
    public void Dispose()
    {
        Debug.Log("FactoryEnvironment.Dispose");
    }

    public async UniTask CreateFurnitureGamePlayAsync()
    {
        GameObject empty = new GameObject("Furniture_Test");
        
        ImportSheetsGoogle importSheetsGoogle = new ImportSheetsGoogle();
        await importSheetsGoogle.LoadItemsSettings(this);
        //Debug.Log($"Count  {_itemsList.Count}");

        foreach (var item in _itemsList)
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
    
    private GameObject CreateGetTable(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.GetTable], itemData.Position, Quaternion.Euler(itemData.Rotation), parent);
        obj.GetComponent<GetTable>().Init(itemData.GiveFood, itemData.ViewFood);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }
    
    private GameObject CreateGiveTable(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.GiveTable], itemData.Position, Quaternion.Euler(itemData.Rotation), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }
    
    private GameObject CreateCuttingTable(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.CuttingTable], itemData.Position, Quaternion.Euler(itemData.Rotation), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }

    private GameObject CreateGarbage(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Garbage], itemData.Position, Quaternion.Euler(itemData.Rotation), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }

    private GameObject CreateOven(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Oven], itemData.Position, Quaternion.Euler(itemData.Rotation), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }

    private GameObject CreateBlender(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Blender], itemData.Position, Quaternion.Euler(itemData.Rotation), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }

    private GameObject CreateSuvide(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Suvide], itemData.Position, Quaternion.Euler(itemData.Rotation), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }

    private GameObject CreateStove(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Stove], itemData.Position, Quaternion.Euler(itemData.Rotation), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }

    private GameObject CreateDistribution(FurnitureItemData itemData, Transform parent)
    {
        //itemData.Show();
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Distribution], itemData.Position, Quaternion.Euler(itemData.Rotation), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }
}

[Serializable]
public class FurnitureItemData
{
    public int Id;
    public string Name;
    public Vector3 Position;
    public Vector3 Rotation;
    public EnumDecorationTableTop DecorationTableTop;
    public EnumDecorationLowerSurface DecorationLowerSurface;
    public IngredientName GiveFood;
    public ViewDishName ViewFood;

    public void Show()
    {
        Debug.Log($"ID = {Id}, Name = {Name}, Position = {Position}, Rotation = {Rotation}," +
                  $" DecorationTableTop = {DecorationTableTop}," +
                  $" DecorationLowerSurface = {DecorationLowerSurface}, GiveFood = {GiveFood}," +
                  $" ViewFood = {ViewFood}");
    }
}


