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

    public async UniTask CreateFurnitureGamePlayAsync(Transform parentFurniture)
    {
        ImportSheetsGoogle importSheetsGoogle = new ImportSheetsGoogle();
        await importSheetsGoogle.LoadItemsSettings(this);

        foreach (var item in _itemsList)
        {
            switch (item.Name)
            {
                case "GetTable":
                    CreateGetTable(item, parentFurniture);
                    break;
                case "GiveTable":
                    CreateGiveTable(item, parentFurniture);
                    break;
                case "CuttingTable":
                    CreateCuttingTable(item, parentFurniture);
                    break;
                case "Garbage":
                    CreateGarbage(item, parentFurniture);
                    break;
                case "Stove":
                    CreateStove(item, parentFurniture);
                    break;
                case "Distribution":
                    CreateDistribution(item, parentFurniture);
                    break;
                case "Oven":
                    CreateOven(item, parentFurniture);
                    break;
                case "Blender":
                    CreateBlender(item, parentFurniture);
                    break;
                case "Suvide":
                    CreateSuvide(item, parentFurniture);
                    break;
            }
            await UniTask.Yield();
        }
        // ждать окончание операции
        
    }
    
    public GameObject CreateGetTable(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.GetTable], itemData.Position, Quaternion.Euler(itemData.Rotation), parent);
        obj.GetComponent<GetTable>().Init(itemData.GiveFood, itemData.ViewFood);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }
    
    public GameObject CreateGiveTable(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.GiveTable], itemData.Position, Quaternion.Euler(itemData.Rotation), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }
    
        public GameObject CreateCuttingTable(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.CuttingTable], itemData.Position, Quaternion.Euler(itemData.Rotation), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }

    public GameObject CreateGarbage(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Garbage], itemData.Position, Quaternion.Euler(itemData.Rotation), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }

    public GameObject CreateOven(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Oven], itemData.Position, Quaternion.Euler(itemData.Rotation), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }

    public GameObject CreateBlender(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Blender], itemData.Position, Quaternion.Euler(itemData.Rotation), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }

    public GameObject CreateSuvide(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Suvide], itemData.Position, Quaternion.Euler(itemData.Rotation), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }

    public GameObject CreateStove(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadReleaseGameplay.FurnitureDic[FurnitureName.Stove], itemData.Position, Quaternion.Euler(itemData.Rotation), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }

    public GameObject CreateDistribution(FurnitureItemData itemData, Transform parent)
    {
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
    public EnumGiveFood GiveFood;
    public EnumViewFood ViewFood;
}


