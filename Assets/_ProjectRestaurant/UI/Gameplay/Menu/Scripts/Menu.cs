using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Menu : IDisposable
{
    public event Action ShowMenuAction;
    public event Action HideMenuAction;
    public event Action HideSettingsAction;
    public event Func<bool> IsOpenSettingsAction;
    
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
        ShowMenuAction?.Invoke();
    }
    
    public void Hide()
    {
        if (IsOpenSettingsAction != null && IsOpenSettingsAction.Invoke())
        {
            HideSettingsAction?.Invoke();
            return;
        }
        _pauseHandler.SetPause(false);
        HideMenuAction?.Invoke();
        EventBus.PauseOff.Invoke();
    }
}
