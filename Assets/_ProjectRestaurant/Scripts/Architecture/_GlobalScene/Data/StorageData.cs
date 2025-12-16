using System;
using System.Collections.Generic;
using UnityEngine;

public class StorageData: IReadStorageData
{
    private JsonHandler _jsonHandler;
    
    // поля для взаимодействия
    private List<FurnitureItemData> _itemsEnvironmentList = new List<FurnitureItemData>();
    private OperatingModeMainMenu _operatingModeMainMenu = OperatingModeMainMenu.WithAnInternetConnection;

    // свойства
    public List<FurnitureItemData> ItemsEnvironmentListRead => _itemsEnvironmentList;
    public OperatingModeMainMenu OperatingModeMainMenu => _operatingModeMainMenu;

    public StorageData(JsonHandler jsonHandler)
    {
        _jsonHandler = jsonHandler;
    }

    public void SaveDataJson()
    {
        EnvironmentItems saveObj = new EnvironmentItems(_itemsEnvironmentList);
        _jsonHandler.Save(JsonPathName.ENVIRONMENT_ITEMS_PATH,saveObj);
        
    }

    public void DownloadDataJson()
    {
        _jsonHandler.Load<EnvironmentItems>(JsonPathName.ENVIRONMENT_ITEMS_PATH, data =>
        {
            _itemsEnvironmentList = data.ItemsEnvironmentList;
        });
        
        if (_itemsEnvironmentList.Count == 0)
        {
            Debug.Log("данных нет - перезапустите игру, подключитесь к интернету");
            _operatingModeMainMenu = OperatingModeMainMenu.WithoutAnInternetConnection;
            return;
            //данных нет - перезапустите игру, подключитесь к интернету
        }
        
        _operatingModeMainMenu = OperatingModeMainMenu.WithoutAnInternetConnectionButOutdatedData;
        
    }
}

public enum OperatingModeMainMenu
{
    WithoutAnInternetConnection,
    WithoutAnInternetConnectionButOutdatedData,
    WithAnInternetConnection
}
