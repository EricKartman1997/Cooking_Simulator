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

    public async void StartChecking()
    {
        if (_isWorking)
            return;

        _isWorking = true;

        while (_isWorking)
        {
            // ждём 5 секунд между попытками
            await UniTask.Delay(TimeSpan.FromSeconds(5));

            // реальная проверка доступности Google
            if (!await GameUtils.IsGoogleSheetsAvailable())
            {
                Debug.Log("Google недоступен, ждём...");
                continue;
            }

            _menu.ShowLoadingPanel();

            try
            {
                var importer = new ImportSheetsGoogle();
                await importer.LoadItemsSettingsProbnic(_storageData);

                _storageData.SaveDataJson();

                Debug.Log("Данные успешно загружены");

                _menu.HideLoadingPanel();
                _isWorking = false;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Ошибка загрузки: {e.Message}");
                _menu.HideLoadingPanel();
            }
        }
    }
    
}
