using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using GoogleSpreadsheets;

public class BootstrapBootScene : MonoBehaviour
{
    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    private IStorageJson _jsonHandler;
    private StorageData _storageData;
    
    [Inject]
    private void ConstructZenject(LoadReleaseGlobalScene loadReleaseGlobalScene, IStorageJson jsonHandler, StorageData storageData)
    {
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
        _jsonHandler = jsonHandler;
    }
    private void Start()
    {
        InitializeAsync().Forget();
    }

    private async UniTaskVoid InitializeAsync()
    {
        await LoadGoogleSheets();
        //await UniTask.Delay(TimeSpan.FromSeconds(1));
        await _loadReleaseGlobalScene.LoadSceneAsync(ScenesNames.SCENE_MAINMENU);
    }

    private async UniTask LoadGoogleSheets()
    {
        try
        {
            ImportSheetsGoogle importSheetsGoogle = new ImportSheetsGoogle();
            await importSheetsGoogle.LoadItemsSettings<FurnitureTrainingItemDataParser>(_jsonHandler);
            await importSheetsGoogle.LoadItemsSettings<FurnitureItemDataParser>(_jsonHandler);
            await importSheetsGoogle.LoadItemsSettings<EnvironmentItemDataParser>(_jsonHandler);
        }
        catch (Exception e)
        {
            Debug.Log("Ошибка загрузки данных в BootScene");
            Console.WriteLine(e);
            _storageData.DownloadDataJson();
        }
        
    }
}
