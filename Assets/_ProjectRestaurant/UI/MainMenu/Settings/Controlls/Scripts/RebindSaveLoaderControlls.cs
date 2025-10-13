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

    public void Start()
    {
        //LoadBindings();
    }

    public void OnEnable()
    {
        LoadBindings();
    }

    public void OnDisable()
    {
        SaveBindings();
    }

    private void LoadBindings()
    {
        // var rebinds = PlayerPrefs.GetString("rebinds");
        // Debug.Log(rebinds);
        // if (!string.IsNullOrEmpty(rebinds))
        // {
        //     actions.LoadBindingOverridesFromJson(rebinds);
        //     
        //     // Принудительно обновляем все UI элементы после загрузки биндингов
        //     //RefreshAllRebindUI();
        // }
        
        _jsonHandler.Load<BindingSettings>(JsonPathName.BINDINGS_SETTINGS_PATH, date =>
        {
            actions.LoadBindingOverridesFromJson(date.Rebinds);
        });
        
        
    }

    private void SaveBindings()
    {
        var rebinds = actions.SaveBindingOverridesAsJson();
        Debug.Log(rebinds);
        BindingSettings saveObj = new BindingSettings(rebinds);
        _jsonHandler.Save(JsonPathName.BINDINGS_SETTINGS_PATH,saveObj);
        
        
        //PlayerPrefs.SetString("rebinds", rebinds);
        //PlayerPrefs.Save();
    }

    private void RefreshAllRebindUI()
    {
        // Находим все UI элементы ребиндинга и обновляем их отображение
        var allRebindUI = FindObjectsOfType<RebindControllsUI>();
        foreach (var rebindUI in allRebindUI)
        {
            rebindUI.UpdateBindingDisplay();
        }
    }
}