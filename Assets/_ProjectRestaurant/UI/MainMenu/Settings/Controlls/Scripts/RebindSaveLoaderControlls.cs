using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class RebindSaveLoaderControlls : MonoBehaviour
{
    public InputActionAsset actions;
    private IStorageJson _jsonHandler;

    [Inject]
    private void ConstructZenject(IStorageJson jsonHandler)
    {
        _jsonHandler = jsonHandler;
    }

    private void Awake()
    {
        LoadBindings();
    }

    public void LoadBindings()
    {
        _jsonHandler.Load<BindingSettings>(JsonPathName.BINDINGS_SETTINGS_PATH, data =>
        {
            if (data != null && !string.IsNullOrEmpty(data.Rebinds))
            {
                actions.Disable();
                actions.LoadBindingOverridesFromJson(data.Rebinds);
                actions.Enable();
                
                // КРИТИЧНО: После загрузки говорим всем кнопкам обновить текст
                var allUI = Object.FindObjectsByType<RebindControllsUI>(FindObjectsSortMode.None);
                foreach (var ui in allUI) ui.UpdateBindingDisplay();
                
                Debug.Log("Управление загружено и UI обновлен");
            }
        });
    }

    public void SaveBindings()
    {
        // 1. Берем текущую строку ребиндов из ассета
        var rebinds = actions.SaveBindingOverridesAsJson();
        BindingSettings saveObj = new BindingSettings(rebinds);
    
        // 2. Сохраняем в JSON (файл на диске)
        _jsonHandler.Save(JsonPathName.BINDINGS_SETTINGS_PATH, saveObj, success => 
        {
            if(success) Debug.Log("Управление успешно сохранено в JSON");
        });

        // 3. МГНОВЕННОЕ ОБНОВЛЕНИЕ ДЛЯ ИГРОКА (Вот решение!):
        // Ищем все компоненты PlayerInput на сцене (твой персонаж)
        var activePlayers = Object.FindObjectsByType<PlayerInput>(FindObjectsSortMode.None);
    
        foreach (var player in activePlayers)
        {
            // Принудительно заталкиваем новые ребинды в игрока
            player.actions.LoadBindingOverridesFromJson(rebinds);
        
            // На всякий случай пересобираем связи
            player.actions.Enable(); 
        
            Debug.Log($"Управление игрока {player.gameObject.name} обновлено в реальном времени!");
        }
    }
}