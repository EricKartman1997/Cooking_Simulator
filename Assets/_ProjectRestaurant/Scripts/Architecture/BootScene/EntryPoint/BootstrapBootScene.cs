using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using GoogleSpreadsheets;

public class BootstrapBootScene : MonoBehaviour
{
    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    private StorageData _storageData;
    
    [Inject]
    private void ConstructZenject(LoadReleaseGlobalScene loadReleaseGlobalScene, StorageData storageData)
    {
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
        _storageData = storageData;
    }
    private void Start()
    {
        InitializeAsync().Forget();
    }

    private async UniTaskVoid InitializeAsync()
    {
        await LoadGoogleSheets();
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        //await LoadJson();
        //Debug.Log("запуск сцены");
        await _loadReleaseGlobalScene.LoadSceneAsync(ScenesNames.SCENE_MAINMENU);
        //Debug.Log("загрузил сцену");
    }

    private async UniTask LoadGoogleSheets()
    {
        try
        {
            //throw new System.Exception("Искусственная ошибка");

            ImportSheetsGoogle importSheetsGoogle = new ImportSheetsGoogle();
            await importSheetsGoogle.LoadItemsSettingsEnvironment(_storageData);
            await importSheetsGoogle.LoadItemsSettingsFurniture(_storageData);
            await importSheetsGoogle.LoadItemsSettingsFurnitureTraining(_storageData);
            
            await _storageData.SaveDataJson(); // сохранить данные в Json
            //Debug.Log("(сохранение) Json Environment, Furniture");
        }
        catch (Exception e)
        {
            Debug.Log("Ошибка загрузки данных в BootScene");
            Console.WriteLine(e);
            _storageData.DownloadDataJson(); // загрузить данные из Json
            //throw;
        }
        
    }
}
