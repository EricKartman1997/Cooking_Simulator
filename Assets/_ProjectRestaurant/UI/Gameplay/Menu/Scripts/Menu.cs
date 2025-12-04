using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Menu : IDisposable
{
    public event Action ShowAction;
    public event Action HideAction;
    
    private LoadReleaseGlobalScene _loadReleaseGlobalScene;
    private PauseHandler _pauseHandler;
    
    private GameObject _panelSettings;
    
    public bool IsPause => _pauseHandler.IsPause;
    

    public Menu(LoadReleaseGlobalScene loadReleaseGlobalScene,PauseHandler pauseHandler)
    {
        _pauseHandler = pauseHandler;
        _loadReleaseGlobalScene = loadReleaseGlobalScene;
    }

    public void Dispose()
    {
        
    }
    
    public void ContinueButton()
    {
        // выкл паузу
        // Time.timeScale = 1f;
        // AudioListener.pause = false;
        Hide();
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
        _pauseHandler.SetPause(true);
        ShowAction?.Invoke();
    }
    
    public void Hide()
    {
        _pauseHandler.SetPause(false);
        HideAction?.Invoke();
    }
}
