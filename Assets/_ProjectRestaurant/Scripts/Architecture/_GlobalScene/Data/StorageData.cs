using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class StorageData: IReadStorageData
{
    private JsonHandler _jsonHandler;
    
    // поля для взаимодействия
    public List<FurnitureItemData> _itemsFurnitureList = new List<FurnitureItemData>();
    public List<EnvironmentItemData> _itemsEnvironmentList = new List<EnvironmentItemData>();
    private OperatingModeMainMenu _operatingModeMainMenu = OperatingModeMainMenu.WithAnInternetConnection;

    // свойства
    public List<FurnitureItemData> ItemsFurnitureListRead => _itemsFurnitureList;
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
        Debug.Log("Прошел сохранение Furniture");
        
        if (_itemsEnvironmentList.Count == 0)
        {
            Debug.LogWarning("нет данных для сохранения");
            return;
        }
        
        EnvironmentItems saveObj1 = new EnvironmentItems(_itemsEnvironmentList);
        await UniTask.Yield();
        _jsonHandler.Save(JsonPathName.ENVIRONMENT_ITEMS_PATH,saveObj1);
        Debug.Log("Прошел сохранение Environment");
    }

    public void DownloadDataJson()
    {
        _jsonHandler.Load<FurnitureItems>(JsonPathName.FURNITURE_ITEMS_PATH, data =>
        {
            _itemsFurnitureList = data.ItemsFurnitureList;
        });
        
        _jsonHandler.Load<EnvironmentItems>(JsonPathName.ENVIRONMENT_ITEMS_PATH, data =>
        {
            _itemsEnvironmentList = data.ItemsEnvironmentList;
        });
        
        if (_itemsFurnitureList.Count == 0)
        {
            Debug.Log("данных нет - перезапустите игру, подключитесь к интернету");
            _operatingModeMainMenu = OperatingModeMainMenu.WithoutAnInternetConnection;
            return;
            //данных нет - перезапустите игру, подключитесь к интернету
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
