using UnityEngine;
using UnityEngine.InputSystem;

public class RebindSaveLoaderControlls : MonoBehaviour
{
    public InputActionAsset actions;

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
        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
        {
            actions.LoadBindingOverridesFromJson(rebinds);
            
            // Принудительно обновляем все UI элементы после загрузки биндингов
            //RefreshAllRebindUI();
        }
    }

    private void SaveBindings()
    {
        var rebinds = actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
        PlayerPrefs.Save();
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