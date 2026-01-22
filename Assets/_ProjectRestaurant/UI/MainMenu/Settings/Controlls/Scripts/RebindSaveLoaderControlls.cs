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
    }
}