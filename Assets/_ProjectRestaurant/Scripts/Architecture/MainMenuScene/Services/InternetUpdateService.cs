using System;
using Cysharp.Threading.Tasks;
using GoogleSpreadsheets;
using UnityEngine;

public class InternetUpdateService
{
    private readonly StorageData _storageData;
    private readonly BootstrapMainMenu _menu;

    private bool _isWorking;

    public InternetUpdateService(StorageData storageData, BootstrapMainMenu menu)
    {
        _storageData = storageData;
        _menu = menu;
    }

    public async UniTask StartChecking()
    {
        if (_isWorking)
            return;

        _isWorking = true;

        while (_isWorking)
        {
            // ждём 5 секунд между попытками
            await UniTask.Delay(TimeSpan.FromSeconds(5));

            // реальная проверка доступности Google
            if (!await GameUtils.IsGoogleSheetAvailable("1egofOmJAB6kx-TRCUNWvBhR6os8RO5oIzNOYsXNL3t8"))
            {
                Debug.Log("Google недоступен, ждём...");
                continue;
            }

            _menu.ShowPanelWaitTheInternetAction?.Invoke();
            

            try
            {
                //throw new System.Exception("Искусственная ошибка");
                var importer = new ImportSheetsGoogle();
                await importer.LoadItemsSettingsFurniture(_storageData);
                await importer.LoadItemsSettingsFurnitureTraining(_storageData);
                await importer.LoadItemsSettingsEnvironment(_storageData);

                await _storageData.SaveDataJson();

                Debug.Log("Данные успешно загружены");

                await UniTask.Delay(TimeSpan.FromSeconds(2.5));
                _menu.HidePanelWaitTheInternetAction?.Invoke();
                _isWorking = false;
                _storageData.ThereIsInternetConnection();
                _menu.ThereIsInternetAction?.Invoke();
            }
            catch (Exception e)
            {
                Debug.Log($"Ошибка загрузки: {e.Message}");
                _menu.HidePanelWaitTheInternetAction?.Invoke();
            }
        }
    }
    
}
