using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Menu : IDisposable
{
    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    private GameObject _panelSettings;

    public Menu(LoadReleaseGlobalScene loadReleaseGlobalScene)
    {
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
    }

    public void Dispose()
    {
        
    }
    
    public void ContinueButton()
    {
        // выкл паузу
        Time.timeScale = 1f;
        AudioListener.pause = false;
        
    }
    
    public void SettingsButton()
    {
        // открыть настройки
        if (_panelSettings == null)
        {
            Debug.Log("пока не работает");
            return;
        }
        _panelSettings.SetActive(true);
    }
    
    public async UniTask ExitButton()
    {
        // загрузить левел меню
        await _loadReleaseGlobalScene.LoadSceneAsync("SceneMainMenu");
    }

    public void Show()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }
}
