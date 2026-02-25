using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class StorageData: IReadStorageData
{
    private JsonHandler _jsonHandler;
    
    public List<FurnitureItemData> _itemsFurnitureList = new List<FurnitureItemData>();
    public List<FurnitureItemData> _itemsFurnitureTrainingList = new List<FurnitureItemData>();
    public List<EnvironmentItemData> _itemsEnvironmentList = new List<EnvironmentItemData>();
    private OperatingModeMainMenu _operatingModeMainMenu = OperatingModeMainMenu.WithAnInternetConnection;
    
    public List<FurnitureItemData> ItemsFurnitureListRead => _itemsFurnitureList;
    public List<FurnitureItemData> ItemsFurnitureTrainingListRead => _itemsFurnitureTrainingList;
    public List<EnvironmentItemData> ItemsEnvironmentListRead => _itemsEnvironmentList;
    public OperatingModeMainMenu OperatingModeMainMenu => _operatingModeMainMenu;

    public StorageData(JsonHandler jsonHandler)
    {
        _jsonHandler = jsonHandler;
    }

    public async UniTask SaveDataJson()
    {
        if (_itemsFurnitureList.Count == 0)
        {
            Debug.LogWarning("нет данных для сохранения");
            return;
        }
        
        FurnitureItems saveObj = new FurnitureItems(_itemsFurnitureList);
        await UniTask.Yield();
        _jsonHandler.Save(JsonPathName.FURNITURE_ITEMS_PATH,saveObj);
        
        if (_itemsFurnitureTrainingList.Count == 0)
        {
            Debug.LogWarning("нет данных для сохранения");
            return;
        }
        
        FurnitureTrainingItems saveObj2 = new FurnitureTrainingItems(_itemsFurnitureTrainingList);
        await UniTask.Yield();
        _jsonHandler.Save(JsonPathName.FURNITURE_TRAINING_ITEMS_PATH,saveObj2);
        
        if (_itemsEnvironmentList.Count == 0)
        {
            Debug.LogWarning("нет данных для сохранения");
            return;
        }
        
        EnvironmentItems saveObj1 = new EnvironmentItems(_itemsEnvironmentList);
        await UniTask.Yield();
        _jsonHandler.Save(JsonPathName.ENVIRONMENT_ITEMS_PATH,saveObj1);
    }

    public void DownloadDataJson()
    {
        _jsonHandler.Load<FurnitureItems>(JsonPathName.FURNITURE_ITEMS_PATH, data =>
        {
            _itemsFurnitureList = data.ItemsFurnitureList;
        });
        
        _jsonHandler.Load<FurnitureTrainingItems>(JsonPathName.FURNITURE_TRAINING_ITEMS_PATH, data =>
        {
            _itemsFurnitureTrainingList = data.ItemsFurnitureList;
        });
        
        _jsonHandler.Load<EnvironmentItems>(JsonPathName.ENVIRONMENT_ITEMS_PATH, data =>
        {
            _itemsEnvironmentList = data.ItemsEnvironmentList;
        });
        
        if (_itemsFurnitureList.Count == 0 || _itemsEnvironmentList.Count == 0 || _itemsFurnitureTrainingList.Count == 0)
        {
            Debug.Log("данных нет - перезапустите игру, подключитесь к интернету");
            _operatingModeMainMenu = OperatingModeMainMenu.WithoutAnInternetConnection;
            return;
        }
        
        _operatingModeMainMenu = OperatingModeMainMenu.WithoutAnInternetConnectionButOutdatedData;
        Debug.Log("данные есть, но игра без интернета");
        
    }

    public void ThereIsInternetConnection()
    {
        _operatingModeMainMenu = OperatingModeMainMenu.WithAnInternetConnection;
    }
}

public enum OperatingModeMainMenu
{
    WithoutAnInternetConnection,
    WithoutAnInternetConnectionButOutdatedData,
    WithAnInternetConnection
}
