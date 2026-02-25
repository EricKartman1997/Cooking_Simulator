using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class FactoryEnvironmentTraining
{
    private IInstantiator _container;
    private LoadReleaseTraining _loadRelease;
    private OutlineManager _outlineManager;
    
    private List<FurnitureItemData> _furnitureItemsTrainingList;
    
    private GetTableTutorialDecorator _getTableApple;
    private GetTableTutorialDecorator _getTableOrange;
    private GiveTableTutorialDecorator _giveTable;
    private CuttingTableTutorialDecorator _cuttingTable;
    private DistributionTutorialDecorator _distribution;

    public GetTableTutorialDecorator GetTableApple => _getTableApple;

    public GetTableTutorialDecorator GetTableOrange => _getTableOrange;

    public GiveTableTutorialDecorator GiveTable => _giveTable;

    public CuttingTableTutorialDecorator CuttingTable => _cuttingTable;

    public DistributionTutorialDecorator Distribution => _distribution;


    public FactoryEnvironmentTraining(IInstantiator container, LoadReleaseTraining loadRelease,IReadStorageData storageData,OutlineManager outlineManager)
    {
        _outlineManager = outlineManager;
        _container = container;
        _loadRelease = loadRelease;
        _furnitureItemsTrainingList = storageData.ItemsFurnitureTrainingListRead;
    }
    
    public async UniTask CreateFurnitureTrainingGamePlayAsync()
    {
        GameObject empty = new GameObject("FurnitureTraining_Test");
        
        foreach (var item in _furnitureItemsTrainingList)
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
                case "Distribution":
                    CreateDistribution(item, empty.transform);
                    break;
            }
            await UniTask.Yield();
        }

        _outlineManager.FindObjs(empty);
    }
    
    private GameObject CreateGetTable(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadRelease.FurnitureDic[FurnitureNameTraining.GetTableTraining], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        obj.GetComponent<GetTable>().Init(itemData.GiveFood, itemData.ViewFood);
        if (itemData.GiveFood == IngredientName.Apple)
        {
            _getTableApple = obj.GetComponent<GetTableTutorialDecorator>();
        }
        else
        {
            _getTableOrange = obj.GetComponent<GetTableTutorialDecorator>();
        }
        
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }
    
    private GameObject CreateGiveTable(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadRelease.FurnitureDic[FurnitureNameTraining.GiveTableTraining], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        _giveTable = obj.GetComponent<GiveTableTutorialDecorator>();
        return obj;
    }
    
    private GameObject CreateCuttingTable(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadRelease.FurnitureDic[FurnitureNameTraining.CuttingTableTraining], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        _cuttingTable = obj.GetComponent<CuttingTableTutorialDecorator>();
        return obj;
    }

    private GameObject CreateGarbage(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadRelease.FurnitureDic[FurnitureNameTraining.GarbageTraining], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        return obj;
    }
    
    private GameObject CreateDistribution(FurnitureItemData itemData, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(_loadRelease.FurnitureDic[FurnitureNameTraining.DistributionTraining], itemData.PositionVector, Quaternion.Euler(itemData.RotationVector), parent);
        obj.GetComponent<DecorationFurniture>().Init(itemData.DecorationTableTop, itemData.DecorationLowerSurface);
        _distribution = obj.GetComponent<DistributionTutorialDecorator>();
        return obj;
    }
}
